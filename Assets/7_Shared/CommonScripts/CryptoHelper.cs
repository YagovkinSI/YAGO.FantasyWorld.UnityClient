using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public class CryptoHelper : MonoBehaviour
{
    private static readonly byte[] key = new byte[] { 123, 42, 46, 6, 79, 79, 35, 6, 236, 7, 57, 246, 2, 3, 82, 7, 27, 2, 57, 3, 72, 6, 246, 2, 46, 7, 37, 3, 2, 67, 2, 6 };
    private static readonly byte[] iv = new byte[] { 4, 0, 25, 25, 25, 2, 26, 29, 6, 2, 62, 6, 26, 236, 53, 7 };

    public static string Encrypt<T>(T data)
    {
        var json = JsonConvert.SerializeObject(data);
        using (var aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = iv;

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(json);
                    }
                    var cryptData = Convert.ToBase64String(msEncrypt.ToArray());
                    return cryptData;
                }
            }
        }
    }

    public static T Decrypt<T>(string cryptData)
    {
        var cipherTextBytes = Convert.FromBase64String(cryptData);
        using (var aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = iv;

            var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            using (var msDecrypt = new MemoryStream(cipherTextBytes))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new StreamReader(csDecrypt))
                    {
                        var json = srDecrypt.ReadToEnd();
                        return JsonConvert.DeserializeObject<T>(json);
                    }
                }
            }
        }
    }
}
