using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupText : MonoBehaviour
{
    public void ShowText(string txt, float duration)
    {
        GetComponent<Text>().text = txt;
        GetComponent<Animator>().Play("PopupFadeOut");
    }
}
