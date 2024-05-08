using System.Collections;
using UI;
using UI.Scenes;
using UnityEngine;
using UnityEngine.Serialization;

namespace Mission
{
    public class Report : UIElement
    {
        private PlanetScene _planetScene;

        [SerializeField] private ReportInputField[] reportAnswers;

        [SerializeField] private Sprite correctSprite;
        [SerializeField] private Sprite wrongSprite;
        
        //TODO: Hold blanks.
        //TODO: Check the blanks.

        private void Awake()
        {
            _planetScene = GetComponentInParent<PlanetScene>();
            reportAnswers = GetComponentsInChildren<ReportInputField>();
        }

        public void OnReportCompleted()
        {
            if (CheckAnswers()) StartCoroutine(OnReportCompletedRoutine());
        }

        private bool CheckAnswers()
        {
            bool allTrue = true;
            
            foreach (ReportInputField reportAnswer in reportAnswers)
            {
                if (reportAnswer.CheckAnswer())
                {
                    reportAnswer.SetCorrectnessImage(correctSprite, Color.green);
                }
                else
                {
                    reportAnswer.SetCorrectnessImage(wrongSprite, Color.red);
                    allTrue = false;
                }
            }
            
            return allTrue;
        }
        
        private IEnumerator OnReportCompletedRoutine()
        {
            Close();
            
            yield return new WaitForSeconds(1f);
            
            _planetScene.SetMission();
        }
    }
}
