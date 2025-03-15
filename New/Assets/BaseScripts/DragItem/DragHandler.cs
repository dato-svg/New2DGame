using UnityEngine;

namespace BaseScripts.DragItem
{
    public class DragHandler : MonoBehaviour
    {
        private IDraggable _currentDraggable;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                TryStartDrag();

            if (Input.GetMouseButton(0) && _currentDraggable != null)
                _currentDraggable.OnDrag(GetMouseWorldPosition());

            if (Input.GetMouseButtonUp(0) && _currentDraggable != null)
                EndDrag();
        }
        
        private void TryStartDrag()
        {
            Vector2 mousePos = GetMouseWorldPosition();
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        
            if (hit.collider != null && hit.collider.TryGetComponent<IDraggable>(out var draggable))
            {
                _currentDraggable = draggable;
                _currentDraggable.OnDragStart();
            }
        }

        private void EndDrag()
        {
            _currentDraggable.OnDragEnd();
            _currentDraggable = null;
        }

        private Vector2 GetMouseWorldPosition()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -Camera.main.transform.position.z;
            return Camera.main.ScreenToWorldPoint(mousePosition);
        }
    }
}