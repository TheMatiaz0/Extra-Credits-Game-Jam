using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedInteractable : InteractableObject
{
	public override string InteractionName => "Sleep";

	protected override void OnInteract()
	{
		if (TimeManager.Instance.CurrentTime.Hours >= 18)
		{
			TimeManager.Instance.SkipDay();
		}

		else
		{
			UIManager.Instance.ShowPopupText("You can't sleep now");
		}
	}
}
