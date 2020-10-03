using Cyberultimate;
using Cyberultimate.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GarbageChecker : MonoSingleton<GarbageChecker>
{
	[SerializeField]
	private Garbage[] garbageArray = null;

	private uint num = 0;

	public void Check(Garbage garbage)
	{
		if (garbageArray.Contains(garbage))
		{
			num++;
		}

		if (num >= (garbageArray.Length / 1.45f))
		{
			TaskManager.Instance.AddTask("Explore the city");
		}
	}
}
