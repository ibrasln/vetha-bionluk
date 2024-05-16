using System;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Mission
{
    public class Mission : UIElement
    {
        public Action OnMissionStarted;
        public Action OnMissionCompleted;

        public Report Report;

        private void Awake()
        {
            Report = transform.Find("Report").GetComponent<Report>();
        }

        public void CallOnMissionStarted()
        {
            Debug.Log($"{gameObject.name} mission started!");
            OnMissionStarted?.Invoke();
        }
        
        public void CallOnMissionCompleted() => OnMissionCompleted?.Invoke();
    }
}