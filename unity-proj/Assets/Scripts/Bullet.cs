using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    public float damage;
    [FormerlySerializedAs("per")] public float penateration;

    private Rigidbody2D rigidbody2D;
    
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    public void Init(float damage, float per, Vector3 direction)
    {
        this.damage = damage;
        this.penateration = per;

        if (per > -1)
        {
            rigidbody2D.velocity = direction * 15f;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") == false || penateration == -1)
            return;

        penateration--;

        if (penateration == -1)
        {
            rigidbody2D.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
