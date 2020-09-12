using Cyberultimate;
using Cyberultimate.Unity;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
	private PlayableDirector director;

	[SerializeField]
	private GameObject pressToSkip = null;

	public bool CutsceneRunning { get; private set; } = true;

	[SerializeField]
	private bool shouldShowCutsceneCanvas = true;

	[SerializeField]
	private UnityEvent onCutsceneEnd;

	[SerializeField]
	private UnityEvent onCutsceneStart;

	protected void OnEnable()
	{
		TimeControl.Register(this, 0);
	}

	protected void OnDisable()
	{
		TimeControl.Unregister(this);
	}


	protected void Start()
	{
		director = GetComponent<PlayableDirector>();
	}

	public void LaunchCutscene()
	{
		Camera.main.transform.rotation = new Quaternion();
		MovementController.Instance.GetComponent<Collider>().enabled = false;
		MovementController.Instance.enabled = false;
		MouseLook.Instance.enabled = false;
		if (shouldShowCutsceneCanvas)
		{
			CanvasManager.Instance.ShowCutsceneCanvas();
		}
		HomeMusic.Instance.gameObject.SetActive(false);
		TownMusic.Instance.gameObject.SetActive(false);
		AudioManager.Instance.gameObject.SetActive(false);
		onCutsceneStart.Invoke();
		CutsceneRunning = true;
	}

	public void CloseCutscene()
	{
		MovementController.Instance.enabled = true;
		MouseLook.Instance.enabled = true;
		if (shouldShowCutsceneCanvas)
		{
			CanvasManager.Instance.ShowMainCanvas();
		}
		MovementController.Instance.GetComponent<Collider>().enabled = true;
		HomeMusic.Instance.gameObject.SetActive(true);
		TownMusic.Instance.gameObject.SetActive(true);
		AudioManager.Instance.gameObject.SetActive(true);
		onCutsceneEnd.Invoke();
		CutsceneRunning = false;
		this.gameObject.SetActive(false);
	}

	protected async void Update()
	{
		if (CutsceneRunning == false)
		{
			return;
		}

		if (Input.GetKeyDown(KeyCode.E))
		{
			director.time = director.playableAsset.duration;
			await Async.Wait(TimeSpan.FromSeconds(2));
			// CloseCutscene();
		}

		if (Input.anyKeyDown == true)
		{
			pressToSkip.SetActive(true);
		}
	}
}
