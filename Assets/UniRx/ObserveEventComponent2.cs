using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ObserveEventComponent2 : MonoBehaviour
{
    [SerializeField] CountDownEventProvider _countDownEventProvider;
    PrintLogObserver<int> _printLogObserver;
    IDisposable _disposable;
    void Start()
    {
        _disposable = _countDownEventProvider.CountDownObservable.Subscribe(x => Debug.Log(x), Debug.LogError, () => Debug.Log("OnCompleted!"));
    }
    void OnDestroy()
    {
        _disposable?.Dispose();
    }
}
