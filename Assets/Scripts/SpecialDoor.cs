/******************************************************************************
Author: Leong Yu Xuan

Name of Class: SpecialDoor

Description of Class: This class is a varient of the door script

Date Created: 09/06/2021
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpecialDoor : MonoBehaviour
{
    /// <summary>
    /// The state of the door (false means it is closed)
    /// </summary>
    private bool doorTrigger = false;

    /// <summary>
    /// to prevent the switch interact from executing more than once
    /// </summary>
    private bool triggerOnce = true;

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
        if (triggerOnce)
        {
            locked = false;
            GetComponent<Animation>().Play(DoorClips[0].name);
            GetComponent<BoxCollider>().enabled = false;
            triggerOnce = false;
        }      
    }
}
