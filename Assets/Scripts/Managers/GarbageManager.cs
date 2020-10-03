using Cyberultimate;
using Cyberultimate.Unity;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GarbageManager : MonoSingleton<GarbageManager>
{
	public bool GarbageSet { get; set; } = true;

	[SerializeField]
	private ReorderableArray<ItemScriptableObject> itemChanceDrops;
	public ItemScriptableObject[] ItemChanceDrops => itemChanceDrops.BaseArray;

	private FakeRandom fakeRandom;
	protected override void Awake()
	{
		base.Awake();
		fakeRandom = new FakeRandom(Random.Range(2, ItemChanceDrops.Length - 1), 0, ItemChanceDrops.Length);
	}
	public ItemScriptableObject GetRandomItem ()
	{
		return ItemChanceDrops[fakeRandom.Next()];
	}
}

