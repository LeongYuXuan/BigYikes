/******************************************************************************
Author: Leong Yu Xuan

Name of Class: Restore

Description of Class: Script to execute when player is defeated
                        

Date Created: 14/07/2021
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restore : MonoBehaviour
{
    /// <summary>
    /// The panel that shows up upon player defeat
    /// </summary>
    public GameObject defeatPanel;

    /// <summary>
    /// the active player from game mananger, for conviniece
    /// </summary>
    private Player activePlayer;

    private void Awake()
    {
        activePlayer = GameManager.instance.activePlayer;
    }

    /// <summary>
    /// Code to execute to being player back to playable condition
    /// </summary>
    public void RecoverfromLobby()
    {
        //find the spawn area for the player
        SpawnArea playerSpot = FindObjectOfType<SpawnArea>();
        //assign rotation and position for convinience
        Vector3 spawnPostion = playerSpot.transform.position;
        Quaternion spawnRotation = playerSpot.transform.rotation;

        ///reset the position and specified player stats.
        activePlayer.health = 10;
        //activePlayer.foodCount = 5;
        activePlayer.CanMove = true;
        activePlayer.transform.position = spawnPostion;
        activePlayer.transform.rotation = spawnRotation;

        //reset the text to match
        //set the text  values to their respective attributes
        GameManager.instance.staminaText.text = "Stamina: " + activePlayer.stamina.ToString();
        GameManager.instance.healthText.text = "Health: " + activePlayer.health.ToString();
        //Gamemanager.instance.foodText.text = "Food: " + activePlayer.foodCount.ToString();

        //set defeat panel to inactive
        defeatPanel.SetActive(false);


    }
}
