/******************************************************************************
Author: Elyas Chua-Aziz

Editor: Leong Yu Xuan

Name of Class: Player

Description of Class: This class is a test class for Wk14's lesson materials
                        has something to do with NavMeshes and Navve Agents
                         

Date Created: 09/06/2021
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavTest : MonoBehaviour
{
    /// <summary>
    /// The NavMeshAgent this is attached to
    /// </summary>
    public NavMeshAgent myAgent;

    /// <summary>
    /// Thing to move towards
    /// </summary>
    public Transform target;


    // Update is called once per frame
    void Update()
    {
        //null check for... something
        if (myAgent == null || target == null)
        {
            return;
        }
        //Set destination of NavMeshAgent
        myAgent.SetDestination(target.position);
        
    }
}
