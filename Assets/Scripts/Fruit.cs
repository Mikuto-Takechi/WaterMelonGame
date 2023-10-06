using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Fruit : MonoBehaviour
{
    static int _totalNumber = 0;
    public int SelfNumber { get; set; } = 0;
    // フルーツのレベル
    public int Level = 0;
    bool _gameOverJudgment = false;
    Fruit _fruit;
    Rigidbody _rb;
    private void Awake()
    {
        _totalNumber++;
        SelfNumber = _totalNumber;
        _fruit = GetComponent<Fruit>();
        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (GameManager.Instance.GameState == GameState.GameOver)
        {
            _rb.Sleep();
            return;
        }
        if (transform.position.y >= GameManager.Instance.GameOverLine && _gameOverJudgment)
        {
            GameManager.Instance.GameOver();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if(_gameOverJudgment == false && !collision.gameObject.CompareTag("Wall"))
        {
            _gameOverJudgment = true;
            gameObject.layer = 0;
        }
        if (collision.gameObject.TryGetComponent(out Fruit fruit))
        {
            if (fruit.Level == Level && Level < 10 && SelfNumber > fruit.SelfNumber)
            {
                FruitManager.Instance.Fruits.Add(Tuple.Create(_fruit, fruit));
            }
        }
    }
}
