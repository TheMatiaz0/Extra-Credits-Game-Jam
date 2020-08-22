using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedInteractable : InteractableObject
{
	protected override void OnInteract()
	{
		if (TimeManager.Instance.CurrentTime.Hours >= 20)
		{
			TimeManager.Instance.SkipDay();
		}

		else
		{
			UIManager.Instance.ShowPopupText("You can't sleep now");
		}
	}
}
