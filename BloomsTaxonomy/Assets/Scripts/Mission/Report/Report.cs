using System.Collections;
using TMPro;
using UI;
using UI.Scenes;
using UnityEngine;

namespace Mission.Report
{
    public class Report : UIElement
    {
        private PlanetScene _planetScene;

        protected bool isCompleted;
        
        [Space(5)] [Header("REPORT PROPERTIES")]
        [SerializeField] protected Sprite correctSprite;
        [SerializeField] protected Sprite wrongSprite;

        [Space(5)]
        [Header("FEEDBACK PANEL PROPERTIES")]
        [SerializeField] protected UIElement feedbackPanel;
        [SerializeField] protected TextMeshProUGUI feedbackText;
        [SerializeField] protected Sprite correctFeedbackPanelSprite;
        [SerializeField] protected Sprite wrongFeedbackPanelSprite;
        
        protected virtual void Awake()
        {
            _planetScene = GetComponentInParent<PlanetScene>();
        }

        public void OnReportCompleted()
        {
            if (isCompleted) Close();
            else ClosePanel();
        }

        public virtual void CheckAnswers() { }

        public virtual void OpenPanel()
        {
            feedbackPanel.Open();
        }

        private void ClosePanel()
        {
            feedbackPanel.Close();
        }
    }
}
