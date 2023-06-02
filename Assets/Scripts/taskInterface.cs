using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Abstract class defining the completion of the different tasks
//we need to know if the task is done and it's explaining text
public abstract class TaskInterface : MonoBehaviour
{
    public abstract bool isTaskDone();
    public abstract string getTaskText();
}
