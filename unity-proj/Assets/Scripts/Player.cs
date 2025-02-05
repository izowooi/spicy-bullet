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

    private Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        rigidbody2D.MovePosition(rigidbody2D.position + ( speed * Time.fixedDeltaTime * inputVector2 ) );
    }
    
    void OnMove(InputValue value)
    {
        inputVector2 = value.Get<Vector2>();
    }

    //왼쪽으로 이동할 때에는 SpriteRenderer의 flipX를 true로 설정
    private void LateUpdate()
    {
        
    }
}
