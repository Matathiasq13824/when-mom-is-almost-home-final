using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using static HandController;

public class RotatingBehavior : MonoBehaviour
{

    [Header("Handle")]
    public HandleObject handle;

    [Header("Rotation Point")]
    public Vector3 rotation_point;

    [Header("Minimum angle")]
    public int min;


    [Header("Maximum angle")]
    public int max;

    [Header("Threshold angle")]
    public float threshold = 1f;

    [Header("Axis")]
    public Vector3 axis = Vector3.up;

    private HandController hand;

    private float angle;
    private Vector3 world_rotation_point;
    

    private void Start()
    {
        angle = 0;
        world_rotation_point = (this.transform.position + rotation_point);
    }


    // Update is called once per frame
    void Update()
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

            //Get position of the handle based on the rotation point and the position where we want the handle to be (where the hand is)
            Vector3 original_position = handle.transform.position - world_rotation_point;
            Vector3 new_position = hand.transform.position - world_rotation_point;

            // To reduce noise, level the vector based on the axis
            if (axis == Vector3.up)
            {
                new_position.y = original_position.y;
            }
            if (axis == Vector3.forward)
            {
                new_position.z = original_position.z;
            }
            if (axis == Vector3.right)
            {
                new_position.x = original_position.x;
            }


            //Get the difference between the two vectors 
            float temp = Vector3.SignedAngle(original_position, new_position, axis);

            //If the angle is big enough and doesn't exceed the maximum and minimum angle, rotate
            if ((temp >= threshold || temp <= -threshold) && angle+temp >= min && angle+temp <= max) {
                this.transform.RotateAround(world_rotation_point, axis, temp);
                angle = angle+temp;
            }
        }
    }
}
