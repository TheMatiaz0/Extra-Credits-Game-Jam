using Cyberultimate;
using Cyberultimate.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

	private Coroutine slowlyType = null;

    public AudioClip growingSound;
    public AudioClip dyingSound;

	protected void OnEnable()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;

		dayText.text = string.Empty;
		plantInfo.text = string.Empty;
		plantStateInfo.text = string.Empty;
		slowlyType = StartCoroutine(SlowlyType());

		TimeControl.Register(this, 0);
	}

	private IEnumerator SlowlyType()
	{
		yield return StartCoroutine(SlowlyTypeUnscaled($"Day {TimeManager.Instance.CurrentDay}", 0.105f, dayText));


		yield return StartCoroutine(SlowlyTypeUnscaled(
			$"The plant is... ", 0.15f, plantInfo));


		const float ACTION_TIME = 0.16f;
		for (int x = 0; x < 2f / (ACTION_TIME * 3); x++)
		{
			yield return StartCoroutine(SlowlyTypeUnscaled("...", ACTION_TIME, plantStateInfo));
			plantStateInfo.text = string.Empty;
		}

		plantStateInfo.text = $"...<color=#{ColorUtility.ToHtmlStringRGB(PlantSystem.Instance.GetColorBasedOnState())}>{PlantSystem.Instance.PlantState.ToString().ToUpper()}</color>";
        AudioManager.Instance.PlayClip((PlantSystem.Instance.PlantState == PlantSystem.State.Growing) ? growingSound : dyingSound);
	}

	public IEnumerator SlowlyTypeUnscaled(string text, float cooldown, Text displayText)
	{
		foreach (char c in text)
		{
			displayText.text += c;
			yield return Async.WaitUnscaled(cooldown);
		}
	}


	protected void OnDisable()
	{
		TimeControl.Unregister(this);
		TimeManager.Instance.StartNewDay();
		GameManager.Instance.LockCursorUp();
	}
}
