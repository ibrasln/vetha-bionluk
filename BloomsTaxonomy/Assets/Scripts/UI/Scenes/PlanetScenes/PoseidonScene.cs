using System;
using System.Collections;
using Mission.Kronos;
using Tutorial;
using UnityEngine;

namespace UI.Scenes
{
    public class PoseidonScene : PlanetScene
    {
        public override void StartMission()
        {
            base.StartMission();
            
            // KronosMission kronosMission = currentMission as KronosMission;
            // if (kronosMission != null) kronosMission.SetDraggableItems(true);
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
                Debug.Log("Start mission after tutorial");

                yield return new WaitForSeconds(.75f);
                //TODO: Start mission.
                
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
            
            //TODO: Set Mission
            // if(_currentStepIndex == 4) SetMission();
            
            _instructionText.text = string.Empty;
            
            _currentTutorialPanel.Open();
            
            yield return new WaitForSeconds(.75f);
            
            
            yield return StartCoroutine(TypeWriterRoutine(_currentStep.Instruction));
            
            yield return new WaitForSeconds(.5f);
            
            _continueButton.Open();
        }
    }
}