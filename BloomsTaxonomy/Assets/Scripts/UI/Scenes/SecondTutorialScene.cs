using System.Collections;
using Manager;
using UnityEngine;

namespace UI.Scenes
{
    public class SecondTutorialScene : UIScene
    {
        [SerializeField] private UIElement notebook;
        [SerializeField] private UIElement reportNotes;
        [SerializeField] private UIElement infoButton;
        [SerializeField] private UIElement quitButton;
        
        
        protected override IEnumerator SkipStepRoutine()
        {
            continueButton.Close();

            yield return new WaitForSeconds(1f);
            
            currentStepIndex++;
            OnSkippedStep?.Invoke();
            StartCoroutine(PlayTutorialStepRoutine());
        }

        protected override IEnumerator PlayTutorialStepRoutine()
        {
            if (currentStepIndex >= currentTutorial.Steps.Length)
            {
                yield return StartCoroutine(StopTutorialRoutine());
                TransitionManager.Instance.ChangeScene(UIObjects.Instance.UniverseScene);
                yield break;
            }

            currentStep = currentTutorial.Steps[currentStepIndex];
            
            ekoBotImage.gameObject.SetActive(true);
            ekoBotImage.sprite = currentStep.EkoBotSprite;
            
            SetTutorialComponents();
            
            instructionText.text = string.Empty;
            
            currentTutorialPanel.Open();

            yield return new WaitForSeconds(1f);
            
            switch (currentStepIndex)
            {
                case 0:
                    yield return StartCoroutine(OpenStepRoutine(notebook));
                    break;
                case 1:
                    yield return StartCoroutine(OpenStepRoutine(reportNotes));
                    break;
                case 2:
                    yield return StartCoroutine(OpenStepRoutine(infoButton));
                    break;
                case 3:
                    yield return StartCoroutine(OpenStepRoutine(quitButton));
                    break;
            }
            
            SetText(currentStep.Instruction);
            
            yield return new WaitForSeconds(.75f);

            switch (currentStepIndex)
            {
                case 0:
                    yield return StartCoroutine(CloseStepRoutine(notebook));
                    yield return ShowWindow(UIObjects.Instance.NotebookWindow);
                    break;
                case 1:
                    yield return StartCoroutine(CloseStepRoutine(reportNotes));
                    yield return ShowWindow(UIObjects.Instance.ReportNotesWindow);
                    break;
                case 2:
                    yield return StartCoroutine(CloseStepRoutine(infoButton));
                    yield return ShowWindow(UIObjects.Instance.InfoWindow);
                    break;
                case 3:
                    yield return StartCoroutine(CloseStepRoutine(quitButton));
                    break;
            }
            
            continueButton.Open();
            
        }

        private IEnumerator OpenStepRoutine(UIElement uiElement)
        {
            uiElement.Open();
            yield return new WaitForSeconds(.75f);
        }
        
        private IEnumerator CloseStepRoutine(UIElement uiElement)
        {
            uiElement.Close();
            yield return new WaitForSeconds(1.5f);
        }

        private IEnumerator ShowWindow(UIWindow uiWindow)
        {
            TransitionManager.Instance.AddWindow(uiWindow);
            yield return new WaitForSeconds(2f);
            TransitionManager.Instance.CloseWindow(uiWindow);
            yield return new WaitForSeconds(1.5f);
        }
    }
}