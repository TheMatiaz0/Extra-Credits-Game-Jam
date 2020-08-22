using Cyberultimate.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultMenu : MonoBehaviour
{
	protected void OnEnable()
	{
		TimeManager.Instance.StartNewDay();
		TimeControl.Register(this, 0);

		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	protected void OnDisable()
	{
		TimeControl.Unregister(this);
	}
}
