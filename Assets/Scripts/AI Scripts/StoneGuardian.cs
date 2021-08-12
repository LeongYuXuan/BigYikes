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
    public int health = 40;

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
        myAgent.speed = 10;
        float time = 0;
        while (currentState == "Shoot")
        { 
            yield return null;
            myAgent.SetDestination(target.position + target.forward*3.5f);
            
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
        myAgent.speed = 15;
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
        myAgent.speed = 0;
        attack = 0;
        while (currentState == "Defeat")
        {
            yield return null;
            myAgent.enabled = false;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Gem" && currentState == "Charge")
        {
            nextState = "Stuck";
        }
    }

    /// <summary>
    /// Function to trigger on anything to do with the boss' health
    /// 
    /// </summary>
    /// <param name="">How much health to add. Go negative to subtract</param>
    public void HealthManager(int Num)
    {
        //change health value based on that (only if not defeated)
        if(currentState != "Defeat")
        {
            health += Num;
        }
        //trigger the colour change
        StartCoroutine(colourChange());

        //disappear if health = 0
        if (health <= 0)
        {
            nextState = "Defeat";
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
