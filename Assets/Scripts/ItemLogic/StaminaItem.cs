using Cyberultimate;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StaminaItem : ItemLogic
{
    [SerializeField]
    protected Cint value;

    public override void Do()
	{
		GameManager.Instance.StaminaSys.Stamina.GiveValue(value, "Item");
		AudioManager.Instance.PlaySFX("eating");
	}
}
