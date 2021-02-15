using System;
using UnityEngine;

namespace Core.UI
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class View : MonoBehaviour
    {
        [SerializeField] protected bool isPopup = false;

        public bool IsPopup => isPopup;

        public void Setup()
        {
            Initialize();
            HideInstantly();
        }

        protected virtual void Initialize() { }

        protected event Action OnDisplayFinished;
        public bool IsDisplayFinished { get; protected set; }
        protected bool isHideFinished;
        protected virtual void BeforeDisplay() { IsDisplayFinished = false; }
        protected virtual void EndDisplay() { IsDisplayFinished = true; OnDisplayFinished?.Invoke(); }
        protected virtual void BeforeHide() { isHideFinished = false; IsDisplayFinished = false; }
        protected virtual void EndHide() { isHideFinished = true; IsDisplayFinished = false; }

        public void Display()
        {
            gameObject.SetActive(true);
            BeforeDisplay();
            EndDisplay();
        }

        public void DisplayInstantly()
        {
            if (this == null) return;
            gameObject.SetActive(true);
            BeforeDisplay();
            EndDisplay();
        }

        public void Hide()
        {
            BeforeHide();
            EndHide();
            gameObject.SetActive(false);
        }

        public void HideInstantly()
        {
            BeforeHide();
            EndHide();
            gameObject.SetActive(false);
        }
    }
}
