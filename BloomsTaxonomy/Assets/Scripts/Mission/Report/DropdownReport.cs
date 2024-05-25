using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mission.Report
{
    public class DropdownReport : Report
    {
        [SerializeField] private ReportDropdownObject[] dropdownAnswers;
        
        public override void CheckAnswers()
        {
            isCompleted = true;
            
            foreach (ReportDropdownObject dropdownAnswer in dropdownAnswers)
            {
                if (dropdownAnswer.IsCorrect)
                {
                    dropdownAnswer.SetCorrectnessImage(correctSprite, Color.green);
                }
                else
                {
                    dropdownAnswer.SetCorrectnessImage(wrongSprite, Color.red);
                    isCompleted = false;
                }
            }
        }

        public override void OpenPanel()
        {
            base.OpenPanel();
            if (isCompleted)
            {
                feedbackPanel.GetComponent<Image>().sprite = correctFeedbackPanelSprite;
                feedbackText.text = correctFeedbackText;
            }
            else
            {
                feedbackPanel.GetComponent<Image>().sprite = wrongFeedbackPanelSprite;
                feedbackText.text = wrongFeedbackText;
            }
        }
    }
}