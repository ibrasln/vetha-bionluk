using BrunoMikoski.AnimationSequencer;
using Interfaces;
using UnityEngine;

namespace UI
{
    public class UIObject : MonoBehaviour, IActivatable
    {
        [SerializeField] private AnimationSequencerController openAnimation;
        [SerializeField] private AnimationSequencerController closeAnimation;

        private bool _isOpened;
        private bool _isClosed;

        public void Open()
        {
            if (_isOpened) return;
            
            openAnimation.Play();
            _isOpened = true;
            _isClosed = false;
        }
        
        public void Close()
        {
            if (_isClosed) return;
            
            closeAnimation.Play();
            _isClosed = true;
            _isOpened = false;
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
