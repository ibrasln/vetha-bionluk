using UnityEngine;
using Utilities;

namespace Manager
{
    public class GameManager : MySingleton<GameManager>
    {
        public string PlayerName = "Oyuncu";
        
        private void Start()
        {
            TransitionManager.Instance.Initialize();
        }

        public void SetPlayerName(string playerName) => PlayerName = playerName;

        public void Quit()
        {
            if (Application.isPlaying) Application.Quit();
        }
    }
}