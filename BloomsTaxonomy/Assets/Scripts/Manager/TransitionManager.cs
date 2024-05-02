using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Utilities;

public class TransitionManager : MySingleton<TransitionManager>
{
    public Canvas CurrentCanvas;
    [SerializeField] private CanvasGroup fadeScreen;

    #region Events
    public Action OnCanvasChanged;
    #endregion
    
    public void ChangeCanvas(Canvas canvas)
    {
        StartCoroutine(ChangeCanvasRoutine(canvas));
    }

    private IEnumerator ChangeCanvasRoutine(Canvas canvas)
    {
        fadeScreen.DOFade(1, 1);
        yield return new WaitForSeconds(1f);
        CurrentCanvas.gameObject.SetActive(false);
        CurrentCanvas = canvas;
        CurrentCanvas.gameObject.SetActive(true);
        fadeScreen.DOFade(0, 1);
        yield return new WaitForSeconds(1f);
        OnCanvasChanged?.Invoke();
    }
}
