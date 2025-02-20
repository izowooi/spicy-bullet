using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rectTransform;
    Item[] items;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show()
    {
        Next();
        rectTransform.localScale = Vector3.one;
        GameManager.Instance.Stop();
    }
    
    public void Hide()
    {
        rectTransform.localScale = Vector3.zero;
        GameManager.Instance.Resume();
    }

    public void Select(int index)
    {
        items[index].OnClick();
    }

    void Next()
    {
        foreach (var item in items)
        {
            item.gameObject.SetActive(false);
        }

        var indices = new List<int>();
        for (var i = 0; i < items.Length; i++)
            indices.Add(i);

        for (var i = 0; i < indices.Count; i++)
        {
            var randomIndex = UnityEngine.Random.Range(i, indices.Count);
            (indices[i], indices[randomIndex]) = (indices[randomIndex], indices[i]);
        }

        for (var i = 0; i < 3; i++)
        {
            if(items[indices[i]].level == items[indices[i]].itemData.damages.Length)
            {
                items[4].gameObject.SetActive(true);
                continue;
            }
            items[indices[i]].gameObject.SetActive(true);
        }
    }
}
