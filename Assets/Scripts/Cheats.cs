using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Cheats : MonoBehaviour
    {
        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.N))
            {
                TimeManager.Instance.SkipDay();
            } else if (Input.GetKeyDown(KeyCode.P))
            {
                PlantSystem.Instance.PlantSize.GiveValue(1);
            } else if (Input.GetKeyDown(KeyCode.O))
            {
                PlantSystem.Instance.AddResources(100, PlantSystem.PlantResources.Soil);
                PlantSystem.Instance.AddResources(100, PlantSystem.PlantResources.Light);
                PlantSystem.Instance.AddResources(100, PlantSystem.PlantResources.Water);
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                GameEndingOptions.Instance.ChooseEnding();
            }
#endif
        }
    }
}