using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace UI.Scenes
{
    public class PlanetScene : UIScene
    {
        [Space(7)] [Header("PLANET PROPERTIES")]
        public bool IsCompleted;
        [SerializeField] private Mission.Mission[] missions;
        
        [ReadOnly] public Mission.Mission CurrentMission;
        protected int currentMissionIndex;

        public void SetIsCompleted(bool state) => IsCompleted = state;
        
        public virtual void StartMission(){}
        
        public void SetMission()
        {
            if (CurrentMission != null) CurrentMission.Deactivate();
            if (currentMissionIndex < missions.Length) CurrentMission = missions[currentMissionIndex];
            
            CurrentMission.Activate();
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
