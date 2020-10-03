using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : EventLogic
{

    public float healthLost = 20;

    public override void Activate()
    {

        var rnd = Random.Range(0, 1);

        var msg = "";

        switch (rnd)
        {
            case 0:
                GameManager.Instance.HealthSys.Health.TakeValue(healthLost, "Event");
                msg = "You felt bad at night - you lost some health.";
                break;
        }

        Debug.Log("plant disease");

        UIManager.Instance.ShowDialogText(msg);
    }
}
