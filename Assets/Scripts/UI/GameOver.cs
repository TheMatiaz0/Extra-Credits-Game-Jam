using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class GameOver : MonoBehaviour
    {
        private CanvasGroup canvas;
        [SerializeField] private Text reasonText;

        private bool loaded = false;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            canvas = GetComponent<CanvasGroup>();
            canvas.alpha = 0f;
        }

        public void Start()
        {
            LeanTween.alphaCanvas(canvas, 1f, 1f).setEaseInOutCubic().setOnComplete(_ => loaded = true);
            reasonText.text = PlayerPrefs.GetString("GameOverReason");
        }

        private void Update()
        {
            if (Input.anyKeyDown && loaded)
            {
                loaded = false;
                LeanTween.alphaCanvas(canvas, 0, 1f).setEaseInOutCubic().setOnComplete(GoToMenu);
            }
        }

        private void GoToMenu()
        {
            SceneManager.LoadScene("Menu");
        }
    }
}