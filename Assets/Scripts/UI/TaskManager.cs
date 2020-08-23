using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cyberultimate.Unity;

public class TaskManager : MonoSingleton<TaskManager>
{
    private Dictionary<string, GameObject> tasks = new Dictionary<string, GameObject>();
    public GameObject textObj;

	public void AddTask(string txt)
    {
        if (!tasks.ContainsKey(txt))
        {
            GameObject current = Instantiate(textObj, Vector3.zero, Quaternion.identity, transform);
            current.GetComponent<Text>().text = txt;
            tasks.Add(txt,current);
        }
    }

    public void RemoveTask(string txt)
    {
        if (tasks.ContainsKey(txt))
        {
            Animator anim = tasks[txt].GetComponent<Animator>();
            anim.Play("FadeOut");
            Destroy(tasks[txt], anim.GetCurrentAnimatorStateInfo(0).length);

            tasks.Remove(txt);
        }
    }

    public void RemoveAllTasks()
    {
        tasks.Clear();
    }
}
