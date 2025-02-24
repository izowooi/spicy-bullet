using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResultUI : MonoBehaviour
{
    public GameObject win;
    public GameObject lose;

    
    public void Lose()
    {
        win.SetActive(true);
        lose.SetActive(true);
    }
    
    public void Win()
    {
        win.SetActive(true);
        lose.SetActive(false);
    }
    
}
