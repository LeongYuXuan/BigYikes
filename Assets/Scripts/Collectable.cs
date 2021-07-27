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

    /// <summary>
    /// Player object to affect 
    /// </summary>
    public GameObject Player;

    /// <summary>
    /// Text to affecr
    /// </summary>
    public Text gemCountText;



    public void Interact()
    {
        if (isMachete)
        {
            Player.GetComponent<Player>().canStab = true;
            Debug.Log("Weapon get");
        }
        else
        {
            Player.GetComponent<Player>().gemCount += 1;
            gemCountText.text = "Gem Count: " + Player.GetComponent<Player>().gemCount;

        }

        //set item to be invisable so Dialogue still gets to play
        GetComponent<MeshCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
    }


}
