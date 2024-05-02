using Interfaces;
using UnityEngine;

namespace UI
{
    public class UIWindow : MonoBehaviour, IInitializable, IDisposable, IActivatable
    {
        public void Initialize()
        {
            
        }

        public void Dispose()
        {
            
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {  
            gameObject.SetActive(false);
        }
    }
}