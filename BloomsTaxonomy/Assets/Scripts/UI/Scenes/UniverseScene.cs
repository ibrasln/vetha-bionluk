using System;
using System.Collections;
using Manager;
using Tutorial;
using UnityEngine;

namespace UI.Scenes
{
    public class UniverseScene : UIScene
    {
        [SerializeField] private PlanetObject[] planets;
        
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
        }
    }
}