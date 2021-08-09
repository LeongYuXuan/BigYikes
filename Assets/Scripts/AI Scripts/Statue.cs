using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Statue : MonoBehaviour
{
    /// <summary>
    /// Assigning the gameObject as an agent
    /// </summary>
    public NavMeshAgent statueAgent;

    /// <summary>
    /// Assigning the gameObject as an obstacle
    /// </summary>
    public NavMeshObstacle statueObstacle;

    /// <summary>
    /// Assigning the script to the gameObject
    /// </summary>
    public GameObject statue;

    /// <summary>
    /// original statue position
    /// </summary>
    private Vector3 originalPos;

    /// <summary>
    /// Current statue position
    /// </summary>
    private Vector3 statuePos;

    /// <summary>
    /// If true, statue is off original position
    /// If false, statue is not off original position
    /// Along the lines of turned on instead of being physically "on"
    /// </summary>
    public bool statueOn;

    // Start is called before the first frame update
    void Start()
    {
        //assigning respective scripts to the statue
        statueAgent = GetComponent<NavMeshAgent>();
        statueObstacle = GetComponent<NavMeshObstacle>();
        originalPos = statue.transform.position;
        
    }

    // Update is called once per frame
    //always checking for distance between current and original position
    void Update()
    {
        statuePos = statue.transform.position;
        if (Vector3.Distance(statuePos, originalPos) < 0.5)
        {
            statueOn = false;
            statueAgent.enabled = false;
            statueObstacle.enabled = true;
        }
        else if (Vector3.Distance(statuePos, originalPos) > 0.5)
        {
            statueOn = true;
            statueObstacle.enabled = false;
            
        }
        if (statueAgent.enabled == true)
        {
            statueAgent.SetDestination(originalPos);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (statueOn == false||other.gameObject.name == "HK")
        {
            statueAgent.enabled = true;
        }
    }
}
