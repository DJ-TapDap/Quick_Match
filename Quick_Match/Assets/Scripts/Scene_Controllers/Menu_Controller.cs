using UnityEngine;

public class Menu_Controller : MonoBehaviour
{
    public GameObject menuContent;
    public GameObject optionContent;
    public GameObject charSelectScreen;

    public GameObject playButton;
    public GameObject optionButton;
    public GameObject backButton;

    public LayerMask play_buttonMask;
    public LayerMask option_buttonMask;
    public LayerMask back_buttonMask;
    public Material buttonSelectionMat;
    public Material buttonSelectedMat;
    public Material buttonBackgroundMat;

    bool playGame = false;
    bool exitGame = false;

    public float transitionSpeed;
    float transitionProgress;
    

    public MeshRenderer shaderRenderer;
    Material backgroundMat;

    // Start is called before the first frame update
    void Start()
    {
        menuContent.SetActive (true);
        optionContent.SetActive (false);
        charSelectScreen.SetActive (false);
        backgroundMat = shaderRenderer.material;
        backgroundMat.SetFloat ("Vector1_A8CE1D87", -0.05f);
        transitionProgress = backgroundMat.GetFloat ("Vector1_A8CE1D87");
    }

    // Update is called once per frame
    void Update()
    {
        if (exitGame) {
            print (exitGame);
            if (transitionProgress <= -0.05f) {
                exitGame = false;
                menuContent.SetActive (true);
                optionContent.SetActive (false);
                charSelectScreen.SetActive (false);
                print ("exitGame: " + exitGame);
            } else {
                backgroundMat.SetFloat ("Vector1_A8CE1D87", transitionProgress);
                transitionProgress -= transitionSpeed * Time.deltaTime;
            }
        }


        manageButtons ();

        if (playGame) {
            if (transitionProgress >= 1f) {
                playGame = false;


            } else {
                backgroundMat.SetFloat ("Vector1_A8CE1D87", transitionProgress);
                transitionProgress += transitionSpeed * Time.deltaTime;
            }
        }
    }

    void manageButtons()
	{
        Renderer play_buttonMat = playButton.GetComponent<Renderer> ();
        Renderer option_buttonMat = optionButton.GetComponent<Renderer> ();
        Renderer back_buttonMat = backButton.GetComponent<Renderer> ();
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast (ray, out hit, 19f, play_buttonMask)) {    //detects if the mouse is hovering over the button and turns the button into the appropriate color
            play_buttonMat.material = buttonSelectionMat;
            if (Input.GetMouseButton (0)) {                          //detects if the mouse is holding down on the button
                play_buttonMat.material = buttonSelectedMat;
            }
            if (Input.GetMouseButtonUp (0)) {                        //detects if the player actually selects the button / pushes the button
                //what the button does when clicked
                playGame = true;
                menuContent.SetActive (false);
                charSelectScreen.SetActive (true);

            }
        } else {
            play_buttonMat.material = buttonBackgroundMat;
        }

        if (Physics.Raycast (ray, out hit, 19f, option_buttonMask)) {    //detects if the mouse is hovering over the button and turns the button into the appropriate color
            option_buttonMat.material = buttonSelectionMat;
            if (Input.GetMouseButton (0)) {                          //detects if the mouse is holding down on the button
                option_buttonMat.material = buttonSelectedMat;
            }
            if (Input.GetMouseButtonUp (0)) {                        //detects if the player actually selects the button / pushes the button
                //what the button does when clicked
                menuContent.SetActive (false);
                optionContent.SetActive (true);
                print ("option pressed");
            }
        } else {
            option_buttonMat.material = buttonBackgroundMat;
        }


        if (Physics.Raycast (ray, out hit, 19f, back_buttonMask)) {    //detects if the mouse is hovering over the button and turns the button into the appropriate color
            back_buttonMat.material = buttonSelectionMat;
            if (Input.GetMouseButton (0)) {                          //detects if the mouse is holding down on the button
                back_buttonMat.material = buttonSelectedMat;
                
            }
            if (Input.GetMouseButtonUp (0)) {                        //detects if the player actually selects the button / pushes the button
                //what the button does when clicked
                menuContent.SetActive (true);
                optionContent.SetActive (false);
                print ("back pressed");
            }
        } else {
            back_buttonMat.material = buttonBackgroundMat;
        }
    }

    public void exitGameProcess()
	{
        print ("exitGameProcess");
        exitGame = true;
    }
}
