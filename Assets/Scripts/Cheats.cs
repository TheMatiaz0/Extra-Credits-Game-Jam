using UnityEngine;

namespace DefaultNamespace
{
	public class Cheats : MonoBehaviour
	{
		public AudioClip testClip;
		private int n = 0;
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
			else if (Input.GetKeyDown(KeyCode.B))
			{
				UIManager.Instance.ShowDialogText($"test{++n}", clip: testClip);
			}
			if (Input.GetKey(KeyCode.RightAlt))
			{
				//items:
				if (Input.GetKeyDown(KeyCode.S))
				{
					Inventory.Instance.AddItem(GarbageManager.Instance.shovelEvolution[0]);
				}
				else if (Input.GetKeyDown(KeyCode.W))
				{
					Inventory.Instance.AddItem(GarbageManager.Instance.bottleEvolution[0]);
				}
			}
#endif
		}
	}
}