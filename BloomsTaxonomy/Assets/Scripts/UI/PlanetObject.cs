using Manager;
using UI.Scenes;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlanetObject : UIElement
    {
        public bool IsLocked;

        [SerializeField] private PlanetScene previousPlanetScene;
        [SerializeField] private PlanetScene planetScene;
        [SerializeField] private Image lockedImage;
        [SerializeField] private Button planetButton;

        private void OnEnable() 
        {
            CheckIfShouldUnlock();
            if (IsLocked) Lock();
            else Unlock();
        }

        private void Lock()
        {
            lockedImage.gameObject.SetActive(true);
            planetButton.interactable = false;
        }

        private void Unlock()
        {
            lockedImage.gameObject.SetActive(false);
            planetButton.interactable = true;
        }

        private void CheckIfShouldUnlock()
        {
            IsLocked = !previousPlanetScene.IsCompleted;
        }
        
        public void EnterPlanet()
        {
            if (planetScene == null) return;
            TransitionManager.Instance.ChangeScene(planetScene);
        }
    }
}
