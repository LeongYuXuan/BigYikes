
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HouseKeeper : MonoBehaviour
{
    /// <summary>
    /// movement target assigning
    /// </summary>
    private GameObject target;

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

        //Agent Assigning
        agentComponent = GetComponent<NavMeshAgent>();

        //Set start state to idle
        nextState = "Checking";
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

    private IEnumerator Checking()
    {
        while (currentState == "Checking")
        {
            yield return null;
            for(int i = 0; i < statueArray.Length; ++i)
            {
                var statueActive = statueArray[i].GetComponent<Statue>().statueOn;
                if (statueActive)
                {
                    Debug.Log("Discrepancy detected.");
                    target = statueArray[i];
                    nextState = "Housekeeping";
                }
            }
        }

    }

    private IEnumerator Housekeeping()
    {
        while (currentState == "Housekeeping")
        {
            yield return null;

            agentComponent.SetDestination(target.transform.position);

            if (target.GetComponent<Statue>().statueOn == false)
            {
                target = HomePoint;
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
            //going to position of return point
            agentComponent.SetDestination(HomePoint.transform.position);

            //set back to idle once within range of the home point
            if (Vector3.Distance(HomePoint.transform.position, transform.position) < 2)
            {
                nextState = "Checking";
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
