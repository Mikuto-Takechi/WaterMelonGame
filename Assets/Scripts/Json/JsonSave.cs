using UnityEngine;
using System.IO;
using System;
using AESCryptography;

public static class JsonSave
{
    static string _testJsonFileName = "/StreamingAssets/TestSaveData.bin";
    static readonly string dataPath = Application.dataPath;
    public static void Save(SaveData saveData)
    {
        string jsonData = JsonUtility.ToJson(saveData);
        byte[] encryptedData = AES.Encrypt(jsonData, IVAndKEY.AES_IV_256, IVAndKEY.AES_Key_256);
        File.WriteAllBytes(dataPath + _testJsonFileName, encryptedData);
    }
    public static SaveData Load()
    {
        byte[] encryptedData = File.ReadAllBytes(dataPath + _testJsonFileName);
        string decryptedData = string.Empty;
        try
        {
            decryptedData = AES.Decrypt(encryptedData, IVAndKEY.AES_IV_256, IVAndKEY.AES_Key_256);
        }
        // 復号出来なかった場合の処理
        catch (Exception e)
        {
            Debug.LogException(e);
            Debug.LogError("セーブデータが破損しています");
        }
        if(decryptedData != string.Empty)
        {
            return JsonUtility.FromJson<SaveData>(decryptedData);
        }
        return default;
    }
}
[Serializable]
public struct SaveData
{
    public int BestScore;
}