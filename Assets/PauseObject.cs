using Cyberultimate.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseObject : MonoBehaviour
{
	[SerializeField]
	private InputField mouseInput = null;

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


	public void TextSetup (string text)
	{
		int.TryParse(text, out int result);		
		mouseInput.text = result.ToString();
		MouseLook.Instance.MouseSensitivity = result;
	}

}
