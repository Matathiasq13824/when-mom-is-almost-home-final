using UnityEngine;
using System.Collections;

public class Liquid : MonoBehaviour
{
    // Store the increasing speed of the water level
    private float increaseSpeed = 0.0005f;

    // Store the maximum water level
    public float maxLiquidLevel;

    // Store the current water level
    private float currentLiquidLevel = 0;

    // Raise water level when the maximum has not yet reached 
    public void raise()
    {
        if (currentLiquidLevel < maxLiquidLevel)
        {
            float newLiquidLevel = transform.position.y + increaseSpeed;
            transform.position = new Vector3(transform.position.x, newLiquidLevel, transform.position.z);
            currentLiquidLevel = newLiquidLevel;
        }
    }

    // Setter for the maximum water level
    public void setMaxLiquidLevel(float newMaxLiquidLevel)
    {
        maxLiquidLevel = newMaxLiquidLevel;
    }

    public bool hasAttainedMaxLevel(float threshold)
    {
        return (currentLiquidLevel + threshold >= maxLiquidLevel);
    }

}

