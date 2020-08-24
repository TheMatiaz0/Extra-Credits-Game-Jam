using Cyberultimate;
using Cyberultimate.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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
			tasks.Add(txt, current);
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
	private void BrutalRemoveTask(string txt)
	{
		Destroy(tasks[txt]);
		tasks.Remove(txt);
	}

	private void Start()
	{
		TimeManager.Instance.OnCurrentDayChange += RemoveAllTasks;
	}

	private void RemoveAllTasks(object sender, SimpleArgs<Cint> e)
	{
		foreach (string t in tasks.Keys.ToList())
		{
			BrutalRemoveTask(t);
		}
	}
}
