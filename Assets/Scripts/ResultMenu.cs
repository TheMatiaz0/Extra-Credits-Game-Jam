using Cyberultimate.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultMenu : MonoBehaviour
{
	protected void OnEnable()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;

		TimeManager.Instance.StartNewDay();
		TimeControl.Register(this, 0);
	}

	protected void OnDisable()
	{
		TimeControl.Unregister(this);
		GameManager.Instance.LockCursorUp();
	}
}
