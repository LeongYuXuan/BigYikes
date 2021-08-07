/******************************************************************************
Author: Leong Yu Xuan

Name of Class: StoneGuardian

Description of Class: This class controls the behaviour of the ""boss"" in the area.
                        It is more of a miniboss with boosted stats

Date Created: 09/06/2021
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class StoneGuardian : MonoBehaviour
{
    //Active state controlled externally
    //Stays away from player from a fixed distance and shoots at them for a while
    //Charges at player. deals twice the damage but can get stuck in walls
    //Stuck for 5 seconds
    //Shoot, Charge and Stuck stages

    /// <summary>
    /// Coroutine setup
    /// </summary>
    public string nextState;
    public string currentState;

    /// <summary>
    /// The NavMeshAgent this is attached to
    /// </summary>
    public NavMeshAgent myAgent;

    /// <summary>
    /// Thing to move towards
    /// </summary>
    public Transform target;

    /// <summary>
    /// Health of swarmer
    /// </summary>
    public int health = 5;

    /// <summary>
    /// attack power of swarmer
    /// </summary>
    public int attack = 1;

    ///<summary>
    ///The material renderer for this
    /// </summary>
    private Renderer render;

    ///<summary>
    ///original colour of the swarmer material
    /// </summary>
    [SerializeField]
    private Color ogColour;

    ///<summary>
    ///colour to fade to upon being damaged
    /// </summary>
    public Color damageColour;

    ///<summary>
    /// the fade time 
    /// </summary>
    private float damageTime = 0.1f;

    /// <summary>
    /// Things to assign upon starting
    /// </summary>
    private void Start()
    {
        render = GetComponent<MeshRenderer>();
        ogColour = render.material.color;

        // Get the attached NavMeshAgent and store it in agentComponent
        myAgent = GetComponent<NavMeshAgent>();

        //Set start state to idle
        nextState = "Shoot";
    }

    /// <summary>
    /// update happens every frame or something
    /// </summary>
    void Update()
    {
        //Check if the AI should change to a new state
        if (nextState != currentState)
        {
            // Stop the current running coroutine first before starting a new one.
            StopCoroutine(currentState);
            currentState = nextState;
            StartCoroutine(currentState);
        }
    }

    private IEnumerator Shoot()
    {
        yield return null;
    }
}
