using UnityEngine;
using System.IO;
using System;
using System.Text;

public static class JsonSave
{
    static string _testJsonFileName = "/TestSaveData.bin";
    static readonly string dataPath = Application.persistentDataPath;
    public static void Save(SaveData saveData)
    {
        string jsonData = JsonUtility.ToJson(saveData);
        byte[] encryptedData = AESSample.Encrypt(jsonData, AES_IV_KEY.AES_IV_256, AES_IV_KEY.AES_Key_256);
        File.WriteAllBytes(dataPath + _testJsonFileName, encryptedData);
    }
    public static void Load()
    {
        byte[] encryptedData = File.ReadAllBytes(dataPath + _testJsonFileName);
        try
        {
            Debug.Log(AESSample.Decrypt(encryptedData, AES_IV_KEY.AES_IV_256, AES_IV_KEY.AES_Key_256));
        }
        // Json�ւ̓W�J���s�@������̉\������
        catch (Exception e)
        {
            Debug.LogException(e);
            Debug.Log("�f�[�^���j�����Ă��܂�");
        }
    }
}
[Serializable]
public struct SaveData
{
    public string Header;
    public string Data;
}