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
			}
			else if (Input.GetKeyDown(KeyCode.P))
			{
				PlantSystem.Instance.PlantSize.GiveValue(1);
			}
			else if (Input.GetKeyDown(KeyCode.O))
			{
				PlantSystem.Instance.AddResources(100, PlantSystem.PlantResources.Soil);
				PlantSystem.Instance.AddResources(100, PlantSystem.PlantResources.Light);
				PlantSystem.Instance.AddResources(100, PlantSystem.PlantResources.Water);
			}
			else if (Input.GetKeyDown(KeyCode.L))
			{
				GameEndingOptions.Instance.StartEnding();
			}
			if (Input.GetKey(KeyCode.RightAlt))
			{
				//items:
				if (Input.GetKeyDown(KeyCode.S))
				{
					Inventory.Instance.AddItem(Inventory.Instance.AllGameItems["Shovel"]);
				}
				else if (Input.GetKeyDown(KeyCode.W))
				{
					Inventory.Instance.AddItem(Inventory.Instance.AllGameItems["Bottle"]);
				}

				else if (Input.GetKeyDown(KeyCode.H))
				{
					Inventory.Instance.AddItem(Inventory.Instance.AllGameItems["Broken Shovel"]);
				}

				else if (Input.GetKeyDown(KeyCode.G))
				{
					Inventory.Instance.AddItem(Inventory.Instance.AllGameItems["Bottle With Filter"]);
				}
				else if (Input.GetKeyDown(KeyCode.Semicolon)) 
				{
					Inventory.Instance.AddItem(Inventory.Instance.AllGameItems["Big Bottle With Filter"]);
				}
			}

			if (Input.GetKeyDown(KeyCode.Alpha0))
			{
				MovementController.Instance.moveSpeed = 50;
			}

			if (Input.GetKeyDown(KeyCode.T))
			{
				
			}

#endif
		}
	}
}