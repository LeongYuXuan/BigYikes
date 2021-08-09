using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Statue : MonoBehaviour
{
    public NavMeshAgent statueAgent;

    public GameObject statue;

    private Vector3 originalPos;

    private Vector3 statuePos;


    public bool statueOn;

    // Start is called before the first frame update
    void Start()
    {
        statueAgent = GetComponent<NavMeshAgent>();
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
        }
        else if (Vector3.Distance(statuePos, originalPos) > 0.5)
        {
            statueOn = true;
            
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
