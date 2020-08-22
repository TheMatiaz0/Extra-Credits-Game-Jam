using Cyberultimate;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StaminaItem : ItemLogic
{
	[SerializeField]
	private Cint staminaRestore;

	public override void Do()
	{
		GameManager.Instance.StaminaSys.Stamina.GiveValue(staminaRestore, "FoodItem");
	}
}
