using UnityEngine;

namespace BaseScripts.DragItem
{
    public class DraggableObject : MonoBehaviour, IDraggable
    {
        private Vector3 _offset;
        private Camera _mainCamera;
        
        private Material _material;
        private void Awake()
        {
            _material = GetComponent<Renderer>().material;
            _mainCamera = Camera.main;
        }

        public void OnDragStart()
        {
            _offset = transform.position - GetMouseWorldPosition();
            GetComponent<SpriteRenderer>().material = null;
            ChangeColor(Color.gray); 
        }
        
        public void OnDrag(Vector2 position)
        {
            transform.position = position + (Vector2)_offset;
        }

        public void OnDragEnd()
        {
            ChangeColor(Color.white); 
            GetComponent<SpriteRenderer>().material = _material;
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