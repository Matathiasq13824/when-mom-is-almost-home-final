using UnityEngine;
using System.Collections;

// currently this behaviour is only applicable if the parent of the collider is the same as the parent set here
// i.e. collision.collider.gameObject.transform.parent.gameObject == parent
public class AttachToParentBehaviour : MonoBehaviour
{
    [Header("Parent")]
    public GameObject parent = null;

    private bool isCollidingWithParent = false;
    private Vector3 initParentPosition;

    private void Update()
    {
        if (isCollidingWithParent)
        {
            Vector3 difference = parent.transform.position - initParentPosition;
            initParentPosition = parent.transform.position;
            transform.position = transform.position + difference;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.transform.parent.gameObject == parent)
        {
            isCollidingWithParent = true;
            initParentPosition = parent.transform.position;
        } else
        {
            isCollidingWithParent = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isCollidingWithParent = false;
    }
}

