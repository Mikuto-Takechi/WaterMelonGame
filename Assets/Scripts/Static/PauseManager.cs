using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class PauseManager
{
    static readonly string PAUSE_DISPLAY_PATH = "Assets/SystemPrefabs/PauseDisplay.prefab";
    static bool _isPaused = false;
    static AsyncOperationHandle<GameObject> _pauseDisplayHandle;
    static GameObject _instantiatedDisplay = null;
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        _pauseDisplayHandle = Addressables.LoadAssetAsync<GameObject>(PAUSE_DISPLAY_PATH);
        Observable.EveryUpdate().Where(_ => Input.GetButtonDown("Cancel"))
            .Where(_ => GameManager.Instance?.GameState == GameState.InGame).Subscribe(_ => Pause());
    }
    static void Pause()
    {
        if (_instantiatedDisplay == null)
        {
            _instantiatedDisplay = Object.Instantiate(_pauseDisplayHandle.Result);
            _instantiatedDisplay.SetActive(false);
        }
        _instantiatedDisplay.SetActive(!_instantiatedDisplay.activeSelf);
    }
}