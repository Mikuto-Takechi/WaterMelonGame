using UnityEngine;

public abstract class SingletonBase<T> : MonoBehaviour where T : Component
{
    static T _instance;
    public static T Instance { get => _instance; }
    /// <summary>Awake�̃^�C�~���O�Ŏ��s����������������</summary>
    protected abstract void DoAwake();
    protected void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
            DoAwake();
        }
    }
}