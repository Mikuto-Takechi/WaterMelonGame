using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;

public static class PauseManager
{
    static bool _isPaused = false;
    static GameObject _pauseDisplay = null;
    static GameObject _instantiatedDisplay = null;
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        _pauseDisplay = Resources.Load("Prefabs/PauseDisplay") as GameObject;
        Observable.EveryUpdate().Where(_ => Input.GetButtonDown("Cancel"))
            .Where(_ => GameManager.Instance?.GameState == GameState.InGame).Subscribe(_ => Pause());
    }
    static void Pause()
    {
        if (_instantiatedDisplay == null)
        {
            _instantiatedDisplay = Object.Instantiate(_pauseDisplay);
            _instantiatedDisplay.SetActive(false);
        }
        _instantiatedDisplay.SetActive(!_instantiatedDisplay.activeSelf);
    }
}