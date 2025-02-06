using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    float spawnTimer = 1f;

    private void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            spawnTimer = 2f;
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
        GameObject enemy = GameManager.Instance.poolManager.Get(Random.Range(0, 3));
        enemy.transform.position = spawnPoints[index].position;
    }
}
