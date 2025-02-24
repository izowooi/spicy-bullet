using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [Header(("게임 정보"))]
    public bool isLive = true;
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
    public LevelUp uiLevelUp;
    public GameObject uiResult;
    
    public static GameManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void GameStart()
    {
        health = maxHealth;
        uiLevelUp.Select(0);
        isLive = true;
    }
    
    public void GameOver()
    {
        StartCoroutine(GameOverCoroutine());
    }
    
    IEnumerator GameOverCoroutine()
    {
        isLive = false;
        yield return new WaitForSeconds(0.5f);
        
        uiResult.SetActive(true);
        
        Stop();
    }

    public void GameRestart()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (!isLive) return;
        
        gametime += Time.deltaTime;
        
        if (gametime >= maxTime)
        {
            gametime = maxTime;
        }
    }
    
    public void GetExp(int value)
    {
        exp += value;
        if (exp >= nextExp[level])  
        {
            exp = 0;
            level++;
            uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }
    
    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}
