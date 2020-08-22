using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class Menu : MonoBehaviour
    {
        public CanvasGroup canvas;

        private bool loaded = false;
        
        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            canvas = GetComponent<CanvasGroup>();
            canvas.alpha = 0f;

            LeanTween.alphaCanvas(canvas, 1f, 1f).setEaseInOutCubic().setOnComplete(_ => loaded = true);
        }

        private void Update()
        {
            if (!loaded) return;
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
                loaded = false;
                LeanTween.alphaCanvas(canvas, 0, 1f).setEaseInOutCubic().setOnComplete(StartGame);
            }
        }

        private void StartGame()
        {
            SceneManager.LoadScene("Game");
        }
    }
}