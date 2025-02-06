using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }
    public GameObject[] prefabs;
    private List<GameObject>[] pool;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        pool = new List<GameObject>[prefabs.Length];
        
        for (int i = 0; i < prefabs.Length; i++)
        {
            pool[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach (var item in pool[index])
        {
            if (item.activeSelf == false)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (select == null)
        {
            select = Instantiate(prefabs[index], transform);
            pool[index].Add(select);
        }
        return select;
    }
}
