using System;
using System.Collections;
using NaughtyAttributes;
using TMPro;
using UI;
using UI.Scenes;
using UnityEngine;
using UnityEngine.UI;

namespace Mission.Report
{
    public class Report : UIElement
    {
        [ReadOnly] public PlanetScene PlanetScene;
        private Button _backButton;

        protected bool isCompleted;
        
        [Space(5)] [Header("REPORT PROPERTIES")]
        [SerializeField] protected Sprite correctSprite;
        [SerializeField] protected Sprite wrongSprite;
        [SerializeField] protected string correctFeedbackText;
        [SerializeField] protected string wrongFeedbackText;
        
        [Space(5)]
        [Header("FEEDBACK PANEL PROPERTIES")]
        [SerializeField] protected UIElement feedbackPanel;
        [SerializeField] protected TextMeshProUGUI feedbackText;
        [SerializeField] protected Sprite correctFeedbackPanelSprite;
        [SerializeField] protected Sprite wrongFeedbackPanelSprite;
        
        protected virtual void Awake()
        {
            PlanetScene = GetComponentInParent<PlanetScene>();
            _backButton = transform.Find("BackButton").GetComponent<Button>();
        }

        protected override void Start()
        {
            base.Start();
        }

        public void OnReportCompleted()
        {
            StartCoroutine(OnReportCompletedRoutine());
        }

        private IEnumerator OnReportCompletedRoutine()
        {
            ClosePanel();
            
            if (isCompleted) Close();

            yield return new WaitForSeconds(1.25f);

            if (!isCompleted) yield break;
            
            OnClosed();
            
            Debug.Log("Report has been added to reports panel!");
                
            transform.SetParent(UIObjects.Instance.ReportsPanelWindow.ReportsParent);

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

        public void SetBackButtonActive(bool state) => _backButton.gameObject.SetActive(state);
    }
}
