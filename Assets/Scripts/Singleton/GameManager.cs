using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : SingletonBase<GameManager>
{
    public float GameOverLine = 10;
    public int Score { get; set; } = 0;
    public string ScoreText { get; set; } = "000000";
    public GameState GameState { get; set; } = GameState.InGame;
    public float BestScore { get; private set; } = 0;
    protected override void DoAwake()
    {
        BestScore = PlayerPrefs.GetFloat("BestScore");
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0, 0, 0.3f);
        Gizmos.DrawCube(new Vector3(0, GameOverLine, 0), new Vector3(10, 0.1f, 10));
    }
    public void GameOver()
    {
        Debug.Log("ゲームオーバー！！");
        AudioManager.Instance.StopBGM("058_BPM150");
        GameState = GameState.GameOver;
        if(Score > BestScore)
        {
            Debug.Log($"ベストスコア更新：{BestScore}->{Score}");
            BestScore = Score;
            PlayerPrefs.SetFloat("BestScore", BestScore);
        }
    }
    public void AddScore(int add)
    {
        DOVirtual.Float(Score, Score + add, 1, val => ScoreText = val.ToString("000000"))
          .OnComplete(() => Score += add);
    }
}
public enum GameState
{
    Title,
    InGame,
    GameOver,
}