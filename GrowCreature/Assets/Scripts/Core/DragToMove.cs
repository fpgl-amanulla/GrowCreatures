using UnityEngine;

namespace Core
{
    public abstract class DragToMove : MonoBehaviour
    {
        private Camera _camera;

        protected bool isDragging = false;
        public bool dragEnabled { get; set; }

        private void Awake() => dragEnabled = true;

        private void Start() => _camera = Camera.main;

        private void OnMouseDrag()
        {
            isDragging = true;
            if (!dragEnabled) return;
            float distance_to_screen = _camera.WorldToScreenPoint(gameObject.transform.position).z;
            Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen);
            transform.position = Vector3.Lerp(transform.position, _camera.ScreenToWorldPoint(screenPosition), .75f);
        }

        private void OnMouseUp() => isDragging = false;
    }
}