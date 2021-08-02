/******************************************************************************
Author: Leong Yu Xuan

Name of Class: WeakWall

Description of Class: This class controls the simple behaviour of the weak wall
                        Essentially a harmless enemy with no ai

Date Created: 02/08/2021
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakWall : MonoBehaviour
{
    /// <summary>
    /// Health of wall
    /// </summary>
    public int health = 3;

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

    private void Start()
    {
        //assign the render and ogcolour
        render = GetComponent<MeshRenderer>();
        ogColour = render.material.color;
    }


    /// <summary>
    /// Function to trigger on anything to do with the health
    /// </summary>
    /// <param name="">How much health to add. Go negative to subtract</param>
    public void HealthManager(int Num)
    {
        //change health value based on that
        health += Num;
        //trigger the colour change
        StartCoroutine(colourChange());

        //disappear if health = 0
        if (health <= 0)
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
