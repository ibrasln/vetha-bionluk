using UnityEngine;

namespace Mission
{
    public class ReportAres : Report
    {
        [SerializeField] private ReportAnswerInputField[] reportAnswers;

        protected override void Awake()
        {
            base.Awake();
            reportAnswers = GetComponentsInChildren<ReportAnswerInputField>();
        }

        protected override void CheckAnswers()
        {
            IsCompleted = true;
            foreach (ReportAnswerInputField reportAnswer in reportAnswers)
            {
                if (reportAnswer.CheckAnswer())
                {
                    reportAnswer.SetCorrectnessImage(correctSprite, Color.green);
                }
                else
                {
                    reportAnswer.SetCorrectnessImage(wrongSprite, Color.red);
                    IsCompleted = false;
                }
            }
        }
    }
}