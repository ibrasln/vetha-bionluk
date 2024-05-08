using System;
using System.Collections;
using UnityEngine;

namespace UI.Scenes
{
    public class PlanetScene : UIScene
    {
        public bool IsCompleted;
        
        [SerializeField] private Mission.Mission[] missions;
        
        protected int currentMissionIndex;
        protected Mission.Mission currentMission;

        public void SetIsCompleted(bool state) => IsCompleted = state;
        
        public virtual void StartMission(){}
        public void SetMission()
        {
            if (currentMission != null) currentMission.Deactivate();
            if (currentMissionIndex < missions.Length) currentMission = missions[currentMissionIndex];
            
            currentMission.Activate();
            currentMissionIndex++;
        }


        protected override IEnumerator SkipStepRoutine()
        {
            yield break;
        }

        protected override IEnumerator PlayTutorialStepRoutine()
        {
            yield break;
        }
    }
}
