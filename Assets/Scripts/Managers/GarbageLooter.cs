using Cyberultimate;
using Cyberultimate.Unity;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GarbageLooter : MonoBehaviour
{
	[SerializeField]
	private DropPattern dropPattern = null;

	private FakeRandom fakeRandom;
	protected void Awake()
	{
		fakeRandom = new FakeRandom(Random.Range(2, dropPattern.ItemChanceDrops.Length - 1), 0, dropPattern.ItemChanceDrops.Length);
	}
	public ItemScriptableObject GetRandomItem ()
	{
		return dropPattern.ItemChanceDrops[fakeRandom.Next()];
	}
}

