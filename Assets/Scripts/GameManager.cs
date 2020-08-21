using System;
using Cyberultimate.Unity;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
