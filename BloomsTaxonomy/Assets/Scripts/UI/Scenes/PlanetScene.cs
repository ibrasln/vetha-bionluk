using System;
using System.Collections;
using Manager;
using Tutorial;
using UnityEngine;

namespace UI.Scenes
{
    public class PlanetScene : UIScene
    {
        [SerializeField] private Transform missionsParent;
        [SerializeField] private Mission.Mission[] missions;
        
        private int _currentMissionIndex;

        private void StartMission(int index)
        {
            
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
                StartMission(_currentMissionIndex);
                yield break;
            }

            _currentStep = _currentTutorial.Steps[_currentStepIndex];
            
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
            
            yield return StartCoroutine(TypeWriterRoutine(_currentStep.Instruction));
            
            yield return new WaitForSeconds(.5f);
            
            _continueButton.Open();
        }

    }
}
