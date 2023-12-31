using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class AddToSample : MonoBehaviour
{
    void Start()
    {
        // AddTo(this)でこのGameObjectのOnDestroyに連動して自動でDispose()させる
        Observable.IntervalFrame(5).Subscribe(_ => Debug.Log("Do!")).AddTo(this);
    }
}
