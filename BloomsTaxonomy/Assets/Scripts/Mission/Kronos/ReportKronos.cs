using UnityEngine;

namespace Mission
{
    public class ReportKronos : Report
    {
        [SerializeField] private ReportAnswerButton[] answerButtons;

        protected override void Awake()
        {
            base.Awake();
            answerButtons = GetComponentsInChildren<ReportAnswerButton>();
        }

        protected override void CheckAnswers()
        {
            IsCompleted = true;
            
            foreach (ReportAnswerButton reportAnswer in answerButtons)
            {
                if (reportAnswer.IsCorrect)
                {
                    reportAnswer.SetCorrectnessImage(correctSprite, Color.green);
                }
                else
                {
                    reportAnswer.SetCorrectnessImage(wrongSprite, Color.red);
                    IsCompleted = false;
                }
                
                reportAnswer.SetButtonInteractables(true);
                
                Debug.Log("IsCompleted: " + IsCompleted);
            }
        }
    }
}