using UnityEngine;

namespace BaseScripts.DragItem
{
    [RequireComponent(typeof(HingeJoint2D))]
    public class DraggableObject : MonoBehaviour, IDraggable
    {
        private HingeJoint2D _joint;
        private DragHandler _handler;

        private Vector2 _lastPosition;
        private Vector2 _offset;
        private Camera _mainCamera;
        
        private Material _material;
        private void Awake()
        {
            _joint = GetComponent<HingeJoint2D>();

            _joint.enabled = false;
            
            _material = GetComponent<Renderer>().material;
            _mainCamera = Camera.main;
        }

        public void OnDragStart(DragHandler handler)
        {
            _lastPosition = GetMouseWorldPosition();
            
            _joint.enabled = true;
            
            _handler = handler;
            _offset = transform.position - GetMouseWorldPosition();
             // GetComponent<SpriteRenderer>().material = null;
             // ChangeColor(Color.gray); 
        }
        
        public void OnDrag(Vector2 position)
        {
            var delta = (Vector3)position - _handler.transform.position;

            if (delta.magnitude > DragHandler.MAX_DISTANCE)
                position = _handler.transform.position + delta.normalized * DragHandler.MAX_DISTANCE;
            
            _joint.connectedAnchor = position;
            _joint.anchor = -_offset;

            _lastPosition = position;
        }

        public void OnDragEnd(Vector2 position)
        {
            var delta = (Vector3)position - _handler.transform.position;
            
            if (delta.magnitude > DragHandler.MAX_DISTANCE)
                position = _handler.transform.position + delta.normalized * DragHandler.MAX_DISTANCE;
            
            var velocity = (position - _lastPosition) / Time.deltaTime * DragHandler.THROW_INPULSE;
            
            _joint.attachedRigidbody.linearVelocity = velocity.magnitude > DragHandler.THROW_INPULSE ? velocity.normalized * DragHandler.THROW_INPULSE : velocity;

            _joint.enabled = false;
            
            _handler = null;
            
            // ChangeColor(Color.white); 
            // GetComponent<SpriteRenderer>().material = _material;
        }

        private Vector3 GetMouseWorldPosition()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -_mainCamera.transform.position.z;
            return _mainCamera.ScreenToWorldPoint(mousePosition);
        }

        private void ChangeColor(Color color)
        {
            if (TryGetComponent<SpriteRenderer>(out var renderer))
            {
                renderer.color = color;
            }
        }
    }
}