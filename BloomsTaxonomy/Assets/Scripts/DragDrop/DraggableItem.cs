using UnityEngine;
using UnityEngine.EventSystems;

namespace DragDrop
{
    public class DraggableItem : DragDropItem, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public bool IsContained;
        
        private Vector3 _oldPosition;
        private ContainerItem _lastDroppedContainer;
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            _lastDroppedContainer = null;
            _oldPosition = transform.position;
            image.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.pointerCurrentRaycast.worldPosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            image.raycastTarget = true;
            if (_lastDroppedContainer is null) ReturnOldPosition();
        }

        public void ReturnOldPosition() => transform.position = _oldPosition;
        
        public void SetDroppedContainer(ContainerItem container) => _lastDroppedContainer = container;
    }
}