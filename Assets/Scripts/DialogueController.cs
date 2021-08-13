/******************************************************************************
Author: Leong Yu Xuan

Name of Class: DialogueController

Description of Class: This controls the dialogue that shows or something

Date Created: 12/06/2021
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    /// <summary>
    /// The innate thing to say, if any
    /// </summary>
    //public string[] thingsToSay;

    /// <summary>
    /// Function to display text on screen
    /// </summary>
    /// <param name="dialogue"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public IEnumerator speak(string[] dialogue, float time)
    {
        for(int i = 0; i <= dialogue.Length; ++i)
        {
            if (dialogue[i] != null)
            {
                GameManager.instance.dialogue.text = dialogue[i];
            }
            else
            {
                GameManager.instance.dialogue.text = "";
            }
            
            yield return new WaitForSeconds(time);
        }
        
    }
}
