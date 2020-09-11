using System;
using Cyberultimate.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseObject : MonoBehaviour
{
	[SerializeField]
	private Slider mouseSensitivity = null;

	private void Awake()
	{
		mouseSensitivity.value = MouseLook.Instance.MouseSensitivity;
		mouseSensitivity.onValueChanged.AddListener(OnSliderValueChanged);
	}

	protected void OnEnable()
	{
		TimeControl.Register(this, 0);
		GameManager.Instance.UnlockCursor();
	}

	protected void OnDisable()
	{
		TimeControl.Unregister(this);
		GameManager.Instance.LockCursorUp();
	}

	public void GoMainMenu ()
	{
		this.gameObject.SetActive(false);
		SceneManager.LoadScene("Menu");
	}

	public void Resume ()
	{
		this.gameObject.SetActive(false);
	}

	public void OnSliderValueChanged(float v)
	{
		MouseLook.Instance.MouseSensitivity = v;
	}

}
