using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nullable : ItemLogic
{
	public Nullable(Item item) : base(item)
	{
	}
	
	protected override void OnUse()
	{
		removeOnUse = false;
	}
}
