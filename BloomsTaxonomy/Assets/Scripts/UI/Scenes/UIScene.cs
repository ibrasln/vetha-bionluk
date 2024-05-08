using System;
using System.Collections;
using Interfaces;
using Manager;
using NaughtyAttributes;
using TMPro;
using Tutorial;
using UnityEngine;
using UnityEngine.UI;
using IDisposable = Interfaces.IDisposable;

namespace UI.Scenes
{
    public abstract class UIScene : UIObject, IInitializable, IDisposable
    {
        public TutorialData[] Tutorials;
        public UIElement[] TutorialPanels;
        [ReadOnly] public int CurrentTutorialIndex;
        
        [SerializeField] protected Image ekoBotImage;
        protected TextMeshProUGUI _instructionText;
        protected UIElement _continueButton;

        protected TutorialData _currentTutorial;
        protected UIElement _currentTutorialPanel;
        protected TutorialStep _currentStep;
        protected int _currentStepIndex;
        
        public Action OnTutorialStarted;
        public Action OnTutorialStopped;
        public Action OnSkippedStep;

        #region Start & Stop Tutorial
        public void StartTutorial(int index)
        {
            if (Tutorials.Length <= 0) return;

            _currentTutorial = Tutorials[index];
            
            OnTutorialStarted?.Invoke();
            
            _currentStepIndex = 0;
            
            StartCoroutine(PlayTutorialStepRoutine());
        }

        public void StopTutorial() => StartCoroutine(StopTutorialRoutine());
        
        protected IEnumerator StopTutorialRoutine()
        {
            _currentTutorialPanel.Close();
            
            if (CurrentTutorialIndex >= Tutorials.Length)
            {
                CurrentTutorialIndex = 0;
            }
            else
            {
                CurrentTutorialIndex++;
            }
            
            yield return new WaitForSeconds(1f);
            
            ekoBotImage.gameObject.SetActive(false);
            
            OnTutorialStopped?.Invoke();
            
        }
        #endregion

        #region Skip Step
        public void SkipStep() => StartCoroutine(SkipStepRoutine());

        protected abstract IEnumerator SkipStepRoutine();
        #endregion
        
        protected abstract IEnumerator PlayTutorialStepRoutine();
        
        protected void SetTutorialComponents()
        {
            _currentTutorialPanel = _currentStep.PanelState switch
            {
                PanelState.Middle => TutorialPanels[0],
                PanelState.Upper => TutorialPanels[1],
                _ => throw new ArgumentOutOfRangeException()
            };
            
            _instructionText = _currentTutorialPanel.GetComponentInChildren<TextMeshProUGUI>();
            _continueButton = _currentTutorialPanel.GetComponentInChildren<Button>().GetComponent<UIElement>();
        }

        #region TypeWriting
        protected IEnumerator TypeWriterRoutine(string text)
        {
            int currentCharacterIndex = 0;
            
            string newText = SetText(text);

            float timeBetweenCharacters = .01f;

            while (currentCharacterIndex < newText.Length)
            {
                char currentCharacter = newText[currentCharacterIndex];
                _instructionText.text += currentCharacter;

                currentCharacterIndex++;

                yield return new WaitForSeconds(timeBetweenCharacters);
            }
        }
        
        private string SetText(string text)
        {
            // Replace the <playerName> with player name. 
            string[] words = text.Split(' ');
            
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Contains("<playerName>"))
                {
                    words[i] = words[i].Replace("<playerName>", GameManager.Instance.PlayerName);
                }
            }
            
            string newText = string.Join(" ", words);
            
            // Set the size of the text
            _instructionText.enableAutoSizing = true;
            _instructionText.text = newText;
            _instructionText.ForceMeshUpdate();
            _instructionText.enableAutoSizing = false;
            _instructionText.text = string.Empty;

            return newText;
        }
        #endregion
        
        public void Initialize()
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}