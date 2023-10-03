using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float GameOverLine = 10;
    public int Score { get; set; } = 0;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
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
        AudioManager.instance.StopBGM("058_BPM150");
        foreach (Fruit fruit in FindObjectsOfType<Fruit>())
        {
            Destroy(fruit.gameObject);
        }
    }
}
