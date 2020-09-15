using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Zone : MonoBehaviour
{
    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.GetComponent<Player_Controller>() != null) {
            Player_Controller target = other.gameObject.GetComponent<Player_Controller> ();
            target.takeDmg (1000);
        }
    }
}
