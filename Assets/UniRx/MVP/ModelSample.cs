using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ModelSample : MonoBehaviour
{
    [SerializeField] int _debugMaxHp; // 300
    [SerializeField] int _debugCurrentHp; // 300
    public int MaxHp { get => _maxHp.Value; set => _maxHp.Value = value; }
    public IObservable<int> MaxChanged => _maxHp;
    private readonly ReactiveProperty<int> _maxHp = new();

    public int CurrentHp { get => _currentHp.Value; set => _currentHp.Value = value; }
    public IObservable<int> CurrentChanged => _currentHp;
    private readonly ReactiveProperty<int> _currentHp = new();
    void Start()
    {
        _maxHp.Value = _debugMaxHp;
        _currentHp.Value = _debugCurrentHp;
    }
    public void Damage()
    {
        int _next = _currentHp.Value;
        _next -= UnityEngine.Random.Range(10, 30);
        if (_next < 0)
        {
            _next = 0;
        }
        _currentHp.Value = _next;
    }

    public void Recovery()
    {
        int _next = _currentHp.Value;
        _next += UnityEngine.Random.Range(10, 30);
        if (_next > _maxHp.Value)
        {
            _next = _maxHp.Value;
        }
        _currentHp.Value = _next;
    }
}
