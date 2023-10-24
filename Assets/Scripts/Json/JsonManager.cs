using UnityEngine;

public class JsonManager : MonoBehaviour
{
    void Start() => Debug.Log(Application.persistentDataPath);
    public void Save() => JsonSave.Save(new SaveData() { Header = "バンタン太郎", Data = "東京校"});
    public void Load() => JsonSave.Load();
}