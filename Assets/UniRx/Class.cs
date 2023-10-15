using System;
using UnityEngine;

public class PrintLogObserver<T> : IObserver<T>
{
    public void OnCompleted()
    {
        Debug.Log("OnCompleted!");
    }

    public void OnError(Exception error)
    {
        Debug.Log(error);
    }

    public void OnNext(T value)
    {
        Debug.Log(value);
    }
}