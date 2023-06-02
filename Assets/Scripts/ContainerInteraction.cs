using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ContainerInteraction : MonoBehaviour
{
    private float nextUpdate;
    private float occurenceTime = 0.2f;
    private void Start()
    {
        nextUpdate = Time.time + occurenceTime;
    }

    private void Update()
    {
        if (Time.time >= nextUpdate)
        {
            nextUpdate = Time.time + occurenceTime;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Verify only once in a given number of frame to verify if the object can still move 
        if (Time.time >= nextUpdate)
        {
            //If the object is not grasped, is not already a children of the container and it's speed is 0 (won't move anymore), fix the object
            if (other.gameObject.GetComponent<ObjectAnchor>().is_available() && other.gameObject.transform.parent != this.transform && other.gameObject.GetComponent<Rigidbody>().velocity.magnitude <= float.Epsilon)
            {
                
                other.gameObject.GetComponent<Rigidbody>().useGravity = false;
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                other.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                other.transform.SetParent(this.transform);
                //As the object is detected when its speed is close to zero, it can be a little inside the boundary of the other object,
                //causing some minor force to push the container.
                //As these force can cause some problems, when an object is detected, we move it a little upward to solve it
                other.transform.position += new Vector3(0, 0.002f, 0);

            } 
        }
        //If the container is upside down, release the objects
        if (other.gameObject.transform.parent == this.transform && Vector3.Dot(this.transform.up, Vector3.down) >= 0.7)
        {
            OnTriggerExit(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Vector3.Dot(this.transform.up, Vector3.down) >= 0.7 && other.gameObject.GetComponent<ObjectAnchor>().is_available()){
            other.transform.SetParent(other.gameObject.GetComponent<ObjectAnchor>().get_initial_parent());
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
