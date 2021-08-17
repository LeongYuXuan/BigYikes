/******************************************************************************
Author: Leong Yu Xuan

Name of Class: BossTrigger

Description of Class: Class for a trigger that activates upon entering the boss room

Date Created: 13/06/2021
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    /// <summary>
    /// objects to make appear when triggered
    /// </summary>
    [SerializeField]
    private GameObject[] objArray;

    /// <summary>
    /// gem pedestal to disappear
    /// </summary>
    [SerializeField]
    private GameObject gem;

    private void OnTriggerEnter(Collider other)
    {
        //set all the enemies to active
        for(int i = 0; i < objArray.Length; ++i)
        {
            objArray[i].SetActive(true);
        }

        //disappear the gem and the post
        gem.SetActive(false);

        //set itself inactive
        gameObject.SetActive(false);
    }
}
