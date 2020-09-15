using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testObstacle : MonoBehaviour
{
    private void OnTriggerEnter (Collider other)
    {
        print ("COLLISION");
        print (other.gameObject.tag);
        if (other.gameObject.tag == "Player") {
            Player_Controller target = other.gameObject.GetComponent<Player_Controller> ();
            target.takeDmg (1000);
        }
    }
}
