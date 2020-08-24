using Cyberultimate.Unity;
using System.Collections;
using System.Collections.Generic;
using Cyberultimate;
using UnityEngine;

public class StaminaSystem : MonoBehaviour
{
	[SerializeField]
	private float startStamina = 100;
	public LockValue<float> Stamina { get; set; }

	protected void Awake()
	{
		Stamina = new LockValue<float>(startStamina, 0, startStamina);
	}
}
