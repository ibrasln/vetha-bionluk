using System.Collections;
using Manager;
using UnityEngine;

namespace UI.Scenes
{
    public class SecondTutorialScene : UIScene
    {
        [SerializeField] private UIElement notebook;
        [SerializeField] private UIElement reportNotes;
        [SerializeField] private UIElement quitButton;
        
        
        protected override IEnumerator SkipStepRoutine()
        {
            _continueButton.Close();

            yield return new WaitForSeconds(1f);
            
            _currentStepIndex++;
            OnSkippedStep?.Invoke();
            StartCoroutine(PlayTutorialStepRoutine());
        }

        protected override IEnumerator PlayTutorialStepRoutine()
        {
            if (_currentStepIndex >= _currentTutorial.Steps.Length)
            {
                yield return StartCoroutine(StopTutorialRoutine());
                TransitionManager.Instance.ChangeScene(UIObjects.Instance.UniverseScene);
                yield break;
            }

            _currentStep = _currentTutorial.Steps[_currentStepIndex];
            
            ekoBotImage.gameObject.SetActive(true);
            ekoBotImage.sprite = _currentStep.EkoBotSprite;
            
            SetTutorialComponents();
            
            _instructionText.text = string.Empty;
            
            _currentTutorialPanel.Open();

            yield return new WaitForSeconds(1f);
            
            switch (_currentStepIndex)
            {
                case 0:
                    yield return StartCoroutine(OpenStepRoutine(notebook));
                    break;
                case 1:
                    yield return StartCoroutine(OpenStepRoutine(reportNotes));
                    break;
                case 2:
                    yield return StartCoroutine(OpenStepRoutine(quitButton));
                    break;
            }
            
            yield return StartCoroutine(TypeWriterRoutine(_currentStep.Instruction));
            
            yield return new WaitForSeconds(.5f);

            switch (_currentStepIndex)
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
                    yield return StartCoroutine(CloseStepRoutine(quitButton));
                    break;
            }
            
            _continueButton.Open();
            
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