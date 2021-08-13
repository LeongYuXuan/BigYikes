/******************************************************************************
Author: Leong Yu Xuan

Name of Class: QuestGhost

Description of Class: This class controls the quest system for the game. 
                        Convoluted boolean tree...
                         

Date Created: 12/8/2021
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGhost : MonoBehaviour
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
    /// array of walls to remove
    /// </summary>
    public GameObject[] wallArray;

    /// <summary>
    /// door to affect
    /// </summary>
    public Door FinalDoor;


    /// <summary>
    /// prevent player from doing things early
    /// </summary>
    [SerializeField]
    private bool grandQuestStart = false;

    /// <summary>
    /// true once player gets 1st gem and weapon
    /// aka canStab = true and gemcount = 1
    /// </summary>
    [SerializeField]
    private bool quest1Finish = false;

    /// <summary>
    /// true once player gets 2 other gems
    /// aka gem count = 3
    /// </summary>
    [SerializeField]
    private bool quest2Finish = false;

    /// <summary>
    /// true once player defeats boss
    /// </summary>
    [SerializeField]
    private bool quest3Finish = false;

    /// <summary>
    /// true once all quest complete
    /// </summary>
    public bool grandQuestFinish = false;


    /// <summary>
    /// bunch of string arrays for dialogue
    /// questDialogue(no.) would contain "quest complete" text for prev quest
    /// </summary>
    public string[] startQuestDialogue;

    public string[] specialRepeatDialogue1;

    public string[] repeatDialogue1;

    public string[] quest2Dialogue;

    public string[] repeatDialogue2;

    public string[] quest3Dialogue;

    public string[] repeatDialogue3;

    public string[] grandfinishDialogue;

    public string[] grandrepeatDialogue;


    // Start is called before the first frame update
    void Start()
    {
        //talk = gameObject.GetComponent<DialogueController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        //stop the previous text coroutine
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        //(re)assign the player
        player = GameManager.instance.activePlayer;

        //execute quest code
        quest();
    }

    private void quest()
    {
        //start the quest chain
        if (!grandQuestStart)
        {
            coroutine = speak(startQuestDialogue, 3f);
            StartCoroutine(coroutine);
            wallArray[0].SetActive(false);
            //deactiate the first wall
            grandQuestStart = true;
        }
        else if (!quest1Finish)
        {
            //complete criteria
            if (player.gemCount == 1 && player.canStab)
            {
                coroutine = speak(quest2Dialogue, 3f);
                StartCoroutine(coroutine);
                //deactivate all other walls.
                for(int i = 1; i <wallArray.Length; ++i)
                {
                    wallArray[i].SetActive(false);
                }
                quest1Finish = true;
            }
            //special dialogue if only 1 collected
            else if (player.gemCount == 1 || player.canStab)
            {
                coroutine = speak(specialRepeatDialogue1, 3f);
                StartCoroutine(coroutine);
            }
            //dialogue to repeat
            else
            {
                coroutine = speak(repeatDialogue1, 3f);
                StartCoroutine(coroutine);
            }
        }
        else if (!quest2Finish)
        {
            //complete criteria
            if (player.gemCount == 3)
            {
                coroutine = speak(quest3Dialogue, 3f);
                FinalDoor.locked = false;
                StartCoroutine(coroutine);
                quest2Finish = true;
            }
            else if (player.gemCount < 3)
            {
                coroutine = speak(repeatDialogue2, 3f);
                StartCoroutine(coroutine);
            }
        }
        else if (!quest3Finish)
        {
            if (player.gemCount == 4)
            {
                coroutine = speak(grandfinishDialogue, 3f);
                StartCoroutine(coroutine);
                quest3Finish = true;
                grandQuestFinish = true;
            }
            else if (player.gemCount < 4)
            {
                coroutine = speak(repeatDialogue3, 3f);
                StartCoroutine(coroutine);
            }
        }
        else if (grandQuestFinish)
        {
            coroutine = speak(grandrepeatDialogue, 3f);
            StartCoroutine(coroutine);
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
