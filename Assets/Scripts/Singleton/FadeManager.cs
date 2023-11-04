using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : SingletonBase<FadeManager>
{
    [SerializeField] Image _fadeImage;
    Tween _tween;
    protected override void DoAwake(){}
    public void FadeIn(Action callback)
    {
        if (_tween != null) return;
        _tween = _fadeImage.DOFade(1, 1).OnComplete(() => 
        {
            _tween = null;
            callback();
        });
    }
    public void FadeOut(Action callback)
    {
        if (_tween != null) return;
        _tween = _fadeImage.DOFade(0, 1).OnComplete(() =>
        {
            _tween = null;
            callback();
        });
    }
}