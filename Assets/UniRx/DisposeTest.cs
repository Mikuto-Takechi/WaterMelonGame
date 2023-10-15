using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class DisposeTest : MonoBehaviour
{
    void Start()
    {   
        var subject = new Subject<int>();
        IDisposable disposableA = subject.Subscribe(x => Debug.Log("A:"+ x));
        IDisposable disposableB = subject.Subscribe(x => Debug.Log("B:" + x));
        IDisposable disposableC = subject.Subscribe(x => Debug.Log("C:" + x));

        subject.OnNext(100);
        disposableA.Dispose();
        Debug.Log("---");
        subject.OnNext(200);
        subject.OnCompleted();
        subject.Dispose();
    }
}
