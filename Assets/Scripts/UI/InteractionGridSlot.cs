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

        [SerializeField] private RectTransform bg;

        private void Awake()
        {
            LeanTween.alpha((RectTransform) bg, 0.5f, 0).setEaseInOutBounce();
        }
        
        public void PointerEnter()
        {
            LeanTween.alpha((RectTransform) bg, 1f, 0.2f).setEaseInOutBounce();
            InteractionGrid.Instance.ShowTooltip(interactionName);
        }

        public void PointerExit()
        {
            LeanTween.alpha((RectTransform) bg, 0.5f, 0.2f).setEaseInOutBounce();
            InteractionGrid.Instance.HideTooltip();
        }

        public void PointerDown()
        {
            onClick?.Invoke();
            InteractionGrid.Instance.Hide();
        }
    }
}