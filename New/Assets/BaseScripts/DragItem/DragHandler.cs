using BaseScripts.BugSystem;
using UnityEngine;

namespace BaseScripts.DragItem
{
    public class DragHandler : MonoBehaviour
    {
        private IDraggable _currentDraggable;
        
        private RaycastHit2D _hit;
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                TryStartDrag();

            if (Input.GetMouseButton(0) && _currentDraggable != null)
                _currentDraggable.OnDrag(GetMouseWorldPosition());

            if (Input.GetMouseButtonUp(0) && _currentDraggable != null)
                EndDrag();

            CheckBlinking();
        }
        
        private void TryStartDrag()
        {
            Vector2 mousePos = GetMouseWorldPosition();
            _hit = Physics2D.Raycast(mousePos, Vector2.zero);
        
            if (_hit.collider != null && _hit.collider.TryGetComponent<IDraggable>(out var draggable) && _hit.collider.TryGetComponent<BlinkingObject>(out var blinkingObject))
            {
                if (blinkingObject.Blinking)
                    return;
                
                _currentDraggable = draggable;
                _currentDraggable.OnDragStart();
            }
        }

        private void CheckBlinking()
        {
            if (_hit.collider != null  && _hit.collider.TryGetComponent<BlinkingObject>(out var blinkingObject))
                if (blinkingObject.Blinking) 
                    EndDrag();
        }

        private void EndDrag()
        {
            if (_currentDraggable == null)
                return;
            
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