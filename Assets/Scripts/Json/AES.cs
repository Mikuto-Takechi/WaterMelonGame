using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AESCryptography
{
    class AES
    {
        /// <summary>
        /// 対称鍵暗号を使って文字列を暗号化する
        /// </summary>
        /// <param name="text">暗号化する文字列</param>
        /// <param name="iv">対称アルゴリズムの初期ベクター</param>
        /// <param name="key">対称アルゴリズムの共有鍵</param>
        /// <returns>暗号化された文字列</returns>
        public static byte[] Encrypt(string text, string iv, string key)
        {

            using (RijndaelManaged myRijndael = new RijndaelManaged())
            {
                // ブロックサイズ（何文字単位で処理するか）
                myRijndael.BlockSize = 128;
                // 暗号化方式はAES-256を採用
                myRijndael.KeySize = 256;
                // 暗号利用モード
                myRijndael.Mode = CipherMode.CBC;
                // パディング
                myRijndael.Padding = PaddingMode.PKCS7;

                myRijndael.IV = Encoding.UTF8.GetBytes(iv);
                myRijndael.Key = Encoding.UTF8.GetBytes(key);

                // 暗号化
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
        /// 対称鍵暗号を使って暗号文を復号する
        /// </summary>
        /// <param name="cipher">暗号化された文字列</param>
        /// <param name="iv">対称アルゴリズムの初期ベクター</param>
        /// <param name="key">対称アルゴリズムの共有鍵</param>
        /// <returns>復号された文字列</returns>
        public static string Decrypt(byte[] cipher, string iv, string key)
        {
            using (RijndaelManaged rijndael = new RijndaelManaged())
            {
                // ブロックサイズ（何文字単位で処理するか）
                rijndael.BlockSize = 128;
                // 暗号化方式はAES-256を採用
                rijndael.KeySize = 256;
                // 暗号利用モード
                rijndael.Mode = CipherMode.CBC;
                // パディング
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