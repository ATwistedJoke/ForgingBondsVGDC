using System;
using UnityEngine;

public class RotationHandler : MonoBehaviour
{
    public GameObject center; 
    public CapsuleCollider2D col; 
    public LineRenderer circleRenderer; 

    public float rotationSpeed; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        circleRenderer = center.GetComponent<LineRenderer>(); 
        DrawCircle(100, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rotationSpeed = -rotationSpeed; 
        }
        this.transform.RotateAround(center.transform.position, Vector3.forward, rotationSpeed);
        circleRenderer.transform.Rotate(Vector3.forward, 5);
    }

    void DrawCircle(int steps, float radius)
    {
        circleRenderer.positionCount = steps; 
        for(int curr = 0; curr < steps; curr++)
        {
            float circumferenceProgress = (float)curr/steps; 

            float currentRadian = circumferenceProgress*2*Mathf.PI; 

            float xScale = Mathf.Cos(currentRadian);
            float yScale = Mathf.Sin(currentRadian);

            float x = xScale * radius; 
            float y = yScale * radius;

            Vector3 currentPosition = new Vector3(x,y,0); 

            circleRenderer.SetPosition(curr, currentPosition); 
        }
    }
}
