using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PresenterSample : MonoBehaviour
{
    // Views
    [SerializeField] Button _buttonAttack;
    [SerializeField] Button _buttonRecovery;
    [SerializeField] ViewSample _hpProgress;

    // Models
    [SerializeField] ModelSample _progressModel;
    public void Start()
    {
        // View ¨ Model
        _buttonAttack.onClick.AsObservable().Subscribe(_ => _progressModel.Damage());
        _buttonRecovery.onClick.AsObservable().Subscribe(_ => _progressModel.Recovery());

        //Model ¨ View(HP)
        _progressModel.MaxChanged.Subscribe(value => _hpProgress.SetMax(value));
        _progressModel.CurrentChanged.Subscribe(value => _hpProgress.SetCurrent(value));
    }
}
