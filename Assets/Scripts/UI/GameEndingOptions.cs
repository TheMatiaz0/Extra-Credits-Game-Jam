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
    public GameObject UI;

    public enum Endings { plant, human }
    public void GameEnding(Endings ending)
    {
        if (ending == Endings.plant)
        {
            //play plant cutscene
            Debug.Log("p");
        } else if (ending == Endings.human)
        {
            //play human cutscene
            Debug.Log("h");
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
        startingCutScene.SetActive(true);
    }

    void SetAllTo(bool what)
    {
        chooseUI.SetActive(what);
        beforeEndCutscene.SetActive(what);
        plantEndingCutscene.SetActive(what);
        humanEndingCutscene.SetActive(what);
        startingCutScene.SetActive(what);
    }

    public void ChooseEnding()
    {
        SetAllTo(false);

        beforeEndCutscene.SetActive(true);
        beforeEndCutscene.GetComponent<TimelineController>().LaunchCutscene();

        UI.SetActive(false);
        chooseUI.SetActive(true);
    }
}
