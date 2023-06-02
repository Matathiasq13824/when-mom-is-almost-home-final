using UnityEngine;

//checks if an object is at the correct place
//Must be the component of an object who has a box collider with isTrigger enabled.
//The box collider must be where you want the object to be
public class TaskPosition : TaskInterface
{
    //The object who need to be checked
    public GameObject task;
    //The text explaining the task
    public string text;
    private bool done;

    private void Start()
    {
        done = false;
    }

    public override bool isTaskDone()
    {
        return done;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform == task.transform)
        {
            done = true;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.transform == task.transform)
        {
            done = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.transform == task.transform)
        {
            done = false;
        }
    }

    public override string getTaskText()
    {
        return text;
    }
}