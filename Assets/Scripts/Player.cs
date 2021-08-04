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

    /// <summary>
    /// Sprint speed mutiplier
    /// </summary>
    [SerializeField]
    private float sprintMultiply = 2;

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
    /// The player's stamina
    /// Number represents how long player can run, stangely
    /// </summary>
    public float stamina = 5;

    /// <summary>
    /// The player's stamina cap, used for the staminaManager
    /// </summary>
    private float staminaCap;

    /// <summary>
    /// Player Jump Power
    /// </summary>
    public float jumpPower;

    ///<summary>
    ///The boolean to control if player can jump
    /// </summary>
    [SerializeField]
    private bool canJump = true;

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
    /// Bool used to control if player can move (include move camera) 
    /// </summary>
    public bool CanMove = true;

    /// <summary>
    /// The camera attached to the player model.
    /// Should be dragged in from Inspector.
    /// </summary>
    [SerializeField]
    private Camera playerCamera;

    [SerializeField]
    private string currentState;

    private string nextState;

    /// <summary>
    /// Float used for raycasting to determine distance of cast 
    /// </summary>
    [SerializeField] private float interactDistance;

    /// <summary>
    /// bool to make sure start function only works once
    /// </summary>
    public bool resetStart = false;

    public void resetvalue()
    {
        if (!resetStart)
        {
            gemCount = 0;
            canStab = false;
            health = 10;
            resetStart = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //set the state to idle
        nextState = "Idle" + "";

        //hide GemCount Text upon starting
        GameManager.instance.gemCountText.text = "";

        //set the text  values to their respective attributes
        GameManager.instance.staminaText.text += " " + stamina.ToString();
        GameManager.instance.healthText.text += " " + health.ToString();

        //set the stamina cap to the respective value
        staminaCap = stamina;

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

        //Jump function
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            Vector3 jumpForce = transform.up * jumpPower;
            GetComponent<Rigidbody>().AddForce(jumpForce, ForceMode.Impulse);
            canJump = false;
        }

        
        
    }

    //reset jump. Uses a raycast or something
    void OnCollisionEnter(Collision collision)
    {
        ////stores what was hit
        //RaycastHit CheckGround;

        ////layer mask for the raycast. Only detect under this
        //int layermask = 1 << LayerMask.NameToLayer("Ground");

        ////do the following if the raycast hits something
        //if (Physics.Raycast(gameObject.transform.position,-gameObject.transform.up, out CheckGround, 1.01f, layermask))
        //{
        //    canJump = true;
        //}

        canJump = true;
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
            //apparently child classes would had been much better, oh dear...
            if (Input.GetMouseButtonDown(0))
            {
                if (objTag == "Gem")
                {
                    hitinfo.transform.GetComponent<Collectable>().Interact();
                }
                else if (objTag == "FinalSwitch")
                {
                    hitinfo.transform.GetComponent<SwitchTerminal>().Interact();
                }
                else if (objTag == "Switch")
                {
                    if (hitinfo.transform.GetComponent<Switch>() != null)
                    {
                        hitinfo.transform.GetComponent<Switch>().Interact();
                    }
                    else if(hitinfo.transform.GetComponent<RotatePost>() != null)
                    {
                        hitinfo.transform.GetComponent<RotatePost>().Interact();
                    }
                    
                }
                else if (objTag == "Weapon")
                {
                    hitinfo.transform.GetComponent<Collectable>().Interact();
                }
                else if (objTag == "Enemy" && canStab) // can ony interact if have weapon
                {
                    if (hitinfo.transform.GetComponent<Swarmer>() != null) 
                    {
                        hitinfo.transform.GetComponent<Swarmer>().HealthManager(-atk);
                    }
                    else
                    {
                        hitinfo.transform.GetComponent<WeakWall>().HealthManager(-atk);
                    } 
                }
                else if (objTag == "Door")
                {
                    hitinfo.transform.GetComponent<Door>().Interact();
                }

            }

            // Change text to display obj name
            GameManager.instance.objectName.text = hitinfo.transform.name;
        } //reset name to blank if it detects nothing
        else
        {
            GameManager.instance.objectName.text = "";
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
        GameManager.instance.healthText.text = "Health: " + health.ToString();

        //lock player movement if health = 0
        if (health <= 0)
        {
            CanMove = false;
            GameManager.instance.healthText.text = "Ded";
        }

    }

    /// <summary>
    /// The Function that manages whether player can sprint or not
    /// Also drains and recharges stamina, as 
    /// </summary>
    /// <returns>
    /// Returns sprintmultiply value if stamina is above 0 and leftshift is held
    /// Otherwise, return 1f
    /// </returns>
    private float StaminaManager()
    {
        //trigger upon left shift press
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //stop the regen coroutine
            StopCoroutine("StamRegen");

            //return sprintmultiply if stamina > 0
            if(stamina > 0)
            {
                stamina -= Time.deltaTime;
                GameManager.instance.staminaText.text = "Stamina: " + stamina.ToString("n2");
                return sprintMultiply;
            }
            //set stamina back to 0 if go past
            else if (stamina < 0)
            {
                stamina = 0;
                GameManager.instance.staminaText.text = "Stamina: " + stamina.ToString();
            }       
        }
        //trigger stamina regen upon releasing shift key
        if (Input.GetKeyUp(KeyCode.LeftShift) && stamina < staminaCap)
        {
            StartCoroutine("StamRegen");
        }   
        return 1;
    }

    private IEnumerator StamRegen()
    {
        //wait 4 seconds before regening
        yield return new WaitForSeconds(4f);
        while (stamina < staminaCap)
        {
            if (currentState == "Idle")
            {
                stamina += 0.01f;
                //Debug.Log("Fast");
            }
            else
            {
                stamina += Time.deltaTime;
                //Debug.Log("Norm");
            }


            GameManager.instance.staminaText.text = "Stamina: " + stamina.ToString("n2");
            yield return new WaitForSeconds(Time.deltaTime);
        }

        if (stamina > staminaCap)
        {
            stamina = staminaCap;
            GameManager.instance.staminaText.text = "Stamina: " + stamina.ToString();
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
            if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 || canJump == false)
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
            if (!CheckMovement() && canJump == true)
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
                //Move the player or something
                //Stamina manager returns the sprintMultipy float if shift is pressed
                movementVector *= ((moveSpeed * StaminaManager()) * Time.deltaTime);
                
                transform.position += movementVector;
                //script for sprinting

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
