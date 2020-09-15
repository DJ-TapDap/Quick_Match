using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    public CharacterController playerCharacter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float movementX = Input.GetAxisRaw ("Horizontal");
        Vector3 velocity = 6 * (transform.right * movementX);   //determines velocity vector or movement vector of the player
        playerCharacter.Move (velocity * Time.deltaTime);
    }

	//private void OnTriggerEnter (Collider other)
	//{
    //    print ("COLLISION");
    //    print (other.gameObject.tag);
	//}
}
