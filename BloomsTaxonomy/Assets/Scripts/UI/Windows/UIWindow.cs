using Interfaces;

namespace UI
{
    public class UIWindow : UIObject, IInitializable, IDisposable, IActivatable
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