using UnityEngine;
using UnityEngine.XR;

public class ObjectAnchor : MonoBehaviour
{

    [Header("Grasping Properties")]
    public float graspingRadius = 0.1f;
    public string objectType = "Normal";

    // Store initial transform parent
    protected Transform initial_transform_parent;

    // Store the boolean state indicating whether the object has been successfully hold and released
    private bool hasHoldAndRelease = false;

    void Start()
    {
        initial_transform_parent = transform.parent;
    }


    // Store the hand controller this object will be attached to
    protected HandController hand_controller = null;

    public virtual void attach_to(HandController hand_controller)
    {
        // Store the hand controller in memory
        this.hand_controller = hand_controller;

        // Set the object to be placed in the hand controller referential
        this.GetComponent<Rigidbody>().useGravity = false;
        this.GetComponent<Rigidbody>().isKinematic = true;

        if (this.objectType == "Normal")
        {
            transform.SetParent(hand_controller.transform);
        }

        hasHoldAndRelease = false;
    }

    public virtual void detach_from(HandController hand_controller)
    {
        // Make sure that the right hand controller ask for the release
        if (this.hand_controller != hand_controller) return;

        // Detach the hand controller
        this.hand_controller = null;

        // Set the object to be placed in the original transform parent
        this.GetComponent<Rigidbody>().useGravity = true;
        this.GetComponent<Rigidbody>().isKinematic = false;
        //When the object is detatched, keep the velocity of the hand 
        if (hand_controller.handType == HandController.HandType.LeftHand)
        {
            this.GetComponent<Rigidbody>().AddForce(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LHand), ForceMode.VelocityChange);
        }
        else
        {
            this.GetComponent<Rigidbody>().AddForce(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RHand), ForceMode.VelocityChange);
        }

        transform.SetParent(initial_transform_parent);

        hasHoldAndRelease = true;
    }

    public bool is_available() { return hand_controller == null; }

    public float get_grasping_radius() { return graspingRadius; }

    public Transform get_initial_parent() { return initial_transform_parent; }

    public bool getHasHoldAndReleased()
    {
        return hasHoldAndRelease;
    }
}
