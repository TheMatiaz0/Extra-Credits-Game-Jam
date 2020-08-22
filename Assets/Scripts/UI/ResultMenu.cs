using Cyberultimate;
using Cyberultimate.Unity;
using System;
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

	[SerializeField]
	private Text plantStateInfo = null;


	protected void OnEnable()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;

		dayText.text = string.Empty;
		plantInfo.text = string.Empty;
		plantStateInfo.text = string.Empty;
		_ = SlowlyType();
	
		TimeControl.Register(this, 0);
	}

	private async Task SlowlyType ()
	{
		await FilmoqueTyping.SlowlyTypeUnscaled($"Day {TimeManager.Instance.CurrentDay}", 0.105f, dayText);


		await FilmoqueTyping.SlowlyTypeUnscaled(
			$"The plant is... ", 0.15f, plantInfo);


		const float ACTION_TIME = 0.16f;
		for (int x = 0; x < 2f/(ACTION_TIME * 3); x++)
		{
			await FilmoqueTyping.SlowlyTypeUnscaled("...", ACTION_TIME, plantStateInfo);
			plantStateInfo.text = string.Empty;
		}

		plantStateInfo.text = $"...<color=#{ColorUtility.ToHtmlStringRGB(PlantSystem.Instance.GetColorBasedOnState())}>{PlantSystem.Instance.PlantState.ToString().ToUpper()}</color>";
	}


	protected void OnDisable()
	{
		TimeControl.Unregister(this);
		TimeManager.Instance.StartNewDay();
		GameManager.Instance.LockCursorUp();
	}
}
