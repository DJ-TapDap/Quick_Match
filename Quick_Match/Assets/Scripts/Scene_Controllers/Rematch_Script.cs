using UnityEngine;
using TMPro;
using System;

public class Rematch_Script : MonoBehaviour
{
    
    public GameObject playerGroup;
    public GameObject respawnText;

    public GameObject yesButton;
    public LayerMask yesButtonMask;
    public GameObject noButton;
    public LayerMask noButtonMask;
    public GameObject background;

    public Material buttonSelectionMat;
    public Material buttonSelectedMat;

    public MeshRenderer shaderRenderer;
    Material backgroundMat;

    public TextMeshPro player1_Win_text;
    public TextMeshPro player2_Win_text;

    public float transitionSpeed;
    bool transition = false;
    float transitionProgress;
    bool startingTransition = false;
    bool backToCharSelection = false;

    public event Action rematch;
    public event Action exit;

    // Update is called once per frame
    private void Start ()
	{
        player1_Win_text.gameObject.SetActive (false);
        player2_Win_text.gameObject.SetActive (false);
        backgroundMat = shaderRenderer.material;
        respawnText.SetActive (false);

        GameObject [] player_1_List = GameObject.FindGameObjectsWithTag ("Player 1");
        foreach (GameObject player_1 in player_1_List) {
            Player_Controller player_1Script = player_1.GetComponent<Player_Controller> ();
            player_1Script.Player_Death += player2_Won;
		}

        GameObject [] player_2_List = GameObject.FindGameObjectsWithTag ("Player 2");
        foreach (GameObject player_2 in player_2_List) {
            Player_Controller player_2Script = player_2.GetComponent<Player_Controller> ();
            player_2Script.Player_Death += player1_Won;
        }

        backgroundMat.SetFloat ("Vector1_A8CE1D87", 1f);
        transitionProgress = backgroundMat.GetFloat ("Vector1_A8CE1D87");
    }

	void Update()
    {

        if (startingTransition) {
            if (transitionProgress <= -0.05f) {
                startingTransition = false;
                background.SetActive (true);
                respawnText.SetActive (true);
                playerGroup.SetActive (false);
            } else {
                backgroundMat.SetFloat ("Vector1_A8CE1D87", transitionProgress);
                transitionProgress -= transitionSpeed * Time.deltaTime;
            }
        }



        Renderer yesMaterial = yesButton.GetComponent<Renderer> ();
        Renderer noMaterial = noButton.GetComponent<Renderer> ();
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 19f, yesButtonMask)) {    //detects if the mouse is hovering over the button and turns the button into the appropriate color
            yesMaterial.material = buttonSelectionMat;
            if (Input.GetMouseButton(0)) {                          //detects if the mouse is holding down on the button
                yesMaterial.material = buttonSelectedMat;
            }
            if (Input.GetMouseButtonUp(0)) {                        //detects if the player actually selects the button / pushes the button
                transition = true;
                respawnText.SetActive (false);
                playerGroup.SetActive (true);
                rematch?.Invoke ();
			}
        } else {
            yesMaterial.material = backgroundMat;
        }

        if (Physics.Raycast (ray, out hit, 19f, noButtonMask)) {    //repeat of code above
            noMaterial.material = buttonSelectionMat;
            if (Input.GetMouseButton (0)) {
                noMaterial.material = buttonSelectedMat;
            }
            if (Input.GetMouseButtonUp (0)) {
                transition = true;
                respawnText.SetActive (false);
                playerGroup.SetActive (true);
                exit?.Invoke ();
                backToCharSelection = true;
            }
        } else {
            noMaterial.material = backgroundMat;
        }


        if (transition) {
            if(backToCharSelection) {
                gameObject.transform.position = new Vector3 (0, 20, -7);
			}

            if (transitionProgress >= 1f) {
                transition = false;
                background.SetActive (false);
                respawnText.SetActive (false);
                if (backToCharSelection) {
                    backToCharSelection = false;
                    gameObject.transform.position = new Vector3 (0, 0, -7);
                }
            }
            else {
                backgroundMat.SetFloat ("Vector1_A8CE1D87", transitionProgress);
                transitionProgress += transitionSpeed * Time.deltaTime;
            }
		}

    }

    public void player1_Won()
	{
        player1_Win_text.gameObject.SetActive (true);
        player2_Win_text.gameObject.SetActive (false);
        startingTransition = true;
        background.SetActive (true);
    }
    public void player2_Won ()
    {
        player1_Win_text.gameObject.SetActive (false);
        player2_Win_text.gameObject.SetActive (true);
        startingTransition = true;
        background.SetActive (true);
    }
}
