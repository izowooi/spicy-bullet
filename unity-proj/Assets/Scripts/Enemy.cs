using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    public float health = 100f;
    public float maxHealth = 100f;
    public RuntimeAnimatorController[] animators;
    public Rigidbody2D target;
    bool isLive;
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    Animator animator;
    WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (isLive == false || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
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
        isLive = true;
        health = maxHealth;
    }
    
    public void Initialize(SpawnData data)
    {
        speed = data.speed;
        health = data.health;
        maxHealth = data.health;
        animator.runtimeAnimatorController = animators[data.spawnType];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") == false)
            return;
        Bullet bullet = other.GetComponent<Bullet>();
        health -= bullet.damage;
        StartCoroutine(Knockback());
        if (health > 0)
        {
            animator.SetTrigger("Hit");
        }
        else
        {
            Dead();
        }
    }

    IEnumerator Knockback()
    {
        yield return waitForFixedUpdate;
        Vector3 playerPosition = GameManager.Instance.player.transform.position;
        Vector3 direction = (transform.position - playerPosition).normalized;
        rigidbody2D.AddForce(direction * 3f, ForceMode2D.Impulse);
    }
    
    void Dead()
    {
        isLive = false;
        gameObject.SetActive(false);
    }
}
