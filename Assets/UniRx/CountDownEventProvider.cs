using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class CountDownEventProvider : MonoBehaviour
{
    [SerializeField] int _countSeconds = 10;
    Subject<int> _subject;
    public IObservable<int> CountDownObservable => _subject;
    void Awake()
    {
        _subject = new Subject<int>();
        StartCoroutine(CountCoroutine());
    }
    IEnumerator CountCoroutine()
    {
        var current = _countSeconds;
        while (current > 0)
        {
            _subject.OnNext(current);
            current--;
            yield return new WaitForSeconds(1);
        }
        _subject.OnNext(0);
        _subject.OnCompleted();
    }
    void OnDestroy()
    {
        _subject?.Dispose();
    }
}
