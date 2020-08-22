using Cyberultimate.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ResultMenu : MonoBehaviour
{
	[SerializeField]
	private Text dayText = null;
	[SerializeField]
	private Text plantInfo = null;


	protected void OnEnable()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;


		_ = SlowlyType();
		

		TimeManager.Instance.StartNewDay();
		TimeControl.Register(this, 0);
	}

	private async Task SlowlyType ()
	{
		await FilmoqueTyping.SlowlyType($"Day {TimeManager.Instance.CurrentDay}", 0.105f, dayText);
		await FilmoqueTyping.SlowlyType(
			$"The plant is... <color={ColorUtility.ToHtmlStringRGB(PlantSystem.Instance.GetColorBasedOnState())}>{PlantSystem.Instance.PlantState}</color>", 0.15f, plantInfo);
	}

	protected void OnDisable()
	{
		TimeControl.Unregister(this);
		GameManager.Instance.LockCursorUp();
	}
}
