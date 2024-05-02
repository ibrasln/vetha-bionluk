using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Scenes
{
    public class BeginningScene : UIScene
    {
        [SerializeField] private Image bounLogo;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private RectTransform startButton;
        [SerializeField] private TextMeshProUGUI footerText;

        public IEnumerator InitializeRoutine()
        {
            yield return new WaitForSeconds(2);
            Debug.Log("a");
        }
    }
}