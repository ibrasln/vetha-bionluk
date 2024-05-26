using NaughtyAttributes;
using TMPro;
using UnityEngine;
using Utilities;

namespace Manager
{
    public class GameManager : MySingleton<GameManager>
    {
        [ReadOnly] public string PlayerName = "Oyuncu";
        [ReadOnly] public int DiamondAmount;
        [SerializeField] private TextMeshProUGUI diamondText;
        
        private void Start()
        {
            TransitionManager.Instance.Initialize();
        }

        public void IncreaseDiamondAmount(int amount)
        {
            DiamondAmount += amount;
            UpdateDiamondText();
        }
        
        public void DecreaseDiamondAmount(int amount)
        {
            DiamondAmount -= amount;
            UpdateDiamondText();
        }

        private void UpdateDiamondText() => diamondText.text = DiamondAmount.ToString();
        
        public void SetPlayerName(string playerName) => PlayerName = playerName;

        public void Quit()
        {
            if (Application.isPlaying) Application.Quit();
        }
    }
}