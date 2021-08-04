/******************************************************************************
Author: Leong Yu Xuan

Name of Class: TriggerTeleport

Description of Class: This class sends the player to the designated teleport point
                        on trigger enter.
                        It can hurt the player if a bool is activated

Date Created: 03/08/2021
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField]
    private int atk = 1;

    [SerializeField]
    private bool canHurt = false;

    [SerializeField]
    private Transform targetLocation;

    

    private void OnTriggerEnter(Collider other)
    {
        if (canHurt)
        {
            other.GetComponent<Player>().HealthManager(-atk);
        }
        other.transform.position = targetLocation.position;
        other.transform.rotation = targetLocation.rotation;
    }
}
