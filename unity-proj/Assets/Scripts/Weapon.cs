using System;
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
    public float intervalFire;
    Player player;

    private void Awake()
    {
        player = GameManager.Instance.player;
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count = count;

        if (id == 0)
            PlaceWeapon();
        
        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void Initialize(ItemData itemData)
    {
        name = "Weapon" + itemData.itemType;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        id = itemData.itemId;
        damage = itemData.baseDamage;
        count = itemData.baseCount;
        for (int i = 0; i<GameManager.Instance.poolManager.prefabs.Length; i++)
        {
            if(itemData.projectile == GameManager.Instance.poolManager.prefabs[i])
            {
                prefabId = i;
                break;
            }
        }
        switch (id)
        {
            case 0:
                speed = 150;
                PlaceWeapon();
                break;
            default:
                intervalFire = 0.3f;
                break;
        }
        
        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    private float timer = 0f;
    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(speed * Time.deltaTime * Vector3.back);
                break;
            default:
                timer += Time.deltaTime;
                if (timer > intervalFire)
                {
                    timer = 0;
                    Fire();
                }
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
            
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // -1 is Infinite Penetration
        }
    }

    void Fire()
    {
        if (player.scanner.nearestTarget == false)
            return;
        
        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 direction = (targetPos - transform.position).normalized;
        
        Transform bullet = GameManager.Instance.poolManager.Get(prefabId).transform;
        bullet.position = transform.position;
        
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        bullet.GetComponent<Bullet>().Init(damage, count, direction);
    }
}
