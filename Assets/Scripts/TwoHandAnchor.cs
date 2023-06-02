using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.PlayerLoop.PreLateUpdate;

public class TwoHandAnchor : MonoBehaviour
{ 
    [Header("first_handle")]
    public HandleObject main_handle;

    [Header("second_handle")]
    public HandleObject second_handle;

    [Header("Distance of deattatchment")]
    public float distance_limit = 0.1f;

    private HandController hand_main;
    private HandController hand_second;

    private float distance_between_handle;

    // Store the boolean state indicating whether the object has been successfully hold and released
    private bool hasHoldAndRelease = false;

    // Store the boolean state indicating if the object is being grabbed by both hands
    private bool isGrabbedByBothHands = false;

    private void Start()
    {
        distance_between_handle =  Vector3.Distance(main_handle.get_local_position(), second_handle.get_local_position());
        hand_main = null;
        hand_second = null;
    }

    // Update is called once per frame
    void Update()
    {

        //check if the two handles are grabbed
        if (hand_main == null && main_handle.is_attatched() && second_handle.is_attatched()){

            isGrabbedByBothHands = true;
            hasHoldAndRelease = false;

            hand_main = main_handle.get_controller();
            hand_second = second_handle.get_controller();

            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = false;


        //Reset everything if one of the hande are not grabbed anymore
        } else if (!main_handle.is_attatched() || !second_handle.is_attatched()){

            // Set hasHoldAndRelease to true when the object was being grabbed and is about to be released
            if (isGrabbedByBothHands) hasHoldAndRelease = true;
            isGrabbedByBothHands = false;

            hand_second = null;
            hand_main = null;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().isKinematic = false;
        }

        //if grabbed by two hands, move
        else if (hand_main != null && hand_second != null){

            //if the hands move further than a giving distance of the handles, detatch
            if ((distance_between_handle + distance_limit) < Vector3.Distance(hand_main.transform.position, hand_second.transform.position) 
                || (distance_between_handle - distance_limit) > Vector3.Distance(hand_main.transform.position, hand_second.transform.position))
            {
                main_handle.detach_from(hand_main);
                second_handle.detach_from(hand_second);
            }

            //Move the object based on the position of the main hand
            Vector3 new_pos = hand_main.transform.position - main_handle.get_position();
            transform.position = transform.position + new_pos;


            //Rotate the object based on the second hand
            Vector3 original_rotation = main_handle.get_position() - second_handle.get_position();
            Vector3 new_rotation = hand_main.transform.position - hand_second.transform.position;

            float angle = Vector3.Angle(original_rotation, new_rotation);
            Vector3 axis1 = Vector3.Cross(original_rotation, new_rotation).normalized;

            //If movement too small, don't move
            if (angle >= 1)
            {
                transform.RotateAround(main_handle.transform.position, axis1, angle);
            }

            //To avoid strange behavior, remove velocity when grabbed
            transform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            transform.GetComponent<Rigidbody>().velocity = Vector3.zero;

 
        }
    }

    public bool getHasHoldAndReleased()
    {
        return hasHoldAndRelease;
    }
}
