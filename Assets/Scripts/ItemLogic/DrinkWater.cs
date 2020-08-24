using Cyberultimate;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkWater : ItemLogic
{
	public override void Do()
	{
		uint water = Inventory.Instance.Water.Value;
		if (water > 0 && GameManager.Instance.HealthSys.Health.Value < GameManager.Instance.HealthSys.Health.Max)
		{
			removeOnUse = true;
			GameManager.Instance.HealthSys.Health.GiveValue(water * 7, "Item");
			UIManager.Instance.ShowPopupText("You drinked fresh water and healed yourself");
			AudioManager.Instance.PlaySFX("drinking");

		}
		else if (water <= 0)
		{
			UIManager.Instance.ShowPopupText("You don't have any water to drink");
		}
		else if (GameManager.Instance.HealthSys.Health.Value < GameManager.Instance.HealthSys.Health.Max)
		{
			UIManager.Instance.ShowPopupText("You're already at max health");
		}
	}
}
