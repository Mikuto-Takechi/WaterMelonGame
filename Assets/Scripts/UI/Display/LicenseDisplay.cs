using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
public class LicenseDisplay : BaseDisplay
{
    [SerializeField] Button _backButton;

    protected override void DoStart()
    {
        SetState(Display.State.License);
        _backButton.OnClickAsObservable()
            .Subscribe(_ => _displayInstance.ChangeDisplay(Display.State.Main)).AddTo(gameObject);
    }
}
