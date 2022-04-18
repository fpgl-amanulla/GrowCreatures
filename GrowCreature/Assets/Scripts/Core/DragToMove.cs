using System;
using UnityEngine;

namespace Core
{
    public abstract class DragToMove : MonoBehaviour
    {
        private Camera _camera;

        protected bool isDragging;
        protected bool dragEnabled { get; private set; }
        private Vector3 _offset;

        private void Awake() => dragEnabled = true;

        private void Start() => _camera = Camera.main;

        private void OnMouseDown()
        {
            _offset = Get_STW_MousePos() - transform.position;
        }

        private void OnMouseDrag()
        {
            isDragging = true;
            if (!dragEnabled) return;
            //float distance_to_screen = _camera.WorldToScreenPoint(gameObject.transform.position).z;
            //Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen);
            transform.position = Vector3.Lerp(transform.position, Get_STW_MousePos() - _offset, .75f);
        }

        private void OnMouseUp() => isDragging = false;

        private Vector3 Get_STW_MousePos() => _camera.ScreenToWorldPoint(Input.mousePosition);
    }
}