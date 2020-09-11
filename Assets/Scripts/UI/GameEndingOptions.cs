using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberultimate.Unity;

public class GameEndingOptions : MonoSingleton<GameEndingOptions>
{
    public GameObject startingCutScene;
    public GameObject beforeEndCutscene;
    public GameObject humanEndingCutscene;
    public GameObject plantEndingCutscene;
    public GameObject chooseUI;
    public GameObject specialUI;
    public GameObject UI;

    public TimelineController currentTimeline;

    public enum Endings { plant, human }
    public void GameEnding(Endings ending)
    {
        MovementController.Instance.transform.localEulerAngles = Vector3.zero;
        if (ending == Endings.plant)
        {
            SetAllTo(false);
            specialUI.SetActive(true);

            currentTimeline = plantEndingCutscene.GetComponent<TimelineController>();
            plantEndingCutscene.SetActive(true);
            plantEndingCutscene.GetComponent<TimelineController>().LaunchCutscene();
        } else if (ending == Endings.human)
        {
            SetAllTo(false);
            specialUI.SetActive(true);

            currentTimeline = humanEndingCutscene.GetComponent<TimelineController>();
            humanEndingCutscene.SetActive(true);
            humanEndingCutscene.GetComponent<TimelineController>().LaunchCutscene();
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
        specialUI.SetActive(true);
        startingCutScene.SetActive(true);
        currentTimeline = startingCutScene.GetComponent<TimelineController>();
    }

    void SetAllTo(bool what)
    {
        chooseUI.SetActive(what);
        beforeEndCutscene.SetActive(what);
        plantEndingCutscene.SetActive(what);
        humanEndingCutscene.SetActive(what);
        startingCutScene.SetActive(what);
        specialUI.SetActive(what);
    }

    public void StartEnding()
    {
        SetAllTo(false);
        specialUI.SetActive(true);

        currentTimeline = beforeEndCutscene.GetComponent<TimelineController>();
        beforeEndCutscene.SetActive(true);
        beforeEndCutscene.GetComponent<TimelineController>().LaunchCutscene();

        UI.SetActive(false);
    }

    public void ChooseEnding()
    {
        chooseUI.SetActive(true);
        specialUI.SetActive(true);

        MouseLook.Instance.BlockAiming = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        MovementController.Instance.GetComponent<Collider>().enabled = false;
        MovementController.Instance.enabled = false;
        MouseLook.Instance.enabled = false;
        CanvasManager.Instance.ShowCutsceneCanvas();
        HomeMusic.Instance.gameObject.SetActive(false);
        TownMusic.Instance.gameObject.SetActive(false);
        AudioManager.Instance.gameObject.SetActive(false);
    }
}
