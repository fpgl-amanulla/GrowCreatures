using System;
using UnityEngine;

namespace Core
{
    public enum MainCameraType
    {
        Perspective,
        Orthographic
    }

    public class CameraBoundaries : MonoBehaviour
    {
        private Camera _camera;
        private MainCameraType _mainCameraType;
        private Vector2 screenBounds;
        private float objectWidth;
        private float objectHeight;

        private float lowerBound = 0;

        private void Start()
        {
            _camera = Camera.main;
            _mainCameraType = _camera != null && _camera.orthographic
                ? MainCameraType.Orthographic
                : MainCameraType.Perspective;

            if (_camera != null)
            {
                Vector3 screenPosition = new Vector3(Screen.width, Screen.height, _camera.transform.position.z);
                screenBounds = _camera.ScreenToWorldPoint(screenPosition);
            }

            objectWidth = transform.GetComponentInChildren<Renderer>().bounds.extents.x; //extents = size of width / 2
            objectHeight = transform.GetComponentInChildren<Renderer>().bounds.extents.y; //extents = size of height / 2
        }

        private void LateUpdate()
        {
            transform.position = GetBoundPosition();
        }

        private Vector3 GetBoundPosition()
        {
            Vector3 viewPos = transform.position;
            switch (_mainCameraType)
            {
                case MainCameraType.Perspective:
                    viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x + objectWidth, screenBounds.x * -1 - objectWidth);
                    viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y + objectHeight,
                        screenBounds.y * -1 - objectHeight);
                    break;
                case MainCameraType.Orthographic:
                    viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
                    viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight + lowerBound,
                        screenBounds.y - objectHeight);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return viewPos;
        }

        public void SetBound(float _lowerBound)
        {
            lowerBound = _lowerBound;
        }
    }
}