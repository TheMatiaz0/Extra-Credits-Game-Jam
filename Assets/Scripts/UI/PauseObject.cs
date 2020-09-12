using System;
using Cyberultimate.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
		if (AudioManager.Instance != null)
		{
			AudioManager.Instance.SfxSource.Pause();
			AudioManager.Instance.SfxSource.volume = 0;
		}

	}

	protected void OnDisable()
	{
		TimeControl.Unregister(this);
		if (AudioManager.Instance != null)
		{
			AudioManager.Instance.SfxSource.UnPause();
			AudioManager.Instance.SfxSource.volume = 1;
		}

	}

	public void GoMainMenu()
	{
		this.gameObject.SetActive(false);
		SceneManager.LoadScene("Menu");
	}

	public void Resume()
	{
		gameObject.SetActive(false);
		GameManager.Instance.LockCursorUp();
	}

	public void OnSliderValueChanged(float v)
	{
		MouseLook.Instance.MouseSensitivity = v;
	}

}
