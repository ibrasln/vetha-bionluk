using UnityEngine;
using UnityEngine.EventSystems;

namespace Mission.Kronos
{
    public class DraggableItem : KronosObject, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Vector3 _oldPosition;
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            _oldPosition = transform.position;
            image.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            image.raycastTarget = true;
        }

        public void ReturnOldPosition() => transform.position = _oldPosition;
    }
}
