using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBase<GameManager>
{
    public float GameOverLine = 10;
    public int Score { get; set; } = 0;
    public GameState GameState { get; set; } = GameState.InGame;
    protected override void DoAwake(){}
    public void GameOver()
    {
        Debug.Log("ゲームオーバー！！");
        AudioManager.Instance.StopBGM("058_BPM150");
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