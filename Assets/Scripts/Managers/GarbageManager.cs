using Cyberultimate;
using Cyberultimate.Unity;
using System.Collections.Generic;
using UnityEngine;

public class GarbageManager : MonoSingleton<GarbageManager>
{

	// 0 - 1 format
	[SerializeField]
	private SerializedDictionary<ItemScriptableObject, int> itemChanceDrops = new SerializedDictionary<ItemScriptableObject, int>();


	public Dictionary<ItemScriptableObject, int> ItemChanceDrops => itemChanceDrops.BaseDictionary;


	public bool GarbageSet { get; set; } = true;
	/*
	public void ResetGarbage(object sender, SimpleArgs<Cint> e)
	{
		foreach (Garbage g in transform.GetComponentsInChildren<Garbage>())
		{
			g.garbageUsed = false;
			g.interactionEnabled = true;
		}
	}

	private void Start()
	{
		TimeManager.Instance.OnCurrentDayChange += ResetGarbage;
	}

	protected void OnDisable()
	{
		TimeManager.Instance.OnCurrentDayChange -= ResetGarbage;
	}
	*/
}
