using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragTongs : MonoBehaviour
{
    private Camera mainCamera;

    private Vector3 objectSize;
    private bool itemEquipped = false;
    public Transform spawnPos;
    public PickUpIron pickUpIron;

    void Start()
    {
        mainCamera = Camera.main;
        pickUpIron = GameObject.FindGameObjectWithTag("Ingot").GetComponent<PickUpIron>();
    }
    void Update()
    {
        if (itemEquipped)
        {
            transform.position = GetWorldPositionFromMouse();
        }
    }
    private void OnMouseDown()
    {
        pickUpIron.setDown();
        itemEquipped = !itemEquipped;
        if(spawnPos != null)
        {
            transform.position = spawnPos.position;
        }
    }
    private Vector2 GetWorldPositionFromMouse()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
}