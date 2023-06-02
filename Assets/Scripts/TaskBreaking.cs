using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskBreaking : TaskInterface
{
    // Start is called before the first frame update
    public string text;

    //It is the beehive who change the value of this task when broken
    public bool done;

    void Start()
    {
        done = false;
    }
    public override string getTaskText()
    {
        return text;
    }
    public override bool isTaskDone()
    {
        return done;
    }
}
