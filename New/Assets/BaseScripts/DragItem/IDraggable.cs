using UnityEngine;

namespace BaseScripts.DragItem
{
    public interface IDraggable
    {
        void OnDragStart();
        void OnDrag(Vector2 position);
        void OnDragEnd();
    }
}