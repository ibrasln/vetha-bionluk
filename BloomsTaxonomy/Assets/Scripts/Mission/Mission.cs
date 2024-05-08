using System;
using UI;
using UnityEngine;

namespace Mission
{
    public class Mission : UIElement
    {
        public Action OnMissionStarted;
        public Action OnMissionCompleted;

        public Report Report;
        
        public void CallOnMissionStarted()
        {
            Debug.Log($"{gameObject.name} mission started!");
            OnMissionStarted?.Invoke();
        }
        public void CallOnMissionCompleted() => OnMissionCompleted?.Invoke();
    }
}