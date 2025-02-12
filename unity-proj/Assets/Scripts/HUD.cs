using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Level, Kill, Exp, Time, Health }
    public InfoType infoType;
    
    TextMeshProUGUI myText;
    Slider mySlider;
    
    private void Awake()
    {
        myText = GetComponent<TextMeshProUGUI>();
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (infoType)
        {
            case InfoType.Exp:
                var currentExp = GameManager.Instance.exp;
                var nextExp = GameManager.Instance.nextExp[GameManager.Instance.level];
                mySlider.value = (float)currentExp / nextExp;
                break;
            case InfoType.Level:
                myText.text = $"Lv.{GameManager.Instance.level:F0}";
                break;
            case InfoType.Kill:
                myText.text = $"{GameManager.Instance.kill:F0}";
                break;
            case InfoType.Time:
                float remainTime = GameManager.Instance.maxTime - GameManager.Instance.gametime;
                var min = (int)remainTime / 60;
                var sec = (int)remainTime % 60;
                myText.text = $"{min:D2}:{sec:D2}";
                break;
            case InfoType.Health:
                var currentHealth = GameManager.Instance.health;
                var maxHeath = GameManager.Instance.maxHealth;
                mySlider.value = (float)currentHealth / maxHeath;
                break;
                
        }
    }
}
