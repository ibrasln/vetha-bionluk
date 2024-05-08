using System;
using UnityEngine;

namespace Mission.Kronos
{
    public class KronosMission : Mission
    {
        [SerializeField] private DraggableItem[] DraggableItems;
        
        private int _correctMatches;
        
        public int CorrectMatches => _correctMatches;

        private void Start()
        {
            SetDraggableItems(false);
            OnMissionCompleted += Report.Open;
        }

        private void OnDestroy()
        {
            OnMissionCompleted -= Report.Open;
        }

        public void SetDraggableItems(bool state)
        {
            foreach (DraggableItem draggableItem in DraggableItems)
            {
                draggableItem.enabled = state;
            }
        }
        
        public void IncreaseCorrectMatches() => _correctMatches++;
    }
}