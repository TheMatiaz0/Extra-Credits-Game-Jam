using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cyberultimate.Unity;

public class PopupText : MonoSingleton<PopupText>
{
    public void ShowText(string txt)
    {
        GetComponent<Text>().text = txt;
        GetComponent<Animator>().SetBool("FadeOut",true);
    }

    public void FadedOut()
    {
        GetComponent<Animator>().SetBool("FadeOut", false);
    }
}
