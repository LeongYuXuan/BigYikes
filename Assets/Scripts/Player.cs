/******************************************************************************
Author: Elyas Chua-Aziz

Editor: Leong Yu Xuan

Name of Class: Player

Description of Class: This class will control the movement and actions of a 
                        player avatar based on user input.
                        Movement control, Camera control and Interaction control.
                         

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

    /// <summary>
    /// the number of quest items the player has
    /// </summary> 
    public int questcollect;

    ///// <summary>
    ///// Stores settings UI
    ///// </summary>
    //public GameObject SettingsUI;

    ///// <summary>
    ///// Bool to toggle SetActive() of SettingsUI
    ///// </summary>
    //private bool toggle = false;

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
        nextState = "Idle" + "";
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
            //toggle = !toggle;
            //SettingsUI.SetActive(toggle);
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

        //layer mask for the raycast. I still do not know how it works
        int layermask = 1 << LayerMask.NameToLayer("Interactable");

        //do something if the raycast hits something
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitinfo, interactDistance, layermask))
        {    
            //Activate the "Interact" function in obj depending on name
            //I directly copied this from my ASSG1 Script...
            //Highly unoptimised, yikes
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("test");
            }
            /// <summary>
            /// Change text to display obj name
            /// </summary> 
            //objectName.text = hitinfo.transform.name;
        } //reset name to blank if it detects nothing
        //else
        //{
        //    objectName.text = "";
        //}        
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
