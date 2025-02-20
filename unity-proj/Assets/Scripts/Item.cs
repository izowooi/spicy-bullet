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
    private TextMeshProUGUI textName;
    private TextMeshProUGUI textDesc;
    
    private void Awake()
    {
        icon = transform.Find("Icon").GetComponent<Image>();
        icon.sprite = itemData.itemIcon;
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];
        textName.text = itemData.itemName;
    }

    private void OnEnable()
    {
        textLevel.text = $"Lv.{level+1}";
        textDesc.text = itemData.itemDesc;
        switch (itemData.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                textDesc.text = string.Format(itemData.itemDesc, itemData.damages[level] * 100, itemData.counts[level]);
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                textDesc.text = string.Format(itemData.itemDesc, itemData.damages[level] * 100);
                break;
            case ItemData.ItemType.Heal:
                textDesc.text = itemData.itemDesc;
                break;
                
        }
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
