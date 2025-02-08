using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public float gametime = 0f;
    float maxTime = 20f;
    public Player player;
    public PoolManager poolManager;
    
    public static GameManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        gametime += Time.deltaTime;
    }
}
