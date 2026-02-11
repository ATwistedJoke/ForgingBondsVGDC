using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragTongs : MonoBehaviour
{
    private Camera mainCamera;

    [SerializeField]
    private float dragSpeed = 10f;

    private Vector3 objectSize;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        followMousePosition();
    }

    private void followMousePosition()
    {
        transform.position = GetWorldPositionFromMouse();
    }

    private Vector2 GetWorldPositionFromMouse()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
}