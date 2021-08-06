/******************************************************************************
Author: Leong Yu Xuan

Name of Class: RotatePost

Description of Class: This class controls rotation of the laser post's "head"

Date Created: 09/06/2021
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePost : MonoBehaviour
{
    /// <summary>
    /// Head to rotate
    /// </summary>
    [SerializeField]
    private GameObject laserHead;

    /// <summary>
    /// the angle to rotate per click
    /// </summary>
    [SerializeField]
    private float rotateAngle = 22.5f;

    ///<summary>
    ///bool to inverse if needed
    /// </summary>
    [SerializeField]
    private bool inverse = false;
    

    public void Interact()
    {
        //assign initial local rotation of object
        Vector3 laserRotation = laserHead.transform.localRotation.eulerAngles;
        //add or subtract to the rotation 
        if (inverse)
        {
            laserRotation.y -= rotateAngle;
        }
        else
        {
            laserRotation.y += rotateAngle;
        }
        //set to new rotation value
        laserHead.transform.localRotation = Quaternion.Euler(laserRotation);
        

    }
}
