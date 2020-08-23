using System;
using Cyberultimate.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlantNeedsUI : MonoSingleton<PlantNeedsUI>
    {
        private CanvasGroup canvas;
        private bool Visible => canvas.alpha > 0;

        [SerializeField] private Image sunlight, soil, water;

        protected override void Awake()
        {
            base.Awake();
            canvas = GetComponent<CanvasGroup>();
            canvas.alpha = 0;
        }
        
        private const float time = 0.7f;
        public void Show()
        {
            InteractionChecker.Instance.checkInteractions = false;
            MouseLook.Instance.BlockAiming = true;

            var p = PlantSystem.Instance;
            
            sunlight.fillAmount = 0;
            soil.fillAmount = 0;
            water.fillAmount = 0;
            
            LeanTween.alphaCanvas(canvas, 1f, 0.2f).setOnComplete(_ =>
            {
                LeanTween.value(sunlight.gameObject, (x) => sunlight.fillAmount = x,  0f, (float)p.Sunlight.Value / 100f, time);
                LeanTween.value(soil.gameObject, (x) => soil.fillAmount = x,  0f, (float)p.Soil.Value / 100f, time);
                LeanTween.value(water.gameObject, (x) => water.fillAmount = x,  0f, (float)p.Water.Value / 100f, time);
            });
        }

        public void Hide()
        {
            LeanTween.alphaCanvas(canvas, 0, 0.2f);
            InteractionChecker.Instance.checkInteractions = true;
            MouseLook.Instance.BlockAiming = false;
        }
        
        private void Update()
        {
            if (Visible && Input.GetKeyDown(KeyCode.E))
            {
                Hide();
            }
        }
    }
}