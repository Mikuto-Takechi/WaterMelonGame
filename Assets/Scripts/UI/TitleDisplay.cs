using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class TitleDisplay : MonoBehaviour
{
    [SerializeField] Button _startButton;
    [SerializeField] Button _optionButton;
    [SerializeField] Button _licenseButton;

    Display _displayInstance;

    [Inject]
    public void Injection(Display displayInstance)
    {
        _displayInstance = displayInstance;
    }

    void Start()
    {
        _optionButton.OnClickAsObservable()
            .Subscribe(_ => _displayInstance.ChangeDisplay(Display.State.Option)).AddTo(gameObject);
        _licenseButton.OnClickAsObservable()
            .Subscribe(_ => _displayInstance.ChangeDisplay(Display.State.License)).AddTo(gameObject);
        _displayInstance.CurrentDisplayState
            .Subscribe(display => 
            {
                if (display == Display.State.Title)
                    Show();
                else
                    Hide();
            }).AddTo(gameObject);
    }
    void Show() => gameObject.SetActive(true);
    void Hide() => gameObject.SetActive(false);
}
