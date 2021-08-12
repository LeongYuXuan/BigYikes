/******************************************************************************
Author: Leong Yu Xuan

Name of Class: SwitchTerminal

Description of Class: Class for the terminal used to control the elevator
                        only works if player has collected 4 gems
                        

Date Created: 28/7/2021
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchTerminal: MonoBehaviour
{
    /// <summary>
    /// dialogue for if player is unable to activate the elevator
    /// </summary>
    public string[] noActivate;

    /// <summary>
    /// dialogue for if player has gems to activate
    /// but have not talked to the ghost yet
    /// </summary>
    public string[] forget;

    /// <summary>
    /// dialogue when player activates elevator
    /// </summary>
    public string[] Activate;

    ///<summary>
    ///Questghost object to check a boolean
    /// </summary> 
    public QuestGhost questGhost;

    ///<summary>
    ///Obj that is to be triggered upon switch acitivation
    /// </summary> 
    public GameObject Target;

    ///<summary>
    ///Control elevator going up or down
    /// </summary>
    private bool ToggleElevator = true;

    ///<summary>
    ///Functions to execute upon interact
    /// </summary>
    public void Interact()
    {
        //Stop all coroutines upon interact
        StopAllCoroutines();
        //(re)set questman obj
        questGhost = FindObjectOfType<QuestGhost>();
        Function();
    }

    ///<summary>
    ///Turns on elevator if criteria are met
    /// </summary>
    public void Function()
    {
        //only trigger if player collected the 4 gems AND talked to Ghost
        if(GameManager.instance.activePlayer.GetComponent<Player>().gemCount >= 4 && questGhost.grandQuestFinish)
        {
            StartCoroutine(speak(Activate, 4f));
            if (ToggleElevator)
            {
                Debug.Log("Going up");
                Target.GetComponent<Animation>().Play();
            }
            else
            {
                Debug.Log("Going down");
            }
            ToggleElevator = !ToggleElevator;
        }
        else if(GameManager.instance.activePlayer.GetComponent<Player>().gemCount >= 4 && !questGhost.grandQuestFinish)
        {
            StartCoroutine(speak(forget, 4f));
        }
        else
        {
            StartCoroutine(speak(noActivate, 4f));
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
