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
    /// ui prefab to look out for
    /// </summary>
    public GameObject uiPrefab;

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

    [HideInInspector]
    public Player activePlayer;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
