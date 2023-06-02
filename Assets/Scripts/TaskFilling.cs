using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskFilling : TaskInterface
{
    public string text;

    public LiquidContainer task;
    public float threshold;


    public override string getTaskText()
    {
        return text;
    }
    public override bool isTaskDone()
    {
        return task.isMax(threshold);
    }
}
