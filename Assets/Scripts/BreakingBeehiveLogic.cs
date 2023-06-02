using UnityEngine;
using System.Collections;

public class BreakingBeehiveLogic : MonoBehaviour
{
    public TaskBreaking task;
    Vector3 PrevPos; 
    Vector3 NewPos; 
    Vector3 ObjVelocity;
    private int flag = 0;
    void OnTriggerEnter (Collider other)
    {
        flag += 1;
        if(flag == 1)
        {
            PrevPos = other.transform.position;
        }
        else
        {
            NewPos = other.transform.position;  // each frame track the new position
            ObjVelocity = (NewPos - PrevPos) / Time.fixedDeltaTime;  // velocity = dist/time
            PrevPos = NewPos;  // update position for next frame calculation
        }
        Debug.LogWarning("VELOCITY MAGNITUDE: "+ObjVelocity.sqrMagnitude);
        if(ObjVelocity.sqrMagnitude >= 20) //check velocity magnitude as measure of force applied
        {
            foreach(Transform t in gameObject.transform) //shatter beehive
            {
                Rigidbody rb = t.gameObject.AddComponent<Rigidbody>() as Rigidbody;
                BoxCollider boxCollider = t.gameObject.AddComponent<BoxCollider>();
                t.parent = t.parent.parent;
            }
            task.done = true;
            Destroy(gameObject);
        }
    }
}
 

