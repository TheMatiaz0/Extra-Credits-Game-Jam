using Cyberultimate.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseObject : MonoBehaviour
{
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
}
