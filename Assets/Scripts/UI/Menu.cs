using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class Menu : MonoBehaviour
    {
        public CanvasGroup canvas;
        private void Start()
        {
            canvas = GetComponent<CanvasGroup>();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
#if UNITY_STANDALONE
                Application.Quit();
#endif
                
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            } else if (Input.anyKeyDown)
            {
                LeanTween.alphaCanvas(canvas, 0, 1f).setEaseInOutCubic().setOnComplete(StartGame);
            }
        }

        private void StartGame()
        {
            SceneManager.LoadScene("Game");
        }
    }
}