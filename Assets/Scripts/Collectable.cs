/******************************************************************************
Author: Leong Yu Xuan

Name of Class: Collectable

Description of Class: This class will controls the behaviour of the two collectables:
                        Gem and Machete
                        They are under same class as Machete itself would be 
                        unremarkable.
                         

Date Created: 09/06/2021
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour
{
    /// <summary>
    /// Bool to control whether to execute machete script
    /// </summary>

    public bool isMachete = false;


    public void Interact()
    {
        if (isMachete)
        {
            GameManager.instance.activePlayer.GetComponent<Player>().canStab = true;
            Debug.Log("Weapon get");
        }
        else
        {
            GameManager.instance.activePlayer.GetComponent<Player>().gemCount += 1;
            GameManager.instance.gemCountText.text = "Gem Count: " + 
            GameManager.instance.activePlayer.GetComponent<Player>().gemCount;

        }

        //set item to be invisable so Dialogue still gets to play
        GetComponent<MeshCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
    }


}
