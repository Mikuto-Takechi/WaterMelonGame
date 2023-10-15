using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
// https://qiita.com/toRisouP/items/1bd2f3b07bf868953178
public class MessageSample : MonoBehaviour
{
    /// <summary>�c�莞��</summary>
    [SerializeField] float _countTimeSeconds = 30f;
    /// <summary>���Ԑ؂��ʒm����Observable</summary>
    public IObservable<Unit> OnTimeUpAsyncSubject => _onTimeUpAsyncSubject;
    /// <summary>AsyncSubject(���b�Z�[�W����x�������s�ł���Subject)</summary>
    readonly AsyncSubject<Unit> _onTimeUpAsyncSubject = new();
    IDisposable _disposable;
    void Start()
    {
        _onTimeUpAsyncSubject.Subscribe(x => Debug.Log(x));
        // �w�莞�Ԍo�߂����烁�b�Z�[�W��ʒm����
        _disposable = Observable.Timer(TimeSpan.FromSeconds(_countTimeSeconds)).Subscribe(_ => 
        {
            // Timer�����΂�����AUnit�^�̃��b�Z�[�W�𔭍s����
            // Unit�^�͂��ꎩ�g�͓��ɈӖ��������Ȃ�
            // ���b�Z�[�W�̓��e�ɈӖ��͂Ȃ��A�C�x���g�ʒm�̃^�C�~���O�݂̂��d�v�Ȏ��ɗ��p�ł���
            _onTimeUpAsyncSubject.OnNext(Unit.Default);
            _onTimeUpAsyncSubject?.OnCompleted();
        });
    }
    void OnDestroy()
    {
        // Observable���܂������Ă�����~�߂�
        _disposable?.Dispose();
        // AsyncSubject��j��
        _onTimeUpAsyncSubject.Dispose();
    }
}
