using UnityEngine;

public class JsonManager : MonoBehaviour
{
    void Start() => Debug.Log(Application.persistentDataPath);
    public void Save() => JsonSave.Save(new SaveData() { Header = "�o���^�����Y", Data = "�����Z"});
    public void Load() => JsonSave.Load();
}