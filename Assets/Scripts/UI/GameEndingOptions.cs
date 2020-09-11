using Cyberultimate.Unity;
using UnityEngine;

public class GameEndingOptions : MonoSingleton<GameEndingOptions>
{
	public GameObject startingCutScene;
	public GameObject beforeEndCutscene;
	public GameObject humanEndingCutscene;
	public GameObject plantEndingCutscene;

	public TimelineController currentTimeline;

	public enum Endings { plant, human }
	public void GameEnding(Endings ending)
	{
		if (ending == Endings.plant)
		{
			SetAllTo(false);
			CanvasManager.Instance.HideEverything();

			currentTimeline = plantEndingCutscene.GetComponent<TimelineController>();
			plantEndingCutscene.SetActive(true);
			plantEndingCutscene.GetComponent<TimelineController>().LaunchCutscene();
			Debug.Log("h");
		}
		else if (ending == Endings.human)
		{
			SetAllTo(false);
			CanvasManager.Instance.HideEverything();

			currentTimeline = humanEndingCutscene.GetComponent<TimelineController>();
			humanEndingCutscene.SetActive(true);
			humanEndingCutscene.GetComponent<TimelineController>().LaunchCutscene();
			Debug.Log("p");
		}
	}

	public void PlantEnding()
	{
		GameEnding(Endings.plant);
	}
	public void HumanEnding()
	{
		GameEnding(Endings.human);
	}

	private void Start()
	{
		SetAllTo(false);
		// specialUI.SetActive(true);
		startingCutScene.SetActive(true);
		currentTimeline = startingCutScene.GetComponent<TimelineController>();
	}

	void SetAllTo(bool what)
	{
		CanvasManager.Instance.HideEverything();
		beforeEndCutscene.SetActive(what);
		plantEndingCutscene.SetActive(what);
		humanEndingCutscene.SetActive(what);
		startingCutScene.SetActive(what);
	}

	public void StartEnding()
	{
		SetAllTo(false);

		currentTimeline = beforeEndCutscene.GetComponent<TimelineController>();
		beforeEndCutscene.SetActive(true);
		currentTimeline.LaunchCutscene();
	}

	public void ChooseEnding()
	{
		CanvasManager.Instance.ShowOnlyEndingCanvas();

		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;

		MovementController.Instance.GetComponent<Collider>().enabled = false;
		MovementController.Instance.enabled = false;
		MouseLook.Instance.enabled = false;
		// CanvasManager.Instance.ShowCutsceneCanvas();
		HomeMusic.Instance.gameObject.SetActive(false);
		TownMusic.Instance.gameObject.SetActive(false);
		AudioManager.Instance.gameObject.SetActive(false);
	}
}
