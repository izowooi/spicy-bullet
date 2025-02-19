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
                break;
            case ItemData.ItemType.Glove:
                break;
            case ItemData.ItemType.Shoe:
                break;
            case ItemData.ItemType.Heal:
                break;
        }

        level++;
        if (level == itemData.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
