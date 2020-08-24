using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class PlantNeedsUIButton : MonoBehaviour
    {
        public UnityEvent onClick;

        public ScriptableObject itemNeeded=null;
        
        private void Awake()
        {
            LeanTween.alpha((RectTransform) transform, 0.5f, 0).setEaseInOutBounce();
        }
        
        public void PointerEnter()
        {
            LeanTween.alpha((RectTransform) transform, 1f, 0.2f).setEaseInOutBounce();

            if (itemNeeded != null && !Inventory.Instance.HasItem(itemNeeded.name)) UIManager.Instance.ShowPopupText(itemNeeded.name + " needed!");
        }

        public void PointerExit()
        {
            LeanTween.alpha((RectTransform) transform, 0.5f, 0.2f).setEaseInOutBounce();
        }

        public void PointerDown()
        {
            if (itemNeeded != null && !Inventory.Instance.HasItem(itemNeeded.name)) return;

            onClick?.Invoke();
        }
    }
}