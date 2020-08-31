using Cyberultimate;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StaminaItem : ItemLogic
{
	private Cint value = 25;

	public override void Do()
	{
		if (GameManager.Instance.StaminaSys.Stamina.Value < GameManager.Instance.StaminaSys.Stamina.Max)
		{
			removeOnUse = true;
			GameManager.Instance.StaminaSys.Stamina.GiveValue(value, "Item");
			AudioManager.Instance.PlaySFX("eating");
			UIManager.Instance.ShowPopupText("You ate protein bar and gained stamina");

		}
		else
		{
			UIManager.Instance.ShowPopupText("You're already at max stamina");
		}
	}
}
