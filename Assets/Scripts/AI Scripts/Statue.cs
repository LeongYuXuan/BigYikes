using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{

    public GameObject statue;

    private Vector3 originalPos;

    private Vector3 statuePos;

    public bool statueOn;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = statue.transform.position;
    }

    // Update is called once per frame
    //always checking for distance between current and original position
    void Update()
    {
        statuePos = statue.transform.position;
        if (Vector3.Distance(statuePos, originalPos) > 0.5)
        {
            statueOn = true;
             
        }
        else if (Vector3.Distance(statuePos, originalPos) < 0.5)
        {
            statueOn = false;
        }
    }
}
