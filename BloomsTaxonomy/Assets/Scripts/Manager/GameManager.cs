using Utilities;

namespace Manager
{
    public class GameManager : MySingleton<GameManager>
    {
        private void Start()
        {
            TransitionManager.Instance.Initialize();
        }
    }
}