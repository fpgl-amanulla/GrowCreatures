using UnityEngine;

public class DragToRotate : MonoBehaviour
{
    private readonly float rotationSpeed = 25;

    private void OnMouseDrag()
    {
        if (Application.isEditor)
        {
            float xAxisRotation = Input.GetAxis("Mouse X") * rotationSpeed;
            transform.Rotate(Vector3.down * xAxisRotation);
        }
        else
        {
            if (Input.touchCount <= 0) return;
            float xAxisRotation = Input.touches[0].deltaPosition.x * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.down * xAxisRotation);
        }
    }
}