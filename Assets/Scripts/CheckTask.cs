using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Class used to recover all of the tasks and give the complete text for the text mesh
//If one of the task is bone, its text will be crossed
public class CheckTask : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    public GameObject taskParent;
    private TaskInterface[] taskInterfaces;

    // Start is called before the first frame update
    void Start()
    {
        taskInterfaces = taskParent.GetComponentsInChildren<TaskInterface>();
    }

    // Update is called once per frame
    void Update()
    {
        textMeshPro.text = getTextFromTask();
    }

    private string getTextFromTask()
    {
        string texts = "";

        foreach (var task in taskInterfaces)
        {
            //If the task is done, cross it
            if (task.isTaskDone() && task.getTaskText() != "")
            {
                texts += "<s>" + task.getTaskText() + "</s><br>";
            }
            //If not, only the simple text
            else if (task.getTaskText() != "")
            {
                texts += task.getTaskText() + "<br>";
            }
        }
        return texts;
    }
}
