using Cyberultimate.Unity;
using System.Collections;
using System.Collections.Generic;
using Cyberultimate;
using UnityEngine;

public class StaminaSystem : MonoBehaviour
{
	public LockValue<float> Stamina { get; set; }

	protected void Awake()
	{
		Stamina = new LockValue<float>(100, 0, 100);
	}
}
