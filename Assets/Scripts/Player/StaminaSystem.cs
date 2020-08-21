using Cyberultimate.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaSystem : MonoSingleton<StaminaSystem>
{
	public LockValue Stamina { get; set; }

	protected override void Awake()
	{
		base.Awake();
		Stamina = new LockValue(1000, 0, 1000);
	}
}
