using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidContainer : MonoBehaviour
{
    // Store the liquid prefab
    [Header("Liquid")]
    public GameObject liquidPrefab = null;

    // Store the maximum liquid level
    [Header("Max Liquid Level")]
    public float maxLiquidLevel = 0.1f;

    // Store the starting point of the liquid
    [Header("Init Liquid Level")]
    public float initLiquidLevel = 0;

    private GameObject liquid = null;
    private Liquid liquidComponent;

    // Raise the liquid level
    public void raiseLiquidLevel()
    {
        liquidComponent.raise();
    }

    // Create and initialize the liquid if it does not exist
    public void createLiquidIfNotExist()
    {
        if (liquid == null)
        {
            liquid = Instantiate(liquidPrefab, transform.position + new Vector3(0, 1, 0) * initLiquidLevel, Quaternion.identity, transform);
            liquidComponent = liquid.GetComponent<Liquid>();
            liquidComponent.setMaxLiquidLevel(maxLiquidLevel + transform.position.y);
        }
    }

    public bool isMax(float threshold)
    {
        if (liquid == null) return false;
        else return liquidComponent.hasAttainedMaxLevel(threshold);
    }
}