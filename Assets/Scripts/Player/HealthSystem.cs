using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberultimate;
using Cyberultimate.Unity;

public class HealthSystem : MonoSingleton<HealthSystem>
{
	public LockValue<float> Health { get; set; }

	protected override void Awake()
	{
		base.Awake();
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
