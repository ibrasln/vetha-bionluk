using System;
using System.Collections;
using Manager;
using Tutorial;
using UnityEngine;

namespace UI.Scenes
{
    public class AresScene : PlanetScene
    {
        public override void StartMission()
        {
            currentMission.Open();
        }

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
                
                if (_currentTutorial == Tutorials[0] || _currentTutorial == Tutorials[2])
                {
                    Debug.Log("Start mission after tutorial");
                    StartMission();
                }
                else if (_currentTutorial == Tutorials[1])
                {
                    yield return new WaitForSeconds(.75f);
                    currentMission.Report.Open();
                }
                else if (_currentTutorial == Tutorials[3])
                {
                    SetIsCompleted(true);
                    TransitionManager.Instance.ChangeScene(UIObjects.Instance.UniverseScene);
                }
                yield break;
            }

            _currentStep = _currentTutorial.Steps[_currentStepIndex];
            TutorialStep previousStep = _currentStep;
            
            if (_currentStepIndex > 0) previousStep = _currentTutorial.Steps[_currentStepIndex - 1];
            
            if (_currentStep.PanelState != previousStep.PanelState) _currentTutorialPanel.Close();
            
            switch (_currentStep.PanelState)
            {
                case PanelState.Upper:
                    ekoBotImage.gameObject.SetActive(false);
                    break;
                case PanelState.Middle:
                    ekoBotImage.gameObject.SetActive(true);
                    ekoBotImage.sprite = _currentStep.EkoBotSprite;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            SetTutorialComponents();
            
            _instructionText.text = string.Empty;
            
            _currentTutorialPanel.Open();
            
            yield return new WaitForSeconds(.75f);
            
            if (_currentTutorial == Tutorials[0] && _currentStepIndex == 1) SetMission();
            
            yield return StartCoroutine(TypeWriterRoutine(_currentStep.Instruction));
            
            yield return new WaitForSeconds(.5f);
            
            _continueButton.Open();
        }
    }
}