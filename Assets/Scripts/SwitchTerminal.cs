/******************************************************************************
Author: Leong Yu Xuan

Name of Class: SwitchTerminal

Description of Class: Class for the terminal used to control the elevator
                        only works if player has collected 4 gems
                        

Date Created: 16/07/2021

Date Modified: 28/7/20201
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchTerminal: MonoBehaviour
{
    ///<summary>
    ///Player object to check gemcount
    /// </summary> 
    public GameObject Player;

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
        //Stop all coroutines upon interact (only text as of now)
        StopAllCoroutines();
        Function();
    }

    ///<summary>
    ///Turns on elevator if criteria are met
    /// </summary>
    public void Function()
    {
        //only trigger if player collected the 4 gems
        if(Player.GetComponent<Player>().gemCount >= 4)
        {
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
        else
        {
            Debug.Log("no");
        }
        
    }
    /////<summary>
    /////Coroutine for dialogue control. "a" is the string to display
    ///// </summary>
    //IEnumerator DialogueControl(string a)
    //{
    //    for (int i = 0; i < 2; ++i)
    //    {
    //        //show assigned dialogue and make it disappear after 5 seconds

    //        if (i == 0)
    //        {
    //            Dialogue.gameObject.SetActive(true);
    //            Dialogue.text = a;

    //        }
    //        else if (i == 1)
    //        {
    //            Dialogue.text = "";
    //            Dialogue.gameObject.SetActive(false);
    //        }

    //        yield return new WaitForSeconds(5f);
    //    }

    //}
}
