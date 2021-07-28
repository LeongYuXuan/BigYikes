/******************************************************************************
Author: Elyas Chua-Aziz

Editor: Leong Yu Xuan

Name of Class: Player

Description of Class: This class will control the movement and actions of a 
                        player avatar based on user input.
                        Movement control, Camera control and Interaction control.
                        Modified to suit IP needs
                         

Date Created: 09/06/2021
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    /// <summary>
    /// The distance this player will travel per second.
    /// </summary>
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float rotationSpeed;

    ///<summary>
    ///Player attack power
    /// </summary>
    private int atk = 1;

    ///<summary>
    ///Player health
    /// </summary>
    public int health = 10;

    /// <summary>
    /// Player health
    /// </summary>
    public int stamina = 100;

    /// <summary>
    /// Player Jump Power
    /// </summary>
    public float jumpPower;

    ///<summary>
    /// Bool to control whether player has weapon
    /// </summary>
    public bool canStab = false;

    ///<summary>
    ///int to check how many gems player has
    /// </summary>
    public int gemCount;

    /// <summary>
    /// Stores settings UI
    /// </summary>
    public GameObject SettingsUI;

    /// <summary>
    /// Bool to toggle SetActive() of SettingsUI
    /// </summary>
    private bool toggle = false;

    /// <summary>
    /// Stores Text Name highlight
    /// </summary>
    public Text objectName;

    /// <summary>
    /// Stores Gem Count text
    /// </summary>
    public Text gemCountText;

    /// <summary>
    /// Stores Stamina text
    /// </summary>
    public Text staminaText;

    /// <summary>
    /// Stores health text
    /// </summary>
    public Text healthText;

    /// <summary>
    /// Bool used to control if player can move (include move camera) 
    /// </summary>
    public bool CanMove = true;

    /// <summary>
    /// The camera attached to the player model.
    /// Should be dragged in from Inspector.
    /// </summary>
    [SerializeField]
    private Camera playerCamera;

    private string currentState;

    private string nextState;

    /// <summary>
    /// Float used for raycasting to determine distance of cast 
    /// </summary>
    [SerializeField] private float interactDistance;

    // Start is called before the first frame update
    void Start()
    {
        //set the state to idle
        nextState = "Idle" + "";

        //hide GemCount Text upon starting
        gemCountText.text = "";

        //set the text  values to their respective attributes
        staminaText.text += " " + stamina.ToString();

        healthText.text += " " + health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (nextState != currentState)
        {
            SwitchState();
        }
        CheckRotation();
        InteractRaycast();
        MenuTrigger();
    }

    /// <summary>
    /// Function that brings up the settings menu 
    /// </summary>
    private void MenuTrigger()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            toggle = !toggle;
            SettingsUI.SetActive(toggle);
            CanMove = !CanMove;
        }
    }

    ///<summary>
    ///function for raycast used for interactions
    /// </summary>
    private void InteractRaycast()
    {
        //debug line for raycast
        Debug.DrawLine(playerCamera.transform.position,
            playerCamera.transform.position + playerCamera.transform.forward * interactDistance);
        
        //variable that stores what raycast has hit
        RaycastHit hitinfo;

        //layer mask for the raycast. Only detect under this
        int layermask = 1 << LayerMask.NameToLayer("Interactable");

        string objTag;

        //do something if the raycast hits something
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitinfo, interactDistance, layermask))
        {
            objTag = hitinfo.transform.tag;
            //Activate the "Interact" function in obj depending on tag
            //Highly unoptimised, yikes
            if (Input.GetMouseButtonDown(0))
            {
                if (objTag == "Gem")
                {
                    hitinfo.transform.GetComponent<Collectable>().Interact();
                }
                else if (objTag == "Switch")
                {
                    Debug.Log(objTag);
                }
                else if (objTag == "Weapon")
                {
                    hitinfo.transform.GetComponent<Collectable>().Interact();
                }
                else if (objTag == "Enemy" && canStab) // can ony interact if have weapon
                {
                    hitinfo.transform.GetComponent<Swarmer>().HealthManager(-atk);
                }

            }
            
            // Change text to display obj name
            objectName.text = hitinfo.transform.name;
        } //reset name to blank if it detects nothing
        else
        {
            objectName.text = "";
        }        
    }

    /// <summary>
    /// Function to trigger on anything to do with the player health
    /// </summary>
    /// <param name="">How much health to add. Go negative to subtract</param>
    public void HealthManager(int Num)
    {
        //change health value based on that
        health += Num;
        //change the health text accordingly
        healthText.text = "Health: " + health.ToString();

        //lock player movement if health = 0
        if (health <= 0)
        {
            CanMove = false;
            healthText.text = "Ded";
        }

    }
    
    /// <summary>
    /// Sets the current state of the player
    /// and starts the correct coroutine.
    /// </summary>
    private void SwitchState()
    {
        StopCoroutine(currentState);
        currentState = nextState;
        StartCoroutine(currentState);
    }


    /// <summary>
    /// Coroutine for the idle state 
    /// </summary>
    private IEnumerator Idle()
    {
        while(currentState == "Idle")
        {
            //Revealed in CA4 ans in wk 11 Logic error
            if(Input.GetAxis("Horizontal") !!= 0 || Input.GetAxis("Vertical") != 0)
            {
                nextState = "Moving";
                
            }
            yield return null;
        }
    }
    /// <summary>
    /// Coroutine for the moving state
    /// </summary>
    private IEnumerator Moving()
    {
        while (currentState == "Moving")
        {
            if (!CheckMovement())
            {
                nextState = "Idle";
                
                
            }
            yield return null;
        }
        
    }

    /// <summary>
    /// Checks and handles camera movement
    /// </summary>
    private void CheckRotation()
    {
        
        Vector3 playerRotation = transform.rotation.eulerAngles;

        if (CanMove)
        {
            playerRotation.y += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

            transform.rotation = Quaternion.Euler(playerRotation);

            //changing from += to -= makes the camera move as intended
            Vector3 cameraRotation = playerCamera.transform.rotation.eulerAngles;
            cameraRotation.x -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            playerCamera.transform.rotation = Quaternion.Euler(cameraRotation);
        }
        
    }

    /// <summary>
    /// Checks and handles movement of the player
    /// </summary>
    /// <returns>True if user input is detected and player is moved.</returns>
    private bool CheckMovement()
    {

        //move left and right
        Vector3 xMovement = transform.right * Input.GetAxis("Horizontal");
        //move forward and back
        Vector3 zMovement = transform.forward * Input.GetAxis("Vertical");

        Vector3 movementVector = xMovement + zMovement;

        //HAs something to do with movement
        if (CanMove)
        {
            if (movementVector.sqrMagnitude > 0)
            {
                movementVector *= (moveSpeed * Time.deltaTime);

                transform.position += movementVector;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
        

    }
}
