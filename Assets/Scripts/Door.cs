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
    /// <summary>
    /// The state of the door (false means it is closed)
    /// </summary>
    private bool doorTrigger = false;

    /// <summary>
    /// Door does nothing if true
    /// </summary>
    public bool locked = false;

    /// <summary>
    /// The animation clips to play depending on action
    /// </summary>
    [SerializeField]
    private AnimationClip[] DoorClips;

    /// <summary>
    /// What happens upon being interacted with
    /// </summary>
    public void Interact()
    {
        DoorFunction();
    }

    /// <summary>
    /// Play the door animations depending on the state. Do nothing if locked
    /// </summary>
    private void DoorFunction()
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

    /// <summary>
    /// The script to execute when interacted with by switch. Unlocks door
    /// </summary>
    public void switchInteract()
    {
        locked = false;
        GetComponent<Animation>().Play(DoorClips[0].name);
        GetComponent<BoxCollider>().enabled= false;
    }
}
