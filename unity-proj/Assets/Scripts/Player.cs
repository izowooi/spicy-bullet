using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Vector2 inputVector2;
    [SerializeField]
    private float speed = 5f;

    private Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputVector2.x = Input.GetAxisRaw("Horizontal");
        inputVector2.y = Input.GetAxisRaw("Vertical");
    }
    
    private void FixedUpdate()
    {
        rigidbody2D.MovePosition(rigidbody2D.position + ( speed * Time.fixedDeltaTime * inputVector2.normalized ) );
    }
}
