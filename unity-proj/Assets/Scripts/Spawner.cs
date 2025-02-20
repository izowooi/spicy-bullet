using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public SpawnData[] spawnData;
    
    int level = 0;
    float spawnTimer = 1f;

    private void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
    }
    
    void Update()
    {
        if (!GameManager.Instance.isLive) return;
        
        spawnTimer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.gametime / 10f), 2);
        
        if (spawnTimer > spawnData[level].spawnTime)
        {
            spawnTimer = 0;
            SpawnEnemy();
        }
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.poolManager.Get(0);
        }
    }
    
    void SpawnEnemy()
    {
        int index = Random.Range(1, spawnPoints.Length);
        GameObject enemy = GameManager.Instance.poolManager.Get(0);
        enemy.transform.position = spawnPoints[index].position;
        enemy.GetComponent<Enemy>().Initialize(spawnData[level]);
    }
}

[Serializable]
public class SpawnData
{
    public int spawnType;
    public float spawnTime;
    public float health;
    public float speed;
}
