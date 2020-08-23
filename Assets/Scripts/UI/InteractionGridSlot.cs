using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class InteractionGridSlot : MonoBehaviour
    {
        public string interactionName;
        public Image image;
        public UnityEvent onClick;

        public ScriptableObject itemNeeded=null;


        [SerializeField] private RectTransform bg;

        private void Awake()
        {
            LeanTween.alpha((RectTransform) bg, 0.5f, 0).setEaseInOutBounce();
        }
        
        public void PointerEnter()
        {
            LeanTween.alpha((RectTransform) bg, 1f, 0.2f).setEaseInOutBounce();
            InteractionGrid.Instance.ShowTooltip(interactionName);

            if (itemNeeded != null && !Inventory.Instance.HasItem(itemNeeded.name)) UIManager.Instance.ShowPopupText(itemNeeded.name + " needed!");
        }

        public void PointerExit()
        {
            LeanTween.alpha((RectTransform) bg, 0.5f, 0.2f).setEaseInOutBounce();
            InteractionGrid.Instance.HideTooltip();
        }

        public void PointerDown()
        {
            if (itemNeeded != null && !Inventory.Instance.HasItem(itemNeeded.name)) return;

            onClick?.Invoke();
            InteractionGrid.Instance.Hide();
        }
    }
}