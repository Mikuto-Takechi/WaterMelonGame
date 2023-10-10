using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneAdminister : SingletonBase<SceneAdminister>
{
    [SerializeField] Image _fadeImage;
    Tween _tween;
    protected override void DoAwake(){}
    public void LoadScene(string sceneName)
    {
        if (_tween != null) return;
        _tween = _fadeImage.DOFade(1, 1).OnComplete(() => 
        {
            var async = SceneManager.LoadSceneAsync(sceneName);
            async.completed += _ => _fadeImage.DOFade(0, 1).OnComplete(() => _tween = null);
        });
    }
}