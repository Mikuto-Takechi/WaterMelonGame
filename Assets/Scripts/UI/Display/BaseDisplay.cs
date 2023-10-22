using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

[RequireComponent(typeof(CanvasGroup))]
public abstract class BaseDisplay : MonoBehaviour
{
    protected Display _displayInstance;
    CanvasGroup _canvasGroup;
    Display.State _displayState;

    protected void SetState(Display.State state) => _displayState = state;
    protected abstract void DoStart();

    [Inject]
    public void Injection(Display displayInstance) => _displayInstance = displayInstance;
    void Start()
    {
        DoStart();
        _canvasGroup = GetComponent<CanvasGroup>();
        _displayInstance.CurrentDisplayState
            .Subscribe(display =>
            {
                if (display == _displayState)
                    Show(true);
                else
                    Show(false);
            }).AddTo(gameObject);
    }
    void Show(bool flag)
    {
        _canvasGroup.alpha = Convert.ToInt32(flag);
        _canvasGroup.blocksRaycasts = flag;
        _canvasGroup.interactable = flag;
    }
}
