using Cyberultimate.Unity;
using System.Collections;
using System.Collections.Generic;
using Cyberultimate;
using UnityEngine;

public class StaminaSystem : MonoSingleton<StaminaSystem>
{
	public LockValue<float> Stamina { get; set; }

	protected override void Awake()
	{
		base.Awake();
		Stamina = new LockValue<float>(100, 0, 100);
	}
}
