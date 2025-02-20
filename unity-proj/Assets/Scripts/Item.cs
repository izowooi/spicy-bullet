using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData itemData;
    public int level;
    public Weapon weapon;
    public Gear gear;


    private Image icon;
    private TextMeshProUGUI textLevel;
    
    private void Awake()
    {
        icon = transform.Find("Icon").GetComponent<Image>();
        icon.sprite = itemData.itemIcon;
        textLevel = transform.Find("Level").GetComponent<TextMeshProUGUI>();
    }

    private void LateUpdate()
    {
        textLevel.text = $"Lv.{level+1}";
    }
    
    public void OnClick()
    {
        switch (itemData.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                if (level == 0)
                {
                    GameObject go = new GameObject();
                    weapon = go.AddComponent<Weapon>();
                    weapon.Initialize(itemData);
                }
                else
                {
                    float nextDamage = itemData.baseDamage + itemData.baseDamage * itemData.damages[level];
                    int nextCount = itemData.counts[level];
                    weapon.LevelUp(nextDamage, nextCount);
                }
                    
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                if (level == 0)
                {
                    GameObject go = new GameObject();
                    gear = go.AddComponent<Gear>();
                    gear.Initialize(itemData);
                }
                else
                {
                    float nextRate = itemData.damages[level];
                    gear.LevelUp(nextRate);
                }
            
                break;
            case ItemData.ItemType.Heal:
                GameManager.Instance.health += GameManager.Instance.maxHealth;
                break;
        }

        level++;
        if (level == itemData.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
