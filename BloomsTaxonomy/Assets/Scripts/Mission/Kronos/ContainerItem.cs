using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mission.Kronos
{
    public class ContainerItem : KronosObject, IDropHandler
    {
        private KronosMission _kronosMission;

        protected override void Awake()
        {
            base.Awake();
            _kronosMission = GetComponentInParent<KronosMission>();
        }

        public void OnDrop(PointerEventData eventData)
        {
            GameObject dropped = eventData.pointerDrag;
            
            if (dropped.TryGetComponent(out DraggableItem draggableItem))
            {
                if (draggableItem.ObjectId == ObjectId)
                {
                    Debug.Log("Matched!");
                    OnCorrect(draggableItem);
                }
                else
                {
                    Debug.Log("Wrong match!");
                    OnWrong(draggableItem);
                }
            }
        }

        private void OnCorrect(DraggableItem draggableItem)
        {
            draggableItem.transform.SetParent(transform);
            draggableItem.transform.position = transform.position;
                    
            image.DOColor(Color.green, .5f);
                    
            draggableItem.enabled = false;
            
            _kronosMission.IncreaseCorrectMatches();
            
            if (_kronosMission.CorrectMatches >= 8) _kronosMission.CallOnMissionCompleted();
            
            enabled = false;
        }

        private void OnWrong(DraggableItem draggableItem)
        {
            StartCoroutine(OnWrongRoutine(draggableItem));
        }
        
        private IEnumerator OnWrongRoutine(DraggableItem draggableItem)
        {
            draggableItem.ReturnOldPosition();
            image.DOColor(Color.red, .5f);

            yield return new WaitForSeconds(1f);
            
            image.DOColor(Color.white, .5f);
        }
    }
}