using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class OperatorTest : MonoBehaviour
{
    void Start()
    {
        var subject = new Subject<int>();
        subject.Subscribe(x => Debug.Log("raw:" + x));
        subject.Where(x => x > 0).Subscribe(x => Debug.Log("filter:" + x));

        subject.OnNext(1);
        subject.OnNext(-1);
        subject.OnNext(3);
        subject.OnNext(0);

        subject.OnCompleted();
        subject.Dispose();
    }
}
