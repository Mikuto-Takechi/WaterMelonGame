using UnityEngine;
using System.IO;
using System;
using AESCryptography;

public static class JsonSave
{
    static string _testJsonFileName = "TestSaveData.bin";
    static readonly string DATA_PATH = Application.dataPath + "/StreamingAssets/";
    public static void Save(SaveData saveData)
    {
        string jsonData = JsonUtility.ToJson(saveData);
        byte[] encryptedData = AES.Encrypt(jsonData, IVAndKEY.AES_IV_256, IVAndKEY.AES_Key_256);
        File.WriteAllBytes(DATA_PATH + _testJsonFileName, encryptedData);
    }
    public static SaveData Load()
    {
        if (!File.Exists(DATA_PATH + _testJsonFileName)) // �t�@�C�����p�X��ɑ��݂��Ȃ��ꍇ��default�l��Ԃ��B
            return default;

        byte[] encryptedData = File.ReadAllBytes(DATA_PATH + _testJsonFileName);
        string decryptedData = string.Empty;
        try
        {
            decryptedData = AES.Decrypt(encryptedData, IVAndKEY.AES_IV_256, IVAndKEY.AES_Key_256);
        }
        // �����o���Ȃ������ꍇ�̏���
        catch (Exception e)
        {
            Debug.LogException(e);
            Debug.LogError("�Z�[�u�f�[�^���j�����Ă��܂�");
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