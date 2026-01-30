using System;
using UnityEngine;

public class Collector : MonoBehaviour
{

    public GameObject mini_game_panel;

    [SerializeField] private float move_speed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
    Rigidbody2D rb = GetComponent<Rigidbody2D>();
    float horizontalInput = Input.GetAxis("Horizontal");
    Vector3 newVelocity = new Vector3(horizontalInput * move_speed, rb.linearVelocity.y, rb.linearVelocity.x);

    rb.linearVelocity = newVelocity;
        
    }


}