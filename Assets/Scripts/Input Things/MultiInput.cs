/******************************************************************************
Author: Leong Yu Xuan

Name of Class: MultiInput

Description of Class: Class that does something if all input objs in array return true
                        currently only works with pressure plates...
                        
                        

Date Created: 06/08/2021
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiInput : MonoBehaviour
{
    /// <summary>
    /// the input devices to check
    /// </summary>
    public GameObject[] inputArray;

    /// <summary>
    /// the things to trigger if self is activated
    /// </summary>
    public GameObject[] activateArray;

    /// <summary>
    /// used to activate self if it matches no. of input devices in array 
    /// </summary>
    private int trueCount;

    /// <summary>
    /// to make sure the thing triggers only once. Wonder if it can be nested...
    /// </summary>
    private bool isActivate = false;

    // Update is called once per frame
    void Update()
    {
        //Stop checking when the manager is already acitivated
        if (!isActivate)
        {
            CheckAllTrue();
        }
        
    }

    private void CheckAllTrue()
    {
        for (int i = 0; i < inputArray.Length; ++i)
        {
            var boolCheck = inputArray[i].GetComponent<PressurePlate>().isActivate;
            // Add 1 to trueCount if isActivate(boolcheck) is true
            if (boolCheck)
            {
                trueCount += 1;
            }
        }

        //if all obj are true, execute the following
        if (trueCount == inputArray.Length)
        {
            //code to execute
            Debug.Log("On!");
            isActivate = true;
            onActivate();
            
        }
        //reset the true count if not
        else
        {
            trueCount = 0;
        }
    }

    /// <summary>
    /// trigger "switchInteract" of items in activateArray
    /// copied from "switch" code
    /// </summary>
    private void onActivate()
    {
        for(int j = 0; j < activateArray.Length; ++j)
        {
            string objtag = activateArray[j].transform.tag;
            if (objtag == "Door")
            {
                activateArray[j].transform.GetComponent<Door>().switchInteract();
            }
            else if (objtag == "Toggle")
            {
                //check if obj has the component before executing it
                if (activateArray[j].transform.GetComponent<ToggleCube>() != null)
                {
                    activateArray[j].transform.GetComponent<ToggleCube>().switchInteract();
                }
                else if (activateArray[j].transform.GetComponent<TimedPlatform>() != null)
                {
                    activateArray[j].transform.GetComponent<TimedPlatform>().switchInteract();
                }
                else
                {
                    continue;
                }
            }
            else if (objtag == "Laser")
            {
                activateArray[j].transform.GetComponent<LaserPost>().switchInteract();
            }
        }
    }
}
