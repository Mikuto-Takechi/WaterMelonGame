using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ViewSample : MonoBehaviour
{
    [SerializeField] Image _greenGauge;
    [SerializeField] Image _redGauge;
    [SerializeField] Text _progressText;
    float _current = 0, _max = 0;
    Tween _redGaugeTween, _greenGaugeTween;
    void Update()
    {
        _progressText.text = $"{_current.ToString("0")} / {_max}";
    }
    public void SetMax(int value)
    {
        _max = value;
    }
    public void SetCurrent(int newValue)
    {
        float valueFrom = _current / _max;
        float valueTo = newValue / _max;

        if (_greenGaugeTween != null) _greenGaugeTween.Kill();
        _greenGaugeTween = DOTween.To(() => valueFrom,
            newValue => _greenGauge.fillAmount = newValue,
            valueTo, 0.1f);

        if (_redGaugeTween != null) _redGaugeTween.Kill();
        _redGaugeTween = DOTween.To(() => valueFrom,
                    newValue => _redGauge.fillAmount = newValue,
                    valueTo, 0.5f);
        _current = newValue;
    }
}