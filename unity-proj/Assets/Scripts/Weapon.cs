using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public float count;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count = count;

        if (id == 0)
            PlaceWeapon();
    }

    void Initialize()
    {
        switch (id)
        {
            case 0:
                speed = -150;
                PlaceWeapon();
                break;
            default:
                break;
        }
    }

    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(speed * Time.deltaTime * Vector3.back);
                break;
            default:
                break;
        }
        
        if (Input.GetButtonDown("Jump"))
            LevelUp(20, 5);
            
    }
    
    void PlaceWeapon()
    {
        for (int index=0; index < count; index++)
        {
            Transform bullet;
            if(index < transform.childCount)
                bullet = transform.GetChild(index);
            else
            {
                bullet = GameManager.Instance.poolManager.Get(prefabId).transform;
                bullet.parent = transform;
            }
            
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;
            
            Vector3 rotation = 360 * index / count * Vector3.forward;
            bullet.Rotate(rotation);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            
            bullet.GetComponent<Bullet>().Init(damage, -1); // -1 is Infinite Penetration
        }
        
    }
}
