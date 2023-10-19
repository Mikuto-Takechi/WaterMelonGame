using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class LicenseDisplay : MonoBehaviour
{
    [SerializeField] Button _backButton;

    Display _displayInstance;

    [Inject]
    public void Injection(Display displayInstance)
    {
        _displayInstance = displayInstance;
    }

    void Start()
    {
        _backButton.OnClickAsObservable()
            .Subscribe(_ => _displayInstance.ChangeDisplay(Display.State.Title)).AddTo(gameObject);
        _displayInstance.CurrentDisplayState
            .Subscribe(display =>
            {
                if (display == Display.State.License)
                    Show();
                else
                    Hide();
            }).AddTo(gameObject);
    }
    void Show() => gameObject.SetActive(true);
    void Hide() => gameObject.SetActive(false);
}
