using System.Collections;
using UnityEngine;
using System;
using TMPro;

public class Char_Selection_Script : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject playerGroup;
    public LayerMask playerMask;
    public GameObject stageVisibility;

    public GameObject confirmButton;
    public LayerMask buttonMask;
    public Material buttonSelectionMat;
    public Material buttonSelectedMat;
    public Material backgroundMat;
    bool buttonSelected = false;

    public GameObject charSelector_11;
    public GameObject charSelector_12;
    public GameObject charSelector_21;
    public GameObject charSelector_22;

    public GameObject charDisplay_11;
    public GameObject charDisplay_12;
    public GameObject charDisplay_21;
    public GameObject charDisplay_22;

    bool select_11 = false;
    bool select_12 = false;
    bool select_21 = false;
    bool select_22 = false;

    public GameObject selectAnimBeam_11;
    public GameObject selectAnimBeam_12;
    public GameObject selectAnimBeam_21;
    public GameObject selectAnimBeam_22;

    Rematch_Script rematchScript;
    public event Action choose_11;
    public event Action choose_12;
    public event Action choose_21;
    public event Action choose_22;

    public GameObject menuController;
    public GameObject menuContent;
    public GameObject optionContent;

    public GameObject player_1;
    public GameObject player_2;

    public int player_1_exit;
    public int player_2_exit;
    public TextMeshPro P1_Exit;
    public TextMeshPro P2_Exit;

    // Start is called before the first frame update
    void Start()
    {
        confirmButton.SetActive (false);
        rematchScript = FindObjectOfType<Rematch_Script> ();
        rematchScript.exit += returnCam;
    }

    // Update is called once per frame
    void Update()
    {
        if (!menuContent.activeSelf && !optionContent.activeSelf) { checkExit (); }

        bool detect_11 = detectorProcess (charSelector_11);
        bool detect_12 = detectorProcess (charSelector_12);
        bool detect_21 = detectorProcess (charSelector_21);
        bool detect_22 = detectorProcess (charSelector_22);

        character_1_Select (detect_11, detect_12);
        character_2_Select (detect_21, detect_22);


        if((select_11 || select_12) && (select_21 || select_22))
        {
            confirmButton.SetActive (true);
            Renderer buttonMat = confirmButton.GetComponent<Renderer> ();
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;


            if (Physics.Raycast (ray, out hit, 19f, buttonMask)) {    //detects if the mouse is hovering over the button and turns the button into the appropriate color
                buttonMat.material = buttonSelectionMat;
                if (Input.GetMouseButton (0)) {                          //detects if the mouse is holding down on the button
                    buttonMat.material = buttonSelectedMat;
                }
                if (Input.GetMouseButtonUp (0)) {                        //detects if the player actually selects the button / pushes the button
                    buttonSelected = true;
                }
            } else {
                buttonMat.material = backgroundMat;
            }


            if (buttonSelected) {
                if (select_11) {
                    Selection_Animation animBeam_11_Script = selectAnimBeam_11.GetComponent<Selection_Animation> ();
                    choose_11?.Invoke ();
                    charDisplay_11.SetActive (false);
                    animBeam_11_Script.beginMatch ();
                } else if (select_12) {
                    Selection_Animation animBeam_12_Script = selectAnimBeam_12.GetComponent<Selection_Animation> ();
                    choose_12?.Invoke ();
                    charDisplay_12.SetActive (false);
                    animBeam_12_Script.beginMatch ();
                }

                if (select_21) {
                    Selection_Animation animBeam_Script_21 = selectAnimBeam_21.GetComponent<Selection_Animation> ();
                    choose_21?.Invoke ();
                    charDisplay_21.SetActive (false);
                    animBeam_Script_21.beginMatch ();
                } else if (select_22) {
                    Selection_Animation animBeam_Script_22 = selectAnimBeam_22.GetComponent<Selection_Animation> ();
                    choose_22?.Invoke ();
                    charDisplay_22.SetActive (false);
                    animBeam_Script_22.beginMatch ();
                }

                buttonSelected = false;
                select_11 = false;
                select_12 = false;
                select_21 = false;
                select_22 = false;
                StartCoroutine (moveCam_PoofStage ());
            }

        }
        else {
            confirmButton.SetActive (false);
		}
    }

    void checkExit()
	{
        if (Input.GetKey (KeyCode.B) && player_1_exit >= 100) {
            P1_Exit.SetText ("EXITING...");
            Menu_Controller menuScript = menuController.GetComponent<Menu_Controller> ();
            menuScript.exitGameProcess ();
		}
        else if (Input.GetKey (KeyCode.B)) {
            player_1_exit += 1;
            P1_Exit.SetText ("Exit: " + player_1_exit.ToString());
        } else {
            player_1_exit = 0;
            P1_Exit.SetText ("");
        }


        if (Input.GetKey (KeyCode.Return) && player_2_exit >= 100) {
            P2_Exit.SetText ("EXITING...");
            Menu_Controller menuScript = menuController.GetComponent<Menu_Controller> ();
            menuScript.exitGameProcess ();
        }
        else if (Input.GetKey (KeyCode.Return)) {
            player_2_exit += 1;
            P2_Exit.SetText ("Exit: " + player_2_exit.ToString ());
        } else {
            player_2_exit = 0;
            P2_Exit.SetText ("");
        }

        
    }
    IEnumerator moveCam_PoofStage ()
	{
        yield return new WaitForSeconds (1);
        camAnimation_Script camScript = mainCamera.GetComponent<camAnimation_Script> ();
        camScript.moveCam ();
        yield return new WaitForSeconds (5);
        stageVisibility.SetActive (false);
	}
    public void returnCam()
	{
        stageVisibility.SetActive (true);
        charDisplay_11.SetActive (false);
        charDisplay_12.SetActive (false);
        charDisplay_21.SetActive (false);
        charDisplay_22.SetActive (false);
        player_1.SetActive (false);
        player_2.SetActive (false);

        player_1.transform.position = new Vector3 (-10.14f, 21.1f, 0f);
        player_2.transform.position = new Vector3 (10.14f, 21.1f, 0f);

        charDisplay_11.transform.position = new Vector3 (-3.85f, 22.24f, 2.93f);
        charDisplay_12.transform.position = new Vector3 (-7.57f, 22.1f, 2.93f);
        charDisplay_21.transform.position = new Vector3 (4.01f, 22.24f, 2.93f);
        charDisplay_22.transform.position = new Vector3 (7.63f, 22.1f, 2.93f);

        player_1.SetActive (true);
        player_2.SetActive (true);
        charDisplay_11.SetActive (true);
        charDisplay_12.SetActive (true);
        charDisplay_21.SetActive (true);
        charDisplay_22.SetActive (true);

        select_11 = false;
        select_12 = false;
        select_21 = false;
        select_22 = false;

        camAnimation_Script camScript = mainCamera.GetComponent<camAnimation_Script> ();
        camScript.moveCam ();
    }
	bool detectorProcess(GameObject detector)
	{
        Collider [] hitColliders = Physics.OverlapBox (detector.transform.position, detector.transform.localScale / 2, detector.transform.rotation, playerMask);
        if (hitColliders.Length > 0) {
            return true;
        }
        else {
            return false;
		}
	}
    void character_1_Select(bool detect_11, bool detect_12)
	{
        Selection_Animation animBeam_11_Script = selectAnimBeam_11.GetComponent<Selection_Animation> ();
        Selection_Animation animBeam_12_Script = selectAnimBeam_12.GetComponent<Selection_Animation> ();

        if (detect_11 && Input.GetKeyDown (KeyCode.V) && !select_11 && !select_12) {
            select_11 = true;
            animBeam_11_Script.select ();
        }
        if (Input.GetKeyDown (KeyCode.B) && select_11) {
            select_11 = false;
            animBeam_11_Script.deselect ();
        }
        if (detect_12 && Input.GetKeyDown (KeyCode.V) && !select_11 && !select_12) {
            select_12 = true;
            animBeam_12_Script.select ();
        }
        if (Input.GetKeyDown (KeyCode.B) && select_12) {
            select_12 = false;
            animBeam_12_Script.deselect ();
        }
    }
    void character_2_Select (bool detect_21, bool detect_22)
    {
        Selection_Animation animBeam_Script_1 = selectAnimBeam_21.GetComponent<Selection_Animation> ();
        Selection_Animation animBeam_Script_2 = selectAnimBeam_22.GetComponent<Selection_Animation> ();

        if (detect_21 && Input.GetKeyDown (KeyCode.RightShift) && !select_21 && !select_22) {
            select_21 = true;
            animBeam_Script_1.select ();
        }
        if (Input.GetKeyDown (KeyCode.Return) && select_21) {
            select_21 = false;
            animBeam_Script_1.deselect ();
        }
        if (detect_22 && Input.GetKeyDown (KeyCode.RightShift) && !select_21 && !select_22) {
            select_22 = true;
            animBeam_Script_2.select ();
        }
        if (Input.GetKeyDown (KeyCode.Return) && select_22) {
            select_22 = false;
            animBeam_Script_2.deselect ();
        }
    }
    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        
        //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
        Gizmos.DrawWireCube (charSelector_11.transform.position, charSelector_11.transform.localScale);
        Gizmos.DrawWireCube (charSelector_12.transform.position, charSelector_12.transform.localScale);
        Gizmos.DrawWireCube (charSelector_21.transform.position, charSelector_21.transform.localScale);
        Gizmos.DrawWireCube (charSelector_22.transform.position, charSelector_22.transform.localScale);
    }
}
