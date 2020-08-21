using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberultimate;
using Cyberultimate.Unity;

public class HealthSystem : MonoSingleton<HealthSystem>
{
	public LockValue Health { get; set; }

	protected override void Awake()
	{
		base.Awake();
		Health = new LockValue(100, 0, 100);
	}

	protected void Update()
	{
		if (Input.GetKeyDown(KeyCode.J))
		{
			Health.Take(10, "Whoooo");
		}
	}

}
