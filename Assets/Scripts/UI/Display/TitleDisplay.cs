using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
public class TitleDisplay : BaseDisplay
{
    [SerializeField] Button _startButton;
    [SerializeField] Button _optionButton;
    [SerializeField] Button _licenseButton;

    protected override void DoStart()
    {
        SetState(Display.State.Main);
        _startButton.OnClickAsObservable()
            .Subscribe(_ => CustomSceneManager.LoadSceneWithFade("Ingame"));
        _optionButton.OnClickAsObservable()
            .Subscribe(_ => _displayInstance.ChangeDisplay(Display.State.Option)).AddTo(gameObject);
        _licenseButton.OnClickAsObservable()
            .Subscribe(_ => _displayInstance.ChangeDisplay(Display.State.License)).AddTo(gameObject);
    }
}
