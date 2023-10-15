using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserveEventComponent : MonoBehaviour
{
    [SerializeField] CountDownEventProvider _countDownEventProvider;
    PrintLogObserver<int> _printLogObserver;
    IDisposable _disposable;
    void Start()
    {
        _printLogObserver = new PrintLogObserver<int>();
        _disposable = _countDownEventProvider.CountDownObservable.Subscribe(_printLogObserver);
    }
    void OnDestroy()
    {
        _disposable?.Dispose();
    }
}
