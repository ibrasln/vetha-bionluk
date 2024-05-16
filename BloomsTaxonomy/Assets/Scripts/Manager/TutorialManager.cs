// using System;
// using System.Collections;
// using DG.Tweening;
// using TMPro;
// using Tutorial;
// using UI;
// using UnityEngine;
// using UnityEngine.UI;
// using Utilities;
//
// namespace Manager
// {
//     public class TutorialUtilities : MySingleton<TutorialUtilities>
//     {
//         public TutorialData CurrentTutorial;
//         public TutorialPanel CurrentTutorialPanel;
//         
//         private TutorialStep currentStep;
//         private int currentStepIndex;
//         
//         public Action OnTutorialStarted;
//         public Action OnTutorialStopped;
//         public Action OnSkippedStep;
//         
//         private void Start()
//         {
//             TransitionManager.Instance.OnSceneChanged += StartTutorial;
//         }
//
//         private void OnDestroy()
//         {
//             TransitionManager.Instance.OnSceneChanged -= StartTutorial;
//         }
//
//         #region Start & Stop Tutorial
//         private void StartTutorial(UIScene scene)
//         {
//             SetCurrentTutorial(scene);
//             SetTutorialPanel(scene);
//             StartCoroutine(StartTutorialRoutine());
//         }
//
//         private IEnumerator StartTutorialRoutine()
//         {
//             OnTutorialStarted?.Invoke();
//             
//             switch (CurrentTutorialPanel.TutorialPanelState)
//             {
//                 case TutorialPanelState.Upper:
//                     CurrentTutorialPanel.MoveY(0, 1);
//                     break;
//                 case TutorialPanelState.Middle:
//                     CurrentTutorialPanel.Resize(1, 1);
//                     break;
//                 default:
//                     throw new ArgumentOutOfRangeException();
//             }
//             
//             yield return new WaitForSeconds(1f);
//             
//             PlayTutorialStep();
//         }
//
//         private void StopTutorial()
//         {
//             StartCoroutine(StopTutorialRoutine());
//         }
//
//         private IEnumerator StopTutorialRoutine()
//         {
//             switch (CurrentTutorialPanel.TutorialPanelState)
//             {
//                 case TutorialPanelState.Upper:
//                     CurrentTutorialPanel.MoveY(500, 1);
//                     break;
//                 case TutorialPanelState.Middle:
//                     CurrentTutorialPanel.Resize(0, 1);
//                     break;
//                 default:
//                     throw new ArgumentOutOfRangeException();
//             }
//             
//             yield return new WaitForSeconds(1f);
//             
//             OnTutorialStopped?.Invoke();
//         }
//         #endregion
//         
//         private void SetCurrentTutorial(UIScene scene)
//         {
//             CurrentTutorial = scene.Tutorial;
//             currentStep = CurrentTutorial.Steps[currentStepIndex];
//         }
//
//         private void SetTutorialPanel(UIScene scene)
//         {
//             CurrentTutorialPanel = scene.TutorialPanel;
//             _ekoBotImage = scene.EkoBotImage;
//             instructionText = CurrentTutorialPanel.GetComponentInChildren<TextMeshProUGUI>();
//             continueButton = CurrentTutorialPanel.GetComponentInChildren<Button>().GetComponent<RectTransform>();
//         }
//
//         #region Skip Step
//         public void SkipStep()
//         {
//             StartCoroutine(SkipStepRoutine());
//         }
//
//         private IEnumerator SkipStepRoutine()
//         {
//             continueButton.DOScale(0, .5f).SetEase(Ease.InBack);
//             
//             yield return new WaitForSeconds(.75f);
//             
//             currentStepIndex++;
//             PlayTutorialStep();
//         }
//         #endregion
//
//         #region Play Tutorial
//         private void PlayTutorialStep()
//         {
//             StartCoroutine(PlayTutorialStepRoutine());
//         }
//
//         private IEnumerator PlayTutorialStepRoutine()
//         {
//             if (currentStepIndex >= CurrentTutorial.Steps.Length)
//             {
//                 StopTutorial();
//                 yield break;
//             }
//             
//             currentStep = CurrentTutorial.Steps[currentStepIndex];
//             OnSkippedStep?.Invoke();
//
//             _ekoBotImage.sprite = currentStep.EkoBotSprite;
//             
//             yield return StartCoroutine(TypeWriterRoutine(currentStep.Instruction));
//             
//             yield return new WaitForSeconds(.5f);
//
//             continueButton.DOScale(1, .5f).SetEase(Ease.OutBack);
//         }
//         #endregion
//     }
// }
