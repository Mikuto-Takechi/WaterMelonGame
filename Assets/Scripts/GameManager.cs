using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float GameOverLine = 10;
    public int Score { get; set; } = 0;
    public GameState GameState { get; set; } = GameState.InGame;
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
        AudioManager.instance.StopBGM("058_BPM150");
        GameState = GameState.GameOver;
        //foreach (Fruit fruit in FindObjectsOfType<Fruit>())
        //{
        //    Destroy(fruit.gameObject);
        //}
    }
}
public enum GameState
{
    Title,
    InGame,
    GameOver,
}