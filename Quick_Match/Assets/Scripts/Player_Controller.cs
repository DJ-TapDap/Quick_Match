using UnityEngine;
using System;

public class Player_Controller : MonoBehaviour
{
    //player variables
    public int playerNum = 1;
    public int playerClass = 0;

    //movement variables
    public float speed = 6;                         //public movement speed of the character
    public float jumpHeight = 3;                    //public jump height of the character
    public CharacterController playerCharacter;     //allows us to access the Character Controller commands for the player character
    public Transform playerBody;

    //gravity variables
    public Transform PropulsionGroundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;
    bool isGrounded;
    public float gravitationalForce = -30f;      //gravitational force of the world
    Vector3 gravitationalVelocity;          //instantiated gravitational velocity of the player

    //utility variables
    KeyCode jumpInput;
    KeyCode leftInput;
    KeyCode rightInput;
    KeyCode attackInput;
    KeyCode ExtendInput;
    public float smoothMoveTime = .1f;
    float smoothInputMagnitude;
    float smoothMoveVelocity;
    public event Action Player_Death;
    public event Action updateHealth;

    //Animation variables
    [SerializeField] private Animator animator = null;
    private static readonly int hashAttack = Animator.StringToHash ("Slice");
    private static readonly int hashExtend = Animator.StringToHash ("Extend");

    //combat variables
    float health = 100f;
    public LayerMask hitMask;
    public GameObject deathAnimBeam;
    public Transform deathLocation;
    public bool currentlyAttacking = false;
    public GameObject playerVisible;


    void Start ()
	{
        inputManager (playerNum);
        deathAnimBeam.SetActive (false);
        if (playerClass == 1) {
            speed += 2f;
		}

    }


	// Update is called once per frame
	void Update ()
    {
        manageGravity ();
        manageMovement ();
        manageCombat ();
        
    }

    //Combat
    public void manageCombat()
	{
        if (health <= 0 && !currentlyAttacking && playerClass != -1) {
            deathAnimBeam.SetActive (true);
            Death_Animation deathAnimation = deathAnimBeam.GetComponent<Death_Animation> ();

            deathLocation.position = new Vector3 (transform.position.x, -8, 4);

            deathAnimation.doDeath ();

			Player_Death?.Invoke ();

            health = 100f;
            gameObject.SetActive (false);
            gameObject.transform.position = new Vector3 (0, 2, 0);
            
        }

        else if (!currentlyAttacking && playerClass != -1) {
            normal ();
            special ();
		}
        
	}
    public void normal()
	{
        if (Input.GetKeyDown(attackInput) && playerClass == 0) {
            RaycastHit hit;
            animator.SetTrigger (hashAttack);
            currentlyAttacking = true;

            if (Physics.Raycast (playerBody.position, playerBody.forward, out hit, 2.5f, hitMask)) {
                Player_Controller target = hit.transform.GetComponent<Player_Controller> ();
                target.takeDmg (20);
            }
		}

        else if (Input.GetKeyDown (attackInput) && playerClass == 1) {
            RaycastHit hit;
            animator.SetTrigger (hashAttack);
            currentlyAttacking = true;

            if (Physics.Raycast (playerBody.position, playerBody.forward, out hit, 2.5f, hitMask)) {
                Player_Controller target = hit.transform.GetComponent<Player_Controller> ();
                target.takeDmg (10);
            }
        }
    }
    public void special()
	{
        if (Input.GetKeyDown (ExtendInput) && playerClass == 0) {
            RaycastHit hit;
            animator.SetTrigger (hashExtend);
            currentlyAttacking = true;

            if (Physics.Raycast (playerBody.position, playerBody.forward, out hit, 7.6f, hitMask)) {
                Player_Controller target = hit.transform.GetComponent<Player_Controller> ();
                target.takeDmg (5);
            }
        }

        else if (Input.GetKeyDown (ExtendInput) && playerClass == 1) {
            animator.SetTrigger (hashExtend);
            currentlyAttacking = true;
        }

    }
    public void takeDmg (float dmg)
    {
        health -= dmg;
        updateHealth?.Invoke ();
        
    }
    public void canAttack()
	{
        currentlyAttacking = false;
	}
    public void setHealth(float healthAmtSet)
	{
        health = healthAmtSet;
        updateHealth?.Invoke (); 
	}
    public int getHealth()
	{
        return (int)health;
	}

    //Movement and Gravity
    void manageGravity ()
    {
        isGrounded = Physics.CheckSphere (PropulsionGroundCheck.position, groundDistance, groundMask);    //creates an invisible shere with radius 0.1 that checks to see if the character collides with the ground

        if (isGrounded && gravitationalVelocity.y < 0)    //checks to see if the player is on the ground and if the gravitational velocity is less than 0
        {
            gravitationalVelocity.y = -4f;     //sets gravitational velocity to 0, because the player is on the ground
        }

        gravitationalVelocity.y += gravitationalForce * Time.deltaTime;     //creates gravitational vector based on eqn: gravitational velocity = gravitational force x time^2
        playerCharacter.Move (gravitationalVelocity * Time.deltaTime);         //moves the player character down by the gravitational velocity
    }
    void manageMovement ()
    {

        //determines which buttons player is pressing
        float viewDirection = getRawHorizontalInput ();
        smoothInputMagnitude = Mathf.SmoothDamp (smoothInputMagnitude, getRawHorizontalInput (), ref smoothMoveVelocity, smoothMoveTime); //gets smooth input for horizontal movement


        Vector3 velocity = speed * (transform.right * smoothInputMagnitude);   //determines velocity vector or movement vector of the player
        playerCharacter.Move (velocity * Time.deltaTime);           //moves the player by the velocity vector
        
        if (viewDirection != 0) {
            playerBody.localRotation = Quaternion.Euler(0, 90 * viewDirection, 0);
		}
        //jumping
        if (isGrounded && Input.GetKeyDown (jumpInput))                  //checks to see if the player is on the ground and if the player is pressing the space bar
        {
            gravitationalVelocity.y = Mathf.Sqrt (-2 * gravitationalForce * jumpHeight);     //Determines the initial velocity required to reach the jump height. this is derived from: v^2 = v0^2 + 2a(x-x0)
        }
    }

    //utility functions
    void inputManager(int playerNum)
	{
        if (playerNum == 1) {
            jumpInput = KeyCode.W;
            rightInput = KeyCode.D;
            leftInput = KeyCode.A;
            attackInput = KeyCode.V;
            ExtendInput = KeyCode.B;
        }
        else if (playerNum == 2) {
            jumpInput = KeyCode.UpArrow;
            rightInput = KeyCode.RightArrow;
            leftInput = KeyCode.LeftArrow;
            attackInput = KeyCode.RightShift;
            ExtendInput = KeyCode.Return;
        }
	}
    float getRawHorizontalInput()
	{
        if (Input.GetKey (rightInput)) { return 1; }
        else if (Input.GetKey (leftInput)) { return -1; }
        return 0;
	}
}
