using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType itemType;
    public float rate;
    
    public void Initialize(ItemData itemData)
    {
        name = $"Gear {itemData.itemId}";
        transform.parent = GameManager.Instance.player.transform;
        transform.localPosition = Vector3.zero;
        
        itemType = itemData.itemType;
        rate = itemData.damages[0];
        ApplyGear();
    }

    void ApplyGear()
    {
        switch (itemType)
        {
            case ItemData.ItemType.Glove:
                RateUp();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }
    public void LevelUp(float rate)
    {
        this.rate = rate;
    }

    private void RateUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();
        
        foreach (Weapon weapon in weapons)
        {
            switch (weapon.id)
            {
                case 0:
                    weapon.speed = 150 + (150 * rate);
                    break;
                case 1:
                    weapon.speed = 0.5f * (1f - rate);
                    break;
            }
        }
    }

    private void SpeedUp()
    {
        float speed = 0;
        GameManager.Instance.player.speed = speed + (speed * rate);
    }
    
    
}
