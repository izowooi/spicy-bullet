using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Vector2 inputVector2;
    [SerializeField]
    private float speed = 5f;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody2D;
    private Animator animator;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    
    private void FixedUpdate()
    {
        rigidbody2D.MovePosition(rigidbody2D.position + ( speed * Time.fixedDeltaTime * inputVector2 ) );
    }
    
    void OnMove(InputValue value)
    {
        inputVector2 = value.Get<Vector2>();
    }

    private void LateUpdate()
    {
        animator.SetFloat("Speed", inputVector2.magnitude);
        
        spriteRenderer.flipX = inputVector2.x switch
        {
            < 0 => true,
            > 0 => false,
            _ => spriteRenderer.flipX
        };
    }
}
