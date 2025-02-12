using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Level, Kill, Exp, Time, Health }
    public InfoType infoType;
    
    Text myText;
    Slider mySlider;
    
    private void Awake()
    {
        myText = GetComponent<Text>();
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
                break;
                
        }
    }
}
