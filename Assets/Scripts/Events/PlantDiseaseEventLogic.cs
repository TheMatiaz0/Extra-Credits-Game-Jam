
using UnityEngine;

public class PlantDiseaseEventLogic : EventLogic
{
    public override void Activate()
    {
        var rnd = Random.Range(0, 1);

        var msg = "";

        switch (rnd)
        {
            case 0:
                PlantSystem.Instance.Soil.SetValue(20);
                msg = "Worms got into the plant's soil over night.";
                break;
            case 1:
                PlantSystem.Instance.Soil.SetValue(20);
                msg = "Your plant has dried up over night.";
                break;
        }
        
        UIManager.Instance.ShowDialogText(msg);
    }
}
