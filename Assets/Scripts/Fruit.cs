using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Fruit : MonoBehaviour
{
    static int _totalNumber = 0;
    public int _selfNumber { get; set; } = 0;
    // フルーツのレベル
    public int _level = 0;
    bool _gameOverJudgment = false;
    Fruit _fruit;
    private void Awake()
    {
        _totalNumber++;
        _selfNumber = _totalNumber;
        _fruit = GetComponent<Fruit>();
    }
    void Update()
    {
        if(transform.position.y >= GameManager.Instance.GameOverLine && _gameOverJudgment)
        {
            GameManager.Instance.GameOver();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        _gameOverJudgment = true;
        if (collision.gameObject.TryGetComponent(out Fruit fruit))
        {
            if (fruit._level == _level && _level < 10 && _selfNumber > fruit._selfNumber)
            {
                FruitManager.instance._fruits.Add(Tuple.Create(_fruit, fruit));
            }
        }
    }
}
