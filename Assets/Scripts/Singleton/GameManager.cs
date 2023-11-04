using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : SingletonBase<GameManager>
{
    public float GameOverLine = 10;
    public int Score { get; set; } = 0;
    public string ScoreText { get; set; } = "000000";
    public GameState GameState { get; set; } = GameState.Title;
    SaveData _saveData = new();
    public SaveData SaveData => _saveData;
    protected override void DoAwake()
    {
        _saveData = JsonSave.Load();
    }
    public void GameOver()
    {
        Debug.Log("ゲームオーバー！！");
        AudioManager.Instance.StopBGM("058_BPM150");
        GameState = GameState.GameOver;
        if(Score > _saveData.BestScore)
        {
            Debug.Log($"ベストスコア更新：{_saveData.BestScore}->{Score}");
            _saveData.BestScore = Score;
            JsonSave.Save(_saveData);
        }
    }
    public void AddScore(int add)
    {
        DOVirtual.Float(Score, Score + add, 1, val => ScoreText = val.ToString("000000"))
          .OnComplete(() => Score += add);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0, 0, 0.3f);
        Gizmos.DrawCube(new Vector3(0, GameOverLine, 0), new Vector3(10, 0.1f, 10));
    }
}
public enum GameState
{
    Title,
    InGame,
    GameOver,
}