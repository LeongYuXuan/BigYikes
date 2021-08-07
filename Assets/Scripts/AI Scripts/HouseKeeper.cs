
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HouseKeeper : MonoBehaviour
{

    public NavMeshAgent agentComponent;

    //public int hkhealth = ;
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
            //if (Vector3.Distance(GameManager.instance.activePlayer.transform.position, transform.position) < 8)
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
            if (Statue.statueOn = true)
            {
                agentComponent.SetDestination(statuePos);
            }
        }
    }

}
