/******************************************************************************
Author: Leong Yu Xuan

Name of Class: GameManager

Description of Class: This is the game manager script, used to keep track of UI things

Date Created: 09/06/2021
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// stores the Gamemanager
    /// </summary>
    public static GameManager instance;

    /// <summary>
    /// Player prefab to look out for
    /// </summary>
    public GameObject playerPrefab;

    /// <summary>
    /// to only spawn 1 player obj
    /// </summary>
    [HideInInspector]
    public Player activePlayer;

    /// <summary>
    /// ui prefab to look out for
    /// </summary>
    public GameObject uiPrefab;

    /// <summary>
    /// to only spawn 1 UI obj
    /// </summary>
    [HideInInspector]
    public Player activeUI;

    /// <summary>
    /// Stores Text Name highlight
    /// </summary>
    public Text objectName;

    /// <summary>
    /// Stores Gem Count text
    /// </summary>
    public Text gemCountText;

    /// <summary>
    /// Stores Stamina text
    /// </summary>
    public Text staminaText;

    /// <summary>
    /// Stores health text
    /// </summary>
    public Text healthText;

    
    void Awake()
    {
        //to destroy itself if the scene already has one by default?
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.activeSceneChanged += SpawnOnSceneLoad;
            instance = this;
        }
        
    }

    private void SpawnOnSceneLoad(Scene currentScene, Scene nextScene)
    {
        //find the spawn area for the player
        SpawnArea playerSpot = FindObjectOfType<SpawnArea>();
        //assign rotation and position for convinience
        Vector3 spawnPostion = playerSpot.transform.position;
        Quaternion spawnRotation = playerSpot.transform.rotation;
        
        //spawn new player at the specified location if active is null
        if (activePlayer == null)
        {
            GameObject newPlayer = Instantiate(playerPrefab, spawnPostion, spawnRotation);
            activePlayer = newPlayer.GetComponent<Player>();
        }//move the active player to that place
        else
        {
            activePlayer.transform.position = spawnPostion;
            activePlayer.transform.rotation = spawnRotation;
        }

    }
}
