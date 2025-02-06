using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D target;
    bool isLive = true;
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (isLive == false)
            return;
        
        Vector2 direction = (target.position - rigidbody2D.position).normalized;
        
        rigidbody2D.MovePosition(rigidbody2D.position + (speed * Time.fixedDeltaTime * direction));
        
        rigidbody2D.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        spriteRenderer.flipX = target.position.x < rigidbody2D.position.x;
    }

    private void OnEnable()
    {
        target = GameManager.Instance.player.rigidbody2D;
    }
}
