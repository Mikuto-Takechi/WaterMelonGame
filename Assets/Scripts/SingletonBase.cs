using UnityEngine;
public abstract class SingletonBase<T> : MonoBehaviour where T : Component
{
    public static T Instance { get; private set; }
    /// <summary>Awake�̃^�C�~���O�Ŏ��s����������������</summary>
    protected abstract void DoAwake();
    protected void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this as T;
            DontDestroyOnLoad(gameObject);
            DoAwake();
        }
    }
}
