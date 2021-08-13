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
    /// to call the coroutine by a var to keep track or something
    /// </summary>
    private IEnumerator coroutine;

    /// <summary>
    /// the game manager's player, for cleaner code
    /// </summary>
    private Player player;

    /// <summary>
    /// door dialogue for when door is locked
    /// </summary>
    public string[] lockedDialogue;

    /// <summary>
    /// door dialogue for when door is unlocked by a switch
    /// </summary>
    public string[] unlockDialogue;

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
        //stop the previous text coroutine
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        //(re)assign the player
        player = GameManager.instance.activePlayer;
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
            coroutine = (speak(lockedDialogue, 5f));
            StartCoroutine(coroutine);
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

            coroutine = (speak(unlockDialogue, 5f));
            StartCoroutine(coroutine);

            triggerOnce = false;
        }      
    }

    /// <summary>
    /// Function to display text on screen
    /// </summary>
    /// <param name="dialogue"> the string array to use for dialogue</param>
    /// <param name="time"> time between dialogue</param>
    /// <returns></returns>
    public IEnumerator speak(string[] dialogue, float time)
    {
        //go through the string array and display the text
        for (int i = 0; i <= dialogue.Length; ++i)
        {
            if (i < dialogue.Length)
            {
                GameManager.instance.dialogue.text = dialogue[i];
            }
            //final i value will be outside array index
            //used to set dialogue back to blank
            else if (i == dialogue.Length)
            {
                GameManager.instance.dialogue.text = "";
            }
            //time between the dialogue
            yield return new WaitForSeconds(time);
        }

    }
}
