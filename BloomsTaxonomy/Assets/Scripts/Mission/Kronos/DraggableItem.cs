using UnityEngine;
using UnityEngine.EventSystems;

namespace Mission.Kronos
{
    public class DraggableItem : KronosObject, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Vector3 _oldPosition;
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("OnBeginDrag");
            _oldPosition = transform.position;
            image.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("OnDrag");
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("OnEndDrag");
            image.raycastTarget = true;
        }

        public void ReturnOldPosition() => transform.position = _oldPosition;
    }
}
