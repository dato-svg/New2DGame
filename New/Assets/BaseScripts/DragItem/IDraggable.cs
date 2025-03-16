using UnityEngine;

namespace BaseScripts.DragItem
{
    public interface IDraggable
    {
        void OnDragStart(DragHandler handler);
        void OnDrag(Vector2 position);
        void OnDragEnd(Vector2 position);
    }
}