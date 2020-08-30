using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField]
    private GameObject mainPauseObject = null;

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameEndingOptions.Instance.currentTimeline.CutsceneRunning)
		{
			mainPauseObject.SetActive(!mainPauseObject.activeSelf);
		}
    }
}
