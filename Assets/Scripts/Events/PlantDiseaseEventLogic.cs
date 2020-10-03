
using UnityEngine;

public class PlantDiseaseEventLogic : EventLogic
{
    public override void Activate()
    {
        var rnd = Random.Range(0, 2);

        var msg = "";

        switch (rnd)
        {
            case 0:
                PlantSystem.Instance.Soil.SetValue(20);
                msg = "Worms got into the plant soil over night!";
                break;
            case 1:
                PlantSystem.Instance.Water.SetValue(20);
                msg = "Your plant has dried up over night!";
                break;
        }
        
        Debug.Log("plant disease");
        
        UIManager.Instance.ShowDialogText(msg);
    }
}
