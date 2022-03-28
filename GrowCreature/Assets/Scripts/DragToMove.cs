using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragToMove : MonoBehaviour
{
    private Camera _camera;

    private bool isDragging = false;
    private void Start()
    {
        _camera = Camera.main;
    }

    private void OnMouseDrag()
    {
        isDragging = true;
        float distance_to_screen = _camera.WorldToScreenPoint(gameObject.transform.position).z;
        Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen);
        transform.position = Vector3.Lerp(transform.position, _camera.ScreenToWorldPoint(screenPosition), .75f);
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!isDragging) return;
        Debug.Log(collision.gameObject.name);
    }
}