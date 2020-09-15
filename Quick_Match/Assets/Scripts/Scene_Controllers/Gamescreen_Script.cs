using UnityEngine;
using System.Collections;

public class Gamescreen_Script : MonoBehaviour {
	Rematch_Script rematchScript;
	Char_Selection_Script charSelectionScript;
	public GameObject player_1_Warrior;
	public GameObject player_2_Warrior;
	public GameObject player_1_Mage;
	public GameObject player_2_Mage;
	public GameObject playersGroup;

	public GameObject _player_1_Warrior;
	public GameObject _player_2_Warrior;
	public GameObject _player_1_Mage;
	public GameObject _player_2_Mage;

	// Use this for initialization
	void Start ()
	{
		player_1_Mage.SetActive (false);
		player_1_Warrior.SetActive (false);
		player_2_Mage.SetActive (false);
		player_2_Warrior.SetActive (false);

		rematchScript = FindObjectOfType<Rematch_Script> ();
		rematchScript.rematch += resetStage;
		rematchScript.exit += clearStage;

		charSelectionScript = FindObjectOfType<Char_Selection_Script> ();
		charSelectionScript.choose_11 += choosePlayer1_Warrior;
		charSelectionScript.choose_12 += choosePlayer1_Mage;
		charSelectionScript.choose_21 += choosePlayer2_Warrior;
		charSelectionScript.choose_22 += choosePlayer2_Mage;
	}

	void resetStage ()
	{
		
		if (player_1_Mage != null) {
			Player_Controller player_1_Mage_Controller = _player_1_Mage.GetComponent<Player_Controller> ();
			player_1_Mage_Controller.setHealth (100);
			Drone_Animations droneMage_Player_1_Controller = _player_1_Mage.GetComponent<Drone_Animations> ();
			droneMage_Player_1_Controller.resetDrones ();
		}
		if (player_1_Warrior != null) {
			Player_Controller player_1_Warrior_Controller = _player_1_Warrior.GetComponent<Player_Controller> ();
			player_1_Warrior_Controller.setHealth (100);
		}
		if (player_2_Mage != null) {
			Player_Controller player_2_Mage_Controller = _player_2_Mage.GetComponent<Player_Controller> ();
			player_2_Mage_Controller.setHealth (100);
			Drone_Animations droneMage_Player_2_Controller = _player_2_Mage.GetComponent<Drone_Animations> ();
			droneMage_Player_2_Controller.resetDrones ();
		}
		if (player_2_Warrior != null) {
			Player_Controller player_2_Warrior_Controller = _player_2_Warrior.GetComponent<Player_Controller> ();
			player_2_Warrior_Controller.setHealth (100);
		}

		playersGroup.SetActive (false);

		_player_1_Mage.transform.position = new Vector3(-4.5f, 2.27f, 0f);
		_player_1_Warrior.transform.position = new Vector3 (-4.5f, 2.27f, 0f);
		_player_2_Mage.transform.position = new Vector3 (4.5f, 2.27f, 0f);
		_player_2_Warrior.transform.position = new Vector3 (4.5f, 2.27f, 0f);

		_player_1_Mage.SetActive (true);
		_player_1_Warrior.SetActive (true);
		_player_2_Mage.SetActive (true);
		_player_2_Warrior.SetActive (true);

		playersGroup.SetActive (true);
	}


	void clearStage()
	{
		resetStage ();
		player_1_Mage.SetActive (false);
		player_1_Warrior.SetActive (false);
		player_2_Mage.SetActive (false);
		player_2_Warrior.SetActive (false);
	}

	void choosePlayer1_Mage ()
	{
		player_1_Mage.SetActive (true);
		player_1_Warrior.SetActive (false);
	}

	void choosePlayer1_Warrior ()
	{
		player_1_Mage.SetActive (false);
		player_1_Warrior.SetActive (true);
	}

	void choosePlayer2_Mage ()
	{
		player_2_Mage.SetActive (true);
		player_2_Warrior.SetActive (false);
	}

	void choosePlayer2_Warrior ()
	{
		player_2_Mage.SetActive (false);
		player_2_Warrior.SetActive (true);

	}
}
