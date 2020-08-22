using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberultimate;
using Cyberultimate.Unity;

public class HealthSystem : MonoBehaviour
{
	public LockValue<float> Health { get; set; }

	protected void Awake()
	{
		Health = new LockValue<float>(100, 0, 100);
	}

	protected void Update()
	{
		if (Input.GetKeyDown(KeyCode.J))
		{
			Health.TakeValue(10f, "Whoooo");
		}
	}

}
