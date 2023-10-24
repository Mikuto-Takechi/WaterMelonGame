using UnityEngine;

public class JsonManager : MonoBehaviour
{
    void Start() => Debug.Log(Application.persistentDataPath);
    public void Save() => JsonSave.Save(new SaveData() { Header = "ƒoƒ“ƒ^ƒ“‘¾˜Y", Data = "“Œ‹žZ"});
    public void Load() => JsonSave.Load();
}