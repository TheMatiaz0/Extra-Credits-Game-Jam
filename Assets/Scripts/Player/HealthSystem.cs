using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberultimate;
using Cyberultimate.Unity;

public class HealthSystem : MonoBehaviour
{
	[SerializeField]
	private float startHealth = 100;

	public LockValue<float> Health { get; set; }

	protected void Awake()
	{
		Health = new LockValue<float>(startHealth, 0, startHealth);
	}
}
