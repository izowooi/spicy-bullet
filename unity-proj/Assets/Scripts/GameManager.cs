using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [Header(("게임 정보"))]
    public float gametime = 0f;
    public float maxTime = 20f;
    
    [Header(("플레이어 정보"))]
    public float maxHealth = 100;
    public float health = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 200, 250 };

    [Header(("오브젝트"))]
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
    
    public void GetExp(int value)
    {
        exp += value;
        if (exp >= nextExp[level])
        {
            exp = 0;
            level++;
        }
    }
}
