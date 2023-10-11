using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour
{
    [SerializeField] Image _greenGauge;
    [SerializeField] Image _redGauge;
    [SerializeField] Text _progressText;
    [SerializeField] float _max = 20;
    float _current = 0;
    Tween _redGaugeTween, _greenGaugeTween;
    void Start()
    {
        _current = _max;
    }
    void Update()
    {
        _progressText.text = $"{_current.ToString("0")} / {_max}";
    }
    public void Increase(float amount)
    {
        float increase = Mathf.Clamp(_current + amount, 0, _max);
        ChangeValue(increase);
        _current = increase;
    }
    public void Decrease(float amount)
    {
        float decrease = Mathf.Clamp(_current - amount, 0, _max);
        ChangeValue(decrease);
        _current = decrease;
    }
    void ChangeValue(float value)
    {
        float valueFrom = _current / _max;
        float valueTo = value / _max;

        if (_greenGaugeTween != null) _greenGaugeTween.Kill();
        _greenGaugeTween = DOTween.To(() => valueFrom,
            newValue => _greenGauge.fillAmount = newValue,
            valueTo, 0.1f);

        if (_redGaugeTween != null) _redGaugeTween.Kill();
        _redGaugeTween = DOTween.To(() => valueFrom,
                    newValue => _redGauge.fillAmount = newValue,
                    valueTo, 0.5f);
    }
}