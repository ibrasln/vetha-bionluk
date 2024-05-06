using Manager;
using UI.Scenes;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlanetObject : UIElement
    {
        public bool IsLocked;
        [SerializeField] private Image lockedImage;
        [SerializeField] private Button planetButton;

        private void OnEnable() 
        {
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

        public void EnterPlanet(UIScene planetScene)
        {
            if (planetScene == null) return;
            TransitionManager.Instance.ChangeScene(planetScene);
        }
    }
}
