using System;
using Cyberultimate.Unity;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlantNeedsUI : MonoSingleton<PlantNeedsUI>
    {
        private CanvasGroup canvas;
        private bool Visible => canvas.alpha > 0;

        [SerializeField] private Image sunlight, soil, water;
            
        public int maximumNeedsToCompleteTask = 60;

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
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            
            sunlight.fillAmount = 0;
            soil.fillAmount = 0;
            water.fillAmount = 0;
            
            canvas.blocksRaycasts = true;

            LeanTween.alphaCanvas(canvas, 1f, 0.2f).setOnComplete(_ => AnimateResources());
        }

        public void Hide(bool enableCheckInteractions=true)
        {
            if(enableCheckInteractions) InteractionChecker.Instance.checkInteractions = true;
            MouseLook.Instance.BlockAiming = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            canvas.blocksRaycasts = false;

            var plant = PlantSystem.Instance;
            var task = TaskManager.Instance;
            
            LeanTween.alphaCanvas(canvas, 0, 0.2f).setOnComplete(_ =>
            {
                if (plant.Water.Value <= maximumNeedsToCompleteTask) task.AddTask("Get water for plant"); else task.RemoveTask("Get water for plant");
                if (plant.Soil.Value <= maximumNeedsToCompleteTask) task.AddTask("Get soil for plant"); else task.RemoveTask("Get soil for plant");
                if (plant.Sunlight.Value <= maximumNeedsToCompleteTask) task.AddTask("Give the plant some sunlight"); else task.RemoveTask("Give the plant some sunlight");
            });
        }
        
        public void AnimateResources()
        {
            var p = PlantSystem.Instance;
            
            LeanTween.value(sunlight.gameObject, (x) => sunlight.fillAmount = x,  0f, (float)p.Sunlight.Value / 100f, time);
            LeanTween.value(soil.gameObject, (x) => soil.fillAmount = x,  0f, (float)p.Soil.Value / 100f, time);
            LeanTween.value(water.gameObject, (x) => water.fillAmount = x,  0f, (float)p.Water.Value / 100f, time);
        }

        public void FilWater()
        {
            if (Inventory.Instance.water.Value == 0)
            {
                UIManager.Instance.ShowPopupText("You don't have any water!");
                return;
            }
            
            PlantSystem.Instance.AddResources(Inventory.Instance.water.Value, PlantSystem.PlantResources.Water);
            Inventory.Instance.DrainWater();
            AnimateResources();
        }

        public void FillSoil()
        {
            if (Inventory.Instance.soil.Value == 0)
            {
                UIManager.Instance.ShowPopupText("You don't have any soil!");
                return;
            }
            
            PlantSystem.Instance.AddResources(Inventory.Instance.soil.Value, PlantSystem.PlantResources.Soil);

            Inventory.Instance.DrainSoil();
            AnimateResources();
        }

        public void PickUpPlant()
        {
            Hand.Instance.PickUp(PlantSystem.Instance.transform);
            Hide(false);
        }

        private int lightStaminaDrain = 5;
        private int lightTimeSkip = 20;
        public void LightPlant()
        {
            GameManager.Instance.StaminaSys.Stamina.TakeValue(lightStaminaDrain);
            TimeManager.Instance.CurrentTime.Add(TimeSpan.FromMinutes(lightTimeSkip));
            Hide();
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