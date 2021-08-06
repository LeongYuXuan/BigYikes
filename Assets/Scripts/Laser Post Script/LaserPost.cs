/******************************************************************************
Author: Leong Yu Xuan

Name of Class: Switch

Description of Class: Class for the laser posts. They rotate when interacted by player
                        Can activate switches. laser is harmless 
                        Essentially a detached player interactRaycast
                        
                        

Date Created: 02/08/2021
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPost : MonoBehaviour
{
    /// <summary>
    /// whether the laser is activated or not
    /// </summary>
    public bool isOn;

    /// <summary>
    /// Where the laser would be emmited
    /// </summary>
    [SerializeField]
    private GameObject laserHead;

    /// <summary>
    /// the length of the laser
    /// </summary>
    private float interactDist = 8;

    private void Update()
    {
        //have raycast on by default if is on
        if (isOn)
        {
            laserRaycast();
        }
        
    }

    private void laserRaycast()
    {
        //variable that stores what raycast has hit
        RaycastHit hitinfo;

        //layer mask for the raycast. Only detect under this
        int layermask = 1 << LayerMask.NameToLayer("Interactable");

        string objTag;

        //debug line for raycast
        Debug.DrawLine(laserHead.transform.position,
            laserHead.transform.position + laserHead.transform.forward * interactDist);

        //do something if the raycast hits something
        if (Physics.Raycast(laserHead.transform.position, laserHead.transform.forward, out hitinfo, interactDist, layermask))
        {
            objTag = hitinfo.transform.tag;
            //do this if laser hits something
            if (objTag == "Laser")
            {
                hitinfo.transform.GetComponent<LaserPost>().laserRaycast();
            }
            if (objTag == "Switch")
            {
                hitinfo.transform.GetComponent<Switch>().Interact();
            }
        }
    }

    /// <summary>
    /// what to execute if being interacted by switch
    /// re-enable it's own collider and unhide the laser head
    /// </summary>
    public void switchInteract() 
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;
        laserHead.GetComponent<MeshRenderer>().enabled = true;
    }

}
