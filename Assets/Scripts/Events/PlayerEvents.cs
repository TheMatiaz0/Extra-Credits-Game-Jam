using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : EventLogic
{

    public float healthLost = 20;
    public float staminaLost = 25;

    public override void Activate()
    {

        var rnd = Random.Range(0, 2);

        var msg = "";

        switch (rnd)
        {
            case 0:
                GameManager.Instance.HealthSys.Health.TakeValue(healthLost, "Event");
                msg = "You felt bad at night - you lost some health.";
                break;

            case 1:
                GameManager.Instance.StaminaSys.Stamina.TakeValue(staminaLost, "Event");
                msg = "You didn't get enough sleep - you lost some stamina.";
                break;
        }

        Debug.Log("plant disease");

        UIManager.Instance.ShowDialogText(msg);
    }
}
