using System.Collections;
using UI;
using UI.Scenes;
using UnityEngine;

namespace Mission
{
    public class Report : UIElement
    {
        private PlanetScene _planetScene;

        public bool IsCompleted;
        
        [SerializeField] protected Sprite correctSprite;
        [SerializeField] protected Sprite wrongSprite;

        protected virtual void Awake()
        {
            _planetScene = GetComponentInParent<PlanetScene>();
        }

        public void OnReportCompleted()
        {
            CheckAnswers();
            if (IsCompleted) StartCoroutine(OnReportCompletedRoutine());
        }

        protected virtual void CheckAnswers() { }
        
        private IEnumerator OnReportCompletedRoutine()
        {
            Close();
            
            yield return new WaitForSeconds(1.5f);
            
            _planetScene.SetMission();
        }
    }
}
