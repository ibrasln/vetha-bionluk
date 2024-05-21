using DragDrop;
using UnityEngine;
using UnityEngine.UI;

namespace Mission.Report
{
    public class DragDropReport : Report
    {
        [SerializeField] private DraggableItem[] draggableItemAnswers;
        
        public override void CheckAnswers()
        {
            isCompleted = true;
            
            foreach (DraggableItem draggableItem in draggableItemAnswers)
            {
                if (draggableItem.IsContained) continue;
                
                isCompleted = false;
            }
        }

        public override void OpenPanel()
        {
            base.OpenPanel();
            if (isCompleted)
            {
                feedbackPanel.GetComponent<Image>().sprite = correctFeedbackPanelSprite;
                feedbackText.text = "Harika, piramidin basamaklarını başarıyla tamamladın.";
            }
            else
            {
                feedbackPanel.GetComponent<Image>().sprite = wrongFeedbackPanelSprite;
                feedbackText.text = "Üzgünüm, bütün boşlukları doğru doldurmalısın. Tekrar dene!\n";
            }
        }
    }
}