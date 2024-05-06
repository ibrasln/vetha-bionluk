using BrunoMikoski.AnimationSequencer;
using Interfaces;
using UnityEngine;

namespace UI
{
    public class UIObject : MonoBehaviour, IActivatable
    {
        public AnimationSequencerController OpenAnimation;
        public AnimationSequencerController CloseAnimation;

        public bool IsOpened;
        public bool IsClosed;

        public void Open()
        {
            if (IsOpened) return;
            
            OpenAnimation.Play();
        }
        
        public void Close()
        {
            if (IsClosed) return;
            
            CloseAnimation.Play();
        }

        public void OnOpened()
        {
            IsOpened = true;
            IsClosed = false;
        }

        public void OnClosed()
        {
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
