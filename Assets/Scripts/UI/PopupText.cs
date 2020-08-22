using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupText : MonoBehaviour
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
