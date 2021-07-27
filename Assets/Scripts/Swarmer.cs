/******************************************************************************
Author: Leong Yu Xuan

Name of Class: Swarmer

Description of Class: This class controls the behaviour of the generic
                        swarmer enemy

Date Created: 09/06/2021
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarmer : MonoBehaviour
{
    //Relentlessly chase the player in a certain radius
    //deals 1 damage upon contact
    //goes back to designated area once player leaves the radius
    //idle and chase state

    /// <summary>
    /// Coroutine setup
    /// </summary>
    public string nextState;
    public string CurrentState;

    /// <summary>
    /// Health of swarmer
    /// </summary>
    public int health = 5;

    /// <summary>
    /// attack power of swarmer
    /// </summary>
    public int attack = 1;

    /// <summary>
    /// Player object to affect
    /// </summary>
    public GameObject Player;

    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.GetComponent<Player>().HealthManager(-attack);
    }

}
