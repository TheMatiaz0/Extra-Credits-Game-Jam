using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
	[SerializeField]
	private Material cleanSkybox;

	public void SceneChange (string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	public void ChangeSkybox ()
	{
		RenderSettings.skybox = cleanSkybox;
	}
}
