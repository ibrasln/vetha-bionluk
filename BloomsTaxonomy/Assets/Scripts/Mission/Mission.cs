using System;
using UI;
using UI.Scenes;
using UnityEngine;
using UnityEngine.Serialization;

namespace Mission
{
    public class Mission : UIElement
    {
        public Action OnMissionStarted;
        public Action OnMissionCompleted;

        public Report.Report Report;
        public PlanetScene Planet;

        private void Awake()
        {
            Report = transform.Find("Report").GetComponent<Report.Report>();
            Planet = GetComponentInParent<PlanetScene>();
        }

        public void CallOnMissionStarted()
        {
            Debug.Log($"{gameObject.name} mission started!");
            OnMissionStarted?.Invoke();
        }
        
        public void CallOnMissionCompleted() => OnMissionCompleted?.Invoke();
    }
}