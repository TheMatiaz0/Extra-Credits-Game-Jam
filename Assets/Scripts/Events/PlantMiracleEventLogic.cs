using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantMiracleEventLogic : EventLogic
{
    public override void Activate()
    {
        var rnd = Random.Range(0, 1);

        var msg = "";

        switch (rnd)
        {
            case 0:
                PlantSystem.Instance.Soil.GiveValue(10);
                PlantSystem.Instance.Water.GiveValue(10);
                msg = "That's strange, but the plant was suddenly improved";
                break;
        }

        UIManager.Instance.ShowDialogText(msg);
    }
}
