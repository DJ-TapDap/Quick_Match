using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health_UI : MonoBehaviour
{
    public GameObject player;
    public TextMeshPro healthDisplay;

	// Update is called once per frame
	private void Start ()
	{
        Player_Controller playerScript = player.GetComponent<Player_Controller> ();
        playerScript.updateHealth += displayHeath;
	}

	void displayHeath()
    {
        Player_Controller playerScript = player.GetComponent<Player_Controller> ();
        int playerHealth = playerScript.getHealth ();
        if (playerHealth > 0) { healthDisplay.SetText (new string ('|', playerHealth / 5)); }
        else { healthDisplay.SetText (""); }
    }
}
