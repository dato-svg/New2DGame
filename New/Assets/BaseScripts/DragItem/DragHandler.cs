using BaseScripts.BugSystem;
using UnityEngine;

namespace BaseScripts.DragItem
{
    public class DragHandler : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider;
        [SerializeField] private LineRenderer _line;
        [SerializeField] private AudioSource _grabSound;
        [SerializeField] private AudioSource _dropSound;
        
        public const float MAX_DISTANCE = 4;
        public const float THROW_INPULSE = 10;
        
        private IDraggable _currentDraggable;
        
        private RaycastHit2D _hit;
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                TryStartDrag();

            if (_currentDraggable as MonoBehaviour == null)
            {
                _currentDraggable = null;

                _line.enabled = false;

                return;
            }
            
            if (Input.GetMouseButton(0))
            {
                _currentDraggable.OnDrag(GetMouseWorldPosition());
                
                _line.SetPosition(0, transform.position);
                _line.SetPosition(1, (_currentDraggable as MonoBehaviour).transform.position);
            }

            if (Input.GetMouseButtonUp(0))
                EndDrag();

            CheckBlinking();
        }
        
        private void TryStartDrag()
        {
            Vector2 mousePos = GetMouseWorldPosition();
            _hit = Physics2D.Raycast(mousePos, Vector2.zero);
        
            if (_hit.collider != null && _hit.collider.TryGetComponent<IDraggable>(out var draggable))
            {
                if (Vector3.Distance(transform.position, _hit.transform.position) > MAX_DISTANCE) return;
                
                _currentDraggable = draggable;
                _currentDraggable.OnDragStart(this);

                if (_grabSound) _grabSound.Play();
                
                Physics2D.IgnoreCollision(_collider, _hit.collider, true);

                _line.positionCount = 2;
                _line.enabled = true;
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
            
            if (_dropSound) _dropSound.Play();
            
            Physics2D.IgnoreCollision(_collider, (_currentDraggable as MonoBehaviour).GetComponent<Collider2D>(), false);
            
            _currentDraggable.OnDragEnd(GetMouseWorldPosition());
            _currentDraggable = null;

            _line.enabled = false;
        }

        private Vector2 GetMouseWorldPosition()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -Camera.main.transform.position.z;
            return Camera.main.ScreenToWorldPoint(mousePosition);
        }
    }
}