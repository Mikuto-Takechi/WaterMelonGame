using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AESCryptography
{
    class AES
    {
        /// <summary>
        /// �Ώ̌��Í����g���ĕ�������Í�������
        /// </summary>
        /// <param name="text">�Í������镶����</param>
        /// <param name="iv">�Ώ̃A���S���Y���̏����x�N�^�[</param>
        /// <param name="key">�Ώ̃A���S���Y���̋��L��</param>
        /// <returns>�Í������ꂽ������</returns>
        public static byte[] Encrypt(string text, string iv, string key)
        {

            using (RijndaelManaged myRijndael = new RijndaelManaged())
            {
                // �u���b�N�T�C�Y�i�������P�ʂŏ������邩�j
                myRijndael.BlockSize = 128;
                // �Í���������AES-256���̗p
                myRijndael.KeySize = 256;
                // �Í����p���[�h
                myRijndael.Mode = CipherMode.CBC;
                // �p�f�B���O
                myRijndael.Padding = PaddingMode.PKCS7;

                myRijndael.IV = Encoding.UTF8.GetBytes(iv);
                myRijndael.Key = Encoding.UTF8.GetBytes(key);

                // �Í���
                ICryptoTransform encryptor = myRijndael.CreateEncryptor(myRijndael.Key, myRijndael.IV);

                byte[] encrypted;
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream ctStream = new CryptoStream(mStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(ctStream))
                        {
                            sw.Write(text);
                        }
                        encrypted = mStream.ToArray();
                    }
                }
                return encrypted;
            }
        }

        /// <summary>
        /// �Ώ̌��Í����g���ĈÍ����𕜍�����
        /// </summary>
        /// <param name="cipher">�Í������ꂽ������</param>
        /// <param name="iv">�Ώ̃A���S���Y���̏����x�N�^�[</param>
        /// <param name="key">�Ώ̃A���S���Y���̋��L��</param>
        /// <returns>�������ꂽ������</returns>
        public static string Decrypt(byte[] cipher, string iv, string key)
        {
            using (RijndaelManaged rijndael = new RijndaelManaged())
            {
                // �u���b�N�T�C�Y�i�������P�ʂŏ������邩�j
                rijndael.BlockSize = 128;
                // �Í���������AES-256���̗p
                rijndael.KeySize = 256;
                // �Í����p���[�h
                rijndael.Mode = CipherMode.CBC;
                // �p�f�B���O
                rijndael.Padding = PaddingMode.PKCS7;

                rijndael.IV = Encoding.UTF8.GetBytes(iv);
                rijndael.Key = Encoding.UTF8.GetBytes(key);

                ICryptoTransform decryptor = rijndael.CreateDecryptor(rijndael.Key, rijndael.IV);

                string plain = string.Empty;
                using (MemoryStream mStream = new MemoryStream(cipher))
                {
                    using (CryptoStream ctStream = new CryptoStream(mStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(ctStream))
                        {
                            plain = sr.ReadLine();
                        }
                    }
                }
                return plain;
            }
        }
    }

}