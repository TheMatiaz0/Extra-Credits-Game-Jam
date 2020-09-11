using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField]
    private GameObject mainPauseObject = null;

    private void Awake()
    {
	    mainPauseObject.SetActive(false);
    }

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameEndingOptions.Instance.currentTimeline.CutsceneRunning)
        {
	        var isPause = !mainPauseObject.activeSelf;
			mainPauseObject.SetActive(isPause);
			GameManager.Instance.SetCursorLock(!isPause);
		}
    }
}
