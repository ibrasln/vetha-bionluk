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
            
            currentStepIndex++;
            OnSkippedStep?.Invoke();
            StartCoroutine(PlayTutorialStepRoutine());
        }

        protected override IEnumerator PlayTutorialStepRoutine()
        {
            if (currentStepIndex >= currentTutorial.Steps.Length)
            {
                yield return StartCoroutine(StopTutorialRoutine());
                yield break;
            }

            currentStep = currentTutorial.Steps[currentStepIndex];
            
            switch (currentStep.PanelState)
            {
                case PanelState.Upper:
                    ekoBotImage.gameObject.SetActive(false);
                    break;
                case PanelState.Middle:
                    ekoBotImage.gameObject.SetActive(true);
                    ekoBotImage.sprite = currentStep.EkoBotSprite;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            SetTutorialComponents();
            
            instructionText.text = string.Empty;
            
            currentTutorialPanel.Open();

            SetText(currentStep.Instruction);
            
            yield return new WaitForSeconds(.75f);
        }
    }
}