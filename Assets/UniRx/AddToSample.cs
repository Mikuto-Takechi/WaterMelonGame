using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class AddToSample : MonoBehaviour
{
    void Start()
    {
        // AddTo(this)�ł���GameObject��OnDestroy�ɘA�����Ď�����Dispose()������
        Observable.IntervalFrame(5).Subscribe(_ => Debug.Log("Do!")).AddTo(this);
    }
}
