/******************************************************************************
Author: Leong Yu Xuan

Name of Class: Weak wall;

Description of Class: This class controls the objects that appear/disappear upon switch activation

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
    private bool initialMeshState;

    /// <summary>
    /// Bool that stores intial state of the obj's mesh collider
    /// </summary>
    private bool initialColliderState;

    /// <summary>
    /// assign the respective bools to the variables
    /// </summary>
    private void Awake()
    {
        initialMeshState = gameObject.GetComponent<MeshRenderer>().enabled;
        initialColliderState = gameObject.GetComponent<MeshCollider>().enabled;
    }

    /// <summary>
    /// The script to execute when interacted with by switch. toggles the mesh renderer of obj
    /// </summary>
    public void switchInteract()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = !initialMeshState;
        gameObject.GetComponent<MeshCollider>().enabled = !initialColliderState;
    }
}
