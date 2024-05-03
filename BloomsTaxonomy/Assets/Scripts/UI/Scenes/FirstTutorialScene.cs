using System;
using System.Collections;
using Manager;
using TMPro;
using Tutorial;
using UnityEngine;

namespace UI.Scenes
{
    public class FirstTutorialScene : UIScene
    {
        [SerializeField] private UIElement playerNameInputField;
        
        protected override IEnumerator SkipStepRoutine()
        {
            _continueButton.Close();

            yield return new WaitForSeconds(1f);
            
            if (_currentStepIndex == 1)
            {
                TMP_InputField inputField = playerNameInputField.GetComponent<TMP_InputField>();
                GameManager.Instance.SetPlayerName(inputField.text);
                playerNameInputField.Deactivate();
            }
            
            _currentStepIndex++;
            OnSkippedStep?.Invoke();
            StartCoroutine(PlayTutorialStepRoutine());
        }
        
        protected override IEnumerator PlayTutorialStepRoutine()
        {
            if (_currentStepIndex >= _currentTutorial.Steps.Length)
            {
                StartCoroutine(StopTutorialRoutine());
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
            
            _currentTutorialPanel.Open();
            
            yield return StartCoroutine(TypeWriterRoutine(_currentStep.Instruction));
            
            yield return new WaitForSeconds(.5f);
            
            if (_currentStepIndex == 1)
            {
                yield return StartCoroutine(GetPlayerNameRoutine());
            }
            
            _continueButton.Open();
        }

        private IEnumerator GetPlayerNameRoutine()
        {
            playerNameInputField.Activate();
            playerNameInputField.Open();
            
            yield return new WaitForSeconds(1f);
            
            TMP_InputField inputField = playerNameInputField.GetComponent<TMP_InputField>();
            
            while (inputField.text == string.Empty)
            {
                yield return null;
            }
        }
    }
}