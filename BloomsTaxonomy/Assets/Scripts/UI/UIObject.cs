using BrunoMikoski.AnimationSequencer;
using Interfaces;
using UnityEngine;

namespace UI
{
    public class UIObject : MonoBehaviour, IActivatable
    {
        [Space(5)] [Header("ANIMATION PROPERTIES")]
        public AnimationSequencerController OpenAnimation;
        public AnimationSequencerController CloseAnimation;
        public bool IsOpened;
        public bool IsClosed;

        protected virtual void Start()
        {
            if (OpenAnimation != null)
            {
                OpenAnimation.OnStartEvent.AddListener(OnOpened);
                Debug.Log(name + ": Added OnOpened listener to OpenAnimation");
            }
            else
            {
                Debug.LogWarning(name + ": OpenAnimation is null");
            }

            if (CloseAnimation != null)
            {
                CloseAnimation.OnFinishedEvent.AddListener(OnClosed);
                Debug.Log(name + ": Added OnClosed listener to CloseAnimation");
            }
            else
            {
                Debug.LogWarning(name + ": CloseAnimation is null");
            }
        }

        public void Open()
        {
            if (OpenAnimation is null) return;
            
            OpenAnimation.Play();
        }
        
        public void Close()
        {
            if (CloseAnimation is null) return;

            CloseAnimation.Play();
        }

        public void OnOpened()
        {
            Debug.Log(name + " is opened!");
            IsOpened = true;
            IsClosed = false;
        }

        public void OnClosed()
        {
            Debug.Log(name + " is closed!");
            IsClosed = true;
            IsOpened = false;
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
