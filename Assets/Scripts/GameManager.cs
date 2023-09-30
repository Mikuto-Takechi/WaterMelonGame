using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float GameOverLine = 10;
    public int Score { get; set; } = 0;
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void GameOver()
    {
        Debug.Log("ゲームオーバー！！");
        foreach(Fruit fruit in FindObjectsOfType<Fruit>())
        {
            Destroy(fruit.gameObject);
        }
    }
}
