/******************************************************************************
Author: Leong Yu Xuan

Name of Class: Weak wall;

Description of Class: This class is a test class for the switch to see if it works

Date Created: 02/08/2021
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCube : MonoBehaviour
{
    /// <summary>
    /// Bool that stores intial state of the obj's mesh renderer
    /// </summary>
    private bool isActive;

    /// <summary>
    /// assign the enabled bool to isActive
    /// </summary>
    private void Awake()
    {
        isActive = gameObject.GetComponent<MeshRenderer>().enabled;
    }

    /// <summary>
    /// The script to execute when interacted with by switch. toggles the mesh renderer of obj
    /// </summary>
    public void switchInteract()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = !isActive;
    }
}
