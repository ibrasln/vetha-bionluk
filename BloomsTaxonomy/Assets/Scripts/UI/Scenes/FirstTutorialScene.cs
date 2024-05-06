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
                yield return StartCoroutine(StopTutorialRoutine());
                TransitionManager.Instance.ChangeScene(UIObjects.Instance.SecondTutorialScene);
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