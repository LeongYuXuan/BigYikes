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
    //Stays away from player from a fixed distance and shoots at them (10 seconds)
    //Charges at player. deals twice the damage but can get stuck in walls (10 seconds)
    //Stuck for 5 seconds
    //Stops enemy spawn and spawns in gem when defeated. Becomes immobile
    //Shoot, Charge, Stuck, Defeat state

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

    ///<summary>
    ///Checkpoint to return to upon losing target sight
    /// </summary>
    [SerializeField]
    private GameObject HomePoint;

    /// <summary>
    /// Things to assign upon starting
    /// </summary>
    private void Start()
    {
        render = GetComponent<MeshRenderer>();
        ogColour = render.material.color;

        // Get the attached NavMeshAgent and store it in agentComponent
        myAgent = GetComponent<NavMeshAgent>();

        // Set the target to the player
        target = GameManager.instance.activePlayer.transform;

        //Set start state to Shoot
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
        myAgent.speed = 5;
        float time = 0;
        while (currentState == "Shoot")
        {
            yield return null;
            myAgent.SetDestination(target.position);
            
            if(time < 10)
            {
                time += Time.deltaTime;
            }
            else if(time > 10)
            {
                time = 0;
                nextState = "Charge";
            }
        }
    }

    private IEnumerator Charge()
    {
        myAgent.speed = 10;
        float time = 0;
        while (currentState == "Charge")
        {
            yield return null;
            myAgent.SetDestination(target.position);

            //timer for change
            if (time < 10)
            {
                time += Time.deltaTime;
            }
            else if (time > 10)
            {
                time = 0;
                nextState = "Shoot";
            }
        }
    }

    private IEnumerator Stuck()
    {
        yield return null;
        myAgent.speed = 0;
        float time = 0;
        while (currentState == "Stuck")
        {
            yield return null;
            myAgent.SetDestination(target.position);

            //timer for change
            if (time < 5)
            {
                time += Time.deltaTime;
            }
            else if (time > 5)
            {
                time = 0;
                nextState = "Shoot";
            }
        }
    }

    private IEnumerator Defeat()
    {
        yield return null;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && currentState == "Charge")
        {
            nextState = "Stuck";
        }
    }

}
