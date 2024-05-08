using System.Collections;
using UI;
using UI.Scenes;
using UnityEngine;

namespace Mission
{
    public class Report : UIElement
    {
        private PlanetScene _planetScene;
        
        //TODO: Implement Drag & Drop.
        //TODO: Hold blanks.
        //TODO: Check the blanks.

        private void Awake()
        {
            _planetScene = GetComponentInParent<PlanetScene>();
        }

        public void OnReportCompleted()
        {
            StartCoroutine(OnReportCompletedRoutine());
        }

        private IEnumerator OnReportCompletedRoutine()
        {
            Close();
            
            yield return new WaitForSeconds(1f);
            
            _planetScene.SetMission();
        }
    }
}
