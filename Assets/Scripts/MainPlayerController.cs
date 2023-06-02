using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerController : MonoBehaviour
{

    public OVRVignette vignetteCenter;
    public int maxStamina;

    private int stamina;

    private bool exhausted = false;

    // Store the boolean state indicating if the user has sprint
    private bool hasSprint = false;

    void Start()
    {
        stamina = maxStamina;
        vignetteCenter.enabled = false;

    }

    void Update()
    {

        //Sprint option 
        if (true && OVRInput.Get(OVRInput.Button.PrimaryThumbstick) && !exhausted)
        {
            this.GetComponent<OVRPlayerController>().Acceleration = 0.3f;
            stamina -= 5;
            vignetteCenter.enabled = true;

            if (stamina <= 0)
            {
                exhausted = true;
            }

            hasSprint = true;
        }
        else if (exhausted)
        {
            //When exhausted, move slower and recover slower
            this.GetComponent<OVRPlayerController>().Acceleration = 0.05f;
            if (exhausted && (stamina >= maxStamina * 0.8))
            {
                exhausted = false;
            }

            if ((stamina + 5) >= maxStamina)
            {
                stamina = maxStamina;
            }
            else
            {
                stamina += 5;
            }
            vignetteCenter.enabled = false;


        }
        else
        {
            this.GetComponent<OVRPlayerController>().Acceleration = 0.1f;
            if (stamina + 15 >= maxStamina)
            {
                stamina = maxStamina;
            }
            else
            {
                stamina += 15;
            }
            vignetteCenter.enabled = false;
        }
    }

    public bool getHasSprint()
    {
        return hasSprint;
    }
}