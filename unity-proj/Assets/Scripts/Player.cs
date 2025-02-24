using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVector2;
    [SerializeField]
    public float speed = 5f;
    private SpriteRenderer spriteRenderer;
    public Rigidbody2D rigidbody2D;
    private Animator animator;
    public Scanner scanner;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
    }
    
    private void FixedUpdate()
    {
        if (!GameManager.Instance.isLive) return;
        
        rigidbody2D.MovePosition(rigidbody2D.position + ( speed * Time.fixedDeltaTime * inputVector2 ) );
    }
    
    void OnMove(InputValue value)
    {
        if (!GameManager.Instance.isLive) return;
        
        inputVector2 = value.Get<Vector2>();
    }

    private void LateUpdate()
    {
        if (!GameManager.Instance.isLive) return;
        
        animator.SetFloat("Speed", inputVector2.magnitude);
        
        spriteRenderer.flipX = inputVector2.x switch
        {
            < 0 => true,
            > 0 => false,
            _ => spriteRenderer.flipX
        };
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (!GameManager.Instance.isLive) return;

        GameManager.Instance.health -= 10 * Time.deltaTime;
        
        if (GameManager.Instance.health <= 0)
        {
            GameManager.Instance.isLive = false;
            animator.SetBool("Dead", true);
            
            GameManager.Instance.GameOver();
        }
    }
}
