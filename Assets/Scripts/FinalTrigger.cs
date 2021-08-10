/******************************************************************************
Author: Leong Yu Xuan

Name of Class: GameManager

Description of Class: The final trigger for the game. 
                        Locks player movement and reveals game finish canvas.

Date Created: 0/06/2021
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalTrigger : MonoBehaviour
{
    public GameObject EndCanvas;

    private void OnTriggerEnter(Collider collision)
    {
        //GameManager.instance.activePlayer.CanMove = false;
        GameManager.instance.endPanel.SetActive(true);

    }
}
