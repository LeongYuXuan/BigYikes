/******************************************************************************
Author: Leong Yu Xuan

Name of Class: TimedPlatform

Description of Class: This class is for the timed platforms, which toggles their state based on a timer

Date Created: 03/08/2021
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedPlatform : MonoBehaviour
{
    [SerializeField]
    private float Time;

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

    public void switchInteract()
    {
        StopCoroutine("TogglePlatform");
        StartCoroutine("TogglePlatform");
    }
    IEnumerator TogglePlatform()
    {
        for (int i = 0; i < 2; ++i)
        {
            if (i == 0)
            {
                gameObject.GetComponent<MeshRenderer>().enabled = !initialMeshState;
                gameObject.GetComponent<MeshCollider>().enabled = !initialColliderState;
            }
            else if (i == 1)
            {
                gameObject.GetComponent<MeshRenderer>().enabled = initialMeshState;
                gameObject.GetComponent<MeshCollider>().enabled = initialColliderState;
            }
            yield return new WaitForSeconds(Time);
        }
    }
}
