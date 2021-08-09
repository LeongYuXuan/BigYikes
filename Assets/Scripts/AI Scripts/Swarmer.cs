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
using UnityEngine.AI;

public class Swarmer : MonoBehaviour
{
    //Relentlessly chase the player in a certain radius
    //deals 1 damage upon contact
    //goes back to designated area once player leaves the radius
    //idle, chase and return state

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

    ///<summary>
    ///Things to assign upon starting
    /// </summary>
    private void Start()
    {
        render = GetComponent<MeshRenderer>();
        ogColour = render.material.color;

        // Get the attached NavMeshAgent and store it in agentComponent
        myAgent = GetComponent<NavMeshAgent>();

        //Set start state to idle
        nextState = "Idle";

        
    }
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

    private IEnumerator Idle()
    {
        while (currentState == "Idle")
        {
            yield return null;
            if (Vector3.Distance(GameManager.instance.activePlayer.transform.position, transform.position) < 8)
            {
                Debug.Log("Stop, criminal scum. You violated the law.");
                nextState = "Chasing";
            }
        }
        
    }

    private IEnumerator Chasing()
    {
        //to chase player I think
        target = GameManager.instance.activePlayer.transform;
        while (currentState == "Chasing") 
        {
            yield return null;
            //move swarmer towards player
            myAgent.SetDestination(target.position);

            if (Vector3.Distance(target.position, transform.position) > 8)
            {
                Debug.Log("Must have been the wind");
                nextState = "Return";
            }
            
        }
        
    }

    private IEnumerator Return()
    {
        target = HomePoint.transform;
        while(currentState == "Return")
        {
            yield return null;
            myAgent.SetDestination(target.position);

            //set back to idle once within range of the home point
            if (Vector3.Distance(target.position, transform.position) < 2)
            {  
                nextState = "Idle";
            }
        }
        
    }

    /// <summary>
    /// hurt the player upon collision
    /// </summary>
    /// <param name="collision">the gameobject it collided with</param>
    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.GetComponent<Player>().HealthManager(-attack);
    }


    /// <summary>
    /// Function to trigger on anything to do with the swarmer health
    /// </summary>
    /// <param name="">How much health to add. Go negative to subtract</param>
    public void HealthManager(int Num)
    {
        //change health value based on that
        health += Num;
        //trigger the colour change
        StartCoroutine(colourChange());

        //disappear if health = 0
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }

    }

    ///<summary>
    ///coroutine that playes to make the object flash
    /// </summary>
    private IEnumerator colourChange()
    {
        for (int i = 0; i < 2; ++i)
        {
            if (i == 0)
            {
                render.material.color = damageColour;
            }
            else if (i == 1)
            {
                render.material.color = ogColour;
            }
            yield return new WaitForSeconds(damageTime);
        }

        
    }

}
