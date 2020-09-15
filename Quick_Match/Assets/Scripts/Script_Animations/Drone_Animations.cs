using UnityEngine;

public class Drone_Animations : MonoBehaviour
{
    public GameObject dronePrefab;
    public Transform droneSet;
    public Transform playerBody;
    public float rotateSpeed = 80f;
    public GameObject[] droneList;
    private int droneIterator = 0;

    public float range;
    public Vector3 forwardDirection;

    // Update is called once per frame
    void Update()
    {
        forwardDirection = playerBody.forward;
        droneSet.Rotate (new Vector3 (0, 1, 0), Time.deltaTime * rotateSpeed);
    }

    void basicAttackStart()
	{
        rotateSpeed = rotateSpeed * 20f;
	}

    void basicAttackEnd ()
    {
        rotateSpeed = rotateSpeed / 20f;
    }

    void deYEETDrone()
	{
        if (droneIterator < droneList.Length) {
            GameObject drone = Instantiate (dronePrefab, droneList [droneIterator].transform.position, Quaternion.LookRotation (new Vector3 (0, 0, 1), Vector3.up));
            Drone_Script droneScript = drone.GetComponent<Drone_Script> ();
            droneScript.orientate (droneList [droneIterator].transform.rotation);
            droneScript.setTarget (forwardDirection * range);

            droneList [droneIterator].SetActive (false);
            droneIterator += 1;
        }
	}

    public void resetDrones()
	{
        foreach (GameObject drone in droneList) {
            drone.SetActive (true);
		}
        droneIterator = 0;
	}
}
