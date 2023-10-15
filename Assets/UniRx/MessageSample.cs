using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
// https://qiita.com/toRisouP/items/1bd2f3b07bf868953178
public class MessageSample : MonoBehaviour
{
    /// <summary>残り時間</summary>
    [SerializeField] float _countTimeSeconds = 30f;
    /// <summary>時間切れを通知するObservable</summary>
    public IObservable<Unit> OnTimeUpAsyncSubject => _onTimeUpAsyncSubject;
    /// <summary>AsyncSubject(メッセージを一度だけ発行できるSubject)</summary>
    readonly AsyncSubject<Unit> _onTimeUpAsyncSubject = new();
    IDisposable _disposable;
    void Start()
    {
        _onTimeUpAsyncSubject.Subscribe(x => Debug.Log(x));
        // 指定時間経過したらメッセージを通知する
        _disposable = Observable.Timer(TimeSpan.FromSeconds(_countTimeSeconds)).Subscribe(_ => 
        {
            // Timerが発火したら、Unit型のメッセージを発行する
            // Unit型はそれ自身は特に意味を持たない
            // メッセージの内容に意味はなく、イベント通知のタイミングのみが重要な時に利用できる
            _onTimeUpAsyncSubject.OnNext(Unit.Default);
            _onTimeUpAsyncSubject?.OnCompleted();
        });
    }
    void OnDestroy()
    {
        // Observableがまだ動いていたら止める
        _disposable?.Dispose();
        // AsyncSubjectを破棄
        _onTimeUpAsyncSubject.Dispose();
    }
}
