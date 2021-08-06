/******************************************************************************
Author: Leong Yu Xuan

Name of Class: Switch

Description of Class: Class for the pressure plates. Returns "true" on trigger stay
                        
                        

Date Created: 06/08/2021
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public bool isActivate = false;
    /// <summary>
    /// return true if colliding item is a "pressureItem"
    /// </summary>
    /// <param name="other"> the thing colliding with the trigger </param>
    /// <returns></returns>
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PressureItem")
        {
            isActivate = true;
            
        }
    }

    public void OnTriggerExit(Collider other)
    {
        isActivate = false;
    }
}
