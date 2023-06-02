using System;
using Unity.VisualScripting;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    // Store the threshold of pouring
    public int pourThreshold = 35;

    // Store the origin of the liquid
    public Transform origin = null;

    // Store stream prefab
    public GameObject streamPrefab = null;

    // Store the axis around which the object may be rotated around
    public Vector3 rotateAxis;

    // Store the angle that the object will be rotate around when calculating the pouring angle
    public float rotateDegree;

    private bool isPouring = false;
    private Stream currentStream = null;

    // Update the pouring effect
    private void Update()
    {
        bool pourCheck = CalcPourAngle() < pourThreshold;

        if (isPouring != pourCheck)
        {
            isPouring = pourCheck;

            if (isPouring)
            {
                StartPour();
            }
            else
            {
                EndPour();
            }
        }
    }

    // Start the pouring effect
    private void StartPour()
    {
        currentStream = CreateStream();
        currentStream.Begin();
    }

    // Stop the pouring effect
    private void EndPour()
    {
        currentStream.End();
        currentStream = null;
    }

    // Calculate the angle of pouring
    private float CalcPourAngle()
    {
        if (!rotateDegree.Equals(0))
        {
            return (Quaternion.AngleAxis(rotateDegree, rotateAxis) * transform.forward).y * Mathf.Rad2Deg;
        } else {
            return transform.forward.y * Mathf.Rad2Deg;
        }
    }

    // Initialize the stream
    private Stream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        return streamObject.GetComponent<Stream>();
    }
}