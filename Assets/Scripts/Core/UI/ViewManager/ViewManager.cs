using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public class ViewManager : MonoBehaviour, IViewManager
    {
        [SerializeField] private View[] viewsReferences;

        private Dictionary<Type, View> viewsDictionary = new Dictionary<Type, View>();
        private View currentView;
        private View currentPopupView;
        private View previousView;

        private Canvas canvas;
        private bool isInitialized = false;

        public void Initialize()
        {
            if (isInitialized) return;

            canvas = GetComponent<Canvas>();

            foreach (var view in viewsReferences)
            {
                viewsDictionary.Add(view.GetType(), view);
                view.Setup();
            }
            isInitialized = true;
        }

        private void PlaySwitchSequence(View viewToHide, View viewToDisplay)
        {
            if (!viewToDisplay.IsPopup)
            {
                viewToHide.Hide();
            }
            if (!viewToHide.IsPopup)
            {
                if (!viewToDisplay.IsPopup) { viewToDisplay.transform.SetAsFirstSibling(); }
                else { viewToDisplay.transform.SetAsLastSibling(); }
                viewToDisplay.Display();
            }
        }

        public T GetView<T>() where T : View
        {
            Type givenType = typeof(T);

            if (viewsDictionary.ContainsKey(givenType))
            {
                return viewsDictionary[givenType] as T;
            }

            return default;
        }

        public T SwitchView<T>() where T : View
        {
            var newView = GetView<T>();
            if (currentView == null)
            {
                currentView = newView;
                currentView?.DisplayInstantly();
                return currentView as T;
            }
            if (newView == currentView) return currentView as T;

            if (newView.IsPopup)
            {
                currentPopupView = newView;
                PlaySwitchSequence(currentView, currentPopupView);
                return currentPopupView as T;
            }
            else
            {
                previousView = currentView;
                currentView = newView;
                PlaySwitchSequence(previousView, currentView);
                return currentView as T;
            }
        }

        public void SetRenderCamera(UnityEngine.Camera camera)
        {
            canvas.worldCamera = camera;
        }

        public View GetCurrentView()
        {
            return currentView;
        }

        public View GetPreviousView()
        {
            return previousView;
        }
    }
}
