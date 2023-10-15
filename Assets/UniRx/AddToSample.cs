using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class AddToSample : MonoBehaviour
{
    void Start()
    {
        // AddTo(this)‚Å‚±‚ÌGameObject‚ÌOnDestroy‚É˜A“®‚µ‚ÄŽ©“®‚ÅDispose()‚³‚¹‚é
        Observable.IntervalFrame(5).Subscribe(_ => Debug.Log("Do!")).AddTo(this);
    }
}
