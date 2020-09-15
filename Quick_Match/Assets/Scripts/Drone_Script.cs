using System.Collections;
using UnityEngine;

public class Drone_Script : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    private static readonly int hashBoom = Animator.StringToHash ("Boom");

    public Vector3 targetPosition;
    public Quaternion droneRotation;
    public Transform droneBody;
    public Transform explosionObject;
    public float speed = 14f;
    public float explosionRadius;
    public float explosionDmg;
    bool targetReached = false;
    public LayerMask hitLayerMask;

    bool explosionHappened = false;

	// Update is called once per frame

	void Update()
    {
        if (!targetReached) {
            travelToTarget (targetPosition);
		}
        else if (!explosionHappened){
            explosionHappened = true;
            StartCoroutine (explosion ());
		}
    }

    void travelToTarget(Vector3 targetPosition)
	{
        Vector3 displacementFromTarget = targetPosition - transform.position;
        Vector3 directionToTarget = displacementFromTarget.normalized;
        Vector3 velocity = directionToTarget * speed;

        float distanceToTarget = displacementFromTarget.magnitude;

        if (distanceToTarget > 1.5f) {
            transform.Translate (velocity * Time.deltaTime);
        }
        else {
            targetReached = true;
		}
    }
    public void orientate(Quaternion droneRotation)
	{
        droneBody.rotation = droneRotation;
	}
    public void setTarget(Vector3 targetPos)
	{
        targetPosition = targetPos + transform.position;
        print (targetPosition);
	}

    IEnumerator explosion()
	{
        yield return new WaitForSeconds (0.5f);
        animator.SetTrigger (hashBoom);

    }
    public void explosionDamage()
	{
        Collider[] playerHit = Physics.OverlapSphere (explosionObject.position, explosionRadius, hitLayerMask);
        foreach (var player in playerHit) {
            Player_Controller playerScript = player.GetComponent<Player_Controller> ();
            playerScript.takeDmg (explosionDmg);
		}
	}
    public void explosionPoof()
	{
        Destroy (gameObject);
	}
}
