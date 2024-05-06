using System;
using System.Collections;
using Manager;
using Tutorial;
using UnityEngine;

namespace UI.Scenes
{
    public class UniverseScene : UIScene
    {
        private void OnEnable()
        {
            TransitionManager.Instance.AddWindow(UIObjects.Instance.GeneralWindow);
        }

        protected override IEnumerator SkipStepRoutine()
        {
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
                yield break;
            }

            _currentStep = _currentTutorial.Steps[_currentStepIndex];
            
            ekoBotImage.sprite = _currentStep.PanelState switch
            {
                PanelState.Upper => null,
                PanelState.Middle => _currentStep.EkoBotSprite,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            SetTutorialComponents();
            
            _instructionText.text = string.Empty;
            
            _currentTutorialPanel.Open();

            yield return new WaitForSeconds(.75f);
            
            yield return StartCoroutine(TypeWriterRoutine(_currentStep.Instruction));
            
            yield return new WaitForSeconds(.5f);            
        }
    }
}