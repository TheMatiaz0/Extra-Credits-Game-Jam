using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLostEventLogic : EventLogic
{
	public override void Activate()
	{
		var rnd = Random.Range(0, 3);
		var msg = "";

		switch(rnd)
		{
			case 0:
				msg = "Someone has destroyed one of your barricades - you waste time on repair and lose some stamina.";
				GameManager.Instance.StaminaSys.Stamina.TakeValue(10, "Event");
				TimeManager.Instance.CurrentTime = new System.TimeSpan(8, 0, 0);
				break;

			case 1:
				msg = "Someone has destroyed two of your barricades - you waste time on repair and lose some stamina.";
				GameManager.Instance.StaminaSys.Stamina.TakeValue(20, "Event");
				TimeManager.Instance.CurrentTime = new System.TimeSpan(10, 0, 0);
				break;

			case 2:
				msg = "Someone has destroyed three of your barricades - you waste time on repair and lose some stamina.";
				GameManager.Instance.StaminaSys.Stamina.TakeValue(30, "Event");
				TimeManager.Instance.CurrentTime = new System.TimeSpan(12, 0, 0);
				break;

		}

		UIManager.Instance.ShowDialogText(msg);
	}
}
