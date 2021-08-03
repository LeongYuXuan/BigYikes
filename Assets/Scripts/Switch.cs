/******************************************************************************
Author: Leong Yu Xuan

Name of Class: Switch

Description of Class: Class for the regular switches. 
                        They target the "Switch Interact" Part of the target obj
                        
                        

Date Created: 02/08/2021
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    /// <summary>
    /// The objects that the switch would affect
    /// </summary>
    public GameObject[] ObjectArray;
     

    //go through the array and trigger all of the switchInteract functions
    public void Interact()
    {
        for(int i = 0; i < ObjectArray.Length; ++i)
        {

            string objtag = ObjectArray[i].transform.tag;
            if (objtag == "Door")
            {
                ObjectArray[i].transform.GetComponent<Door>().switchInteract();
            }
            else if (objtag == "Toggle")
            {
                //check if obj has the component before executing it
                if (ObjectArray[i].transform.GetComponent<ToggleCube>() != null)
                {
                    ObjectArray[i].transform.GetComponent<ToggleCube>().switchInteract();
                }
                else if (ObjectArray[i].transform.GetComponent<TimedPlatform>() != null)
                {
                    ObjectArray[i].transform.GetComponent<TimedPlatform>().switchInteract();
                }
                else
                {
                    continue;
                }
            }
        }
    }
}
