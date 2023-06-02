using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class similar than TaskPosition, but who check the position of multiple objects
//Simply check if all of the TaskPosition are done
//All of the TaskPosition in tasks must have an empty text to avoid accumulation of text
public class TaskPositions : TaskInterface
{
    public string text;
    public TaskPosition[] tasks;


    public override bool isTaskDone()
    {
        foreach (TaskPosition task in tasks)
        {
            if (!task.isTaskDone())
            {
                return false;
            }

        }
        return true;
    }

    public override string getTaskText()
    {
        return text;
    }
}
