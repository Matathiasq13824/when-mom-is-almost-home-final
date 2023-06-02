using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// Simple class used in some grabbing interaction as the rotation of doors, the movement of drawers and the two-handed grabbing.
// Do nothing except knowing if it was grabbed and by what 
public class HandleObject : ObjectAnchor
{
    protected bool isAttached = false;
    public override void attach_to(HandController hand_controller)
    {
        // Store the hand controller in memory
        this.hand_controller = hand_controller;
        isAttached = true;      
    }

    public override void detach_from(HandController hand_controller)
    {
        // Make sure that the right hand controller ask for the release
        if (this.hand_controller != hand_controller) return;

        // Detach the hand controller
        this.hand_controller = null;
        isAttached = false;
    }


    public bool is_attatched() { return isAttached; }

    public HandController get_controller() { return hand_controller; }

    public Vector3 get_position() { return this.transform.position; }

    public Vector3 get_local_position() { return this.transform.localPosition; }

}
