
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HouseKeeper : MonoBehaviour
{
    public Statue stat;

    public NavMeshAgent agentComponent;

    public int hkhealth = 30;
    /// <summary>
    /// Array for keeping track of the statues
    /// </summary>
    [SerializeField]
    private GameObject[] statueArray;


    public string nextState;
    public string currentState;

    private bool active;

    [SerializeField]
    private float moveSpeed;

    //pillar to go toward
    [SerializeField]
    Transform pillars;

    private Renderer render;

    [SerializeField]
    private Color ogColour;

    public Color damageColour;

    private float damageTime = 0.1f;

    [SerializeField]
    private GameObject HomePoint;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<MeshRenderer>();
        ogColour = render.material.color;



        //Set start state to idle
        nextState = "Idle";
    }

    // Update is called once per frame
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

        ////if (pillars != null)
        //{
        //    agentComponent.SetDestination(pillars.position);
        //}

        //if(pillars.position != pillarsOriginal)

    }

    private IEnumerator Idle()
    {
        while (currentState == "Idle")
        {
            yield return null;
            if (stat.statueOn = true)
            {
                Debug.Log("Discrepancy detected.");
                nextState = "Housekeeping";
            }
        }

    }

    private IEnumerator Housekeeping()
    {
        while (currentState == "Housekeeping")
        {
            yield return null;
            agentComponent.SetDestination(stat.statue.transform.position);

            if (stat.statueOn = false)
            {
                Debug.Log("Nice and clean");
                nextState = "Return";
            }
        }
    }

    private IEnumerator Return()
    {
        while (currentState == "Return")
        {
            yield return null;
            agentComponent.SetDestination(HomePoint.transform.position);

            //set back to idle once within range of the home point
            if (Vector3.Distance(HomePoint.transform.position, transform.position) < 2)
            {
                nextState = "Idle";
            }
        }

    }

    public void HealthManager(int Num)
    {
        //change health value based on that
        hkhealth += Num;
        //trigger the colour change
        StartCoroutine(colourChange());

        //disappear if health = 0
        if (hkhealth <= 0)
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
