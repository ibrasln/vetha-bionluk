using System;
using System.Collections;
using DG.Tweening;
using Interfaces;
using NaughtyAttributes;
using UI;
using UI.Scenes;
using UnityEngine;
using Utilities;

namespace Manager
{
    public class TransitionManager : MySingleton<TransitionManager>, IInitializable
    {
        [ReadOnly] public UIScene CurrentScene;
        [ReadOnly] public UIScene PreviousScene;
        [SerializeField] private UIScene fadeScreenScene;
        [SerializeField] private CanvasGroup fadeScreen;
        [SerializeField] private BeginningScene beginningScene;

        #region Events
        public Action<UIScene> OnSceneChanged;
        public Action<UIWindow> OnWindowAdded;
        public Action<UIWindow> OnWindowClosed;
        #endregion
        
        public void Initialize()
        {
            StartCoroutine(InitializeRoutine());
        }

        private IEnumerator InitializeRoutine()
        {
            beginningScene.Activate();
            fadeScreenScene.Activate();
            fadeScreen.DOFade(0f, 1f);
            CurrentScene = beginningScene;
            
            yield return new WaitForSeconds(1f);

            yield return StartCoroutine(beginningScene.InitializeRoutine());
            
            fadeScreenScene.Deactivate();
        }
        
        public void ChangeScene(UIScene scene)
        {
            StartCoroutine(ChangeSceneRoutine(scene));
        }

        private IEnumerator ChangeSceneRoutine(UIScene scene)
        {
            fadeScreenScene.Activate();
            fadeScreen.DOFade(1, .75f);
            
            yield return new WaitForSeconds(1f);
            
            CurrentScene.Deactivate();
            PreviousScene = CurrentScene;
            CurrentScene = scene;
            CurrentScene.Activate();
            fadeScreen.DOFade(0, .75f);
            
            yield return new WaitForSeconds(1f);
            
            fadeScreenScene.Deactivate();
            OnSceneChanged?.Invoke(CurrentScene);
        }

        public void AddWindow(UIWindow window)
        {
            StartCoroutine(AddWindowRoutine(window));
        }

        private IEnumerator AddWindowRoutine(UIWindow window)
        {
            fadeScreenScene.Activate();
            fadeScreen.DOFade(.5f, .5f);
            window.Activate();
            
            yield return new WaitForSeconds(.5f);
            
            OnWindowAdded?.Invoke(window);
        }
        
        public void CloseWindow(UIWindow window)
        {
            StartCoroutine(CloseWindowRoutine(window));
        }

        private IEnumerator CloseWindowRoutine(UIWindow window)
        {
            fadeScreen.DOFade(0f, .5f);
            
            yield return new WaitForSeconds(.5f);
            
            fadeScreenScene.Deactivate();
            window.Deactivate();
            OnWindowClosed?.Invoke(window);
        }
    }
}
