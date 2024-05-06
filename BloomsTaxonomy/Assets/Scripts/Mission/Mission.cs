using System;
using UI;
using UnityEngine;

namespace Mission
{
    public class Mission : UIElement
    {
        public Action OnMissionStarted;
        public Action OnMissionCompleted;

        public void CallOnMissionStarted() => OnMissionStarted?.Invoke();
        public void CallOnMissionCompleted() => OnMissionCompleted?.Invoke();
    }
}