/******************************************************************************
Author: Leong Yu Xuan

Name of Class: Door

Description of Class: This class controls the opening/closing of the door, depending on bool
                        Locked by a bool as well

Date Created: 09/06/2021
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Door : MonoBehaviour
{
    private bool doorTrigger = false;

    public bool locked = false;

    [SerializeField]
    private AnimationClip[] DoorClips;

    public void Interact()
    {
        if (!locked)
        {
            if (!doorTrigger)
            {
                GetComponent<Animation>().Play(DoorClips[0].name);
                
            }
            else
            {
                GetComponent<Animation>().Play(DoorClips[1].name);
                
            }
            doorTrigger = !doorTrigger;
        }
        else
        {
            Debug.Log("hmm");
        }
        
    }
}
