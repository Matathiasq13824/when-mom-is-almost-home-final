using UnityEngine;

public class DrawerBehaviour : MonoBehaviour
{
    // Store the grabable of the drawer
    [Header("Handle")]
    public HandleObject handle;

    // Store along which axis can this drawer move
    [Header("Axis")]
    public Vector3 axis;

    // Store the degree of tolerance angle
    // i.e. drawer can be dragged/pushed when the force angle is from +tolerance to -tolerance
    [Header("Tolerance")]
    public float tolerance = 20.0f;

    // Store the maximum distance that the drawer can move
    [Header("Max Movement")]
    public float maxMovement = 0.8f;

    private HandController hand;
    private Vector3 initPosition;

    void Start()
    {
        initPosition = transform.position;
    }

    // Update the position of the drawer
    void FixedUpdate()
    {
        if (handle.is_attatched())
        {
            hand = handle.get_controller();
        }
        else
        {
            hand = null;
        }

        if (hand != null)
        {
            Vector3 handMovementDirection = hand.transform.position - handle.transform.position;
            float angle = Vector3.Angle(handMovementDirection, axis);
            float change = Vector3.Dot(handMovementDirection, axis);
            if (isPullingInDirection(angle) && canBeFurtherPulled(change) &&  change < maxMovement)
            {
                transform.position = transform.position + Vector3.Dot(handMovementDirection, axis) * axis;
            }
        }
    }

    // Return true if the drawer is pulled/pushed in a valid direction
    private bool isPullingInDirection(float angle)
    {
        return Mathf.Abs(angle) < tolerance || (180.0f - Mathf.Abs(angle)) < tolerance;
    }

    // Return true if the drawer if pulled to the max distance
    private bool isPulledToMax()
    {
        return Vector3.Dot(transform.position - initPosition, axis) >= maxMovement;
    }

    // Return true if the drawer can be further pulled OR pushed
    // i.e. It can be pulled until the max distance is reached and it can be pushed until the origin position
    private bool canBeFurtherPulled(float change)
    {
        return (!isPulledToMax() || change < 0) && (Vector3.Dot(transform.position - initPosition, axis) > 0 || change > 0);
    }
}

