using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RDK.Core.Cryptography
{
    public static class Cryptography
    {
        #region MD5

        /// <summary>
        ///   Get the md5 from a string
        /// </summary>
        /// <param name = "input">String input</param>
        /// <returns>MD5 Hash</returns>
        public static string GetMD5Hash(string input)
        {
            string md5Result;
            MD5 md5Hasher  = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new();

            foreach (byte hashByte in data)
            {
                sBuilder.Append(hashByte.ToString("x2", CultureInfo.CurrentCulture));
            }

            md5Result = sBuilder.ToString();

            return md5Result;
        }

        public static string GetFileMD5Hash(string fileName)
        {
            string md5Result;
            StringBuilder sBuilder = new();
            MD5 md5Hasher = MD5.Create();

            using (FileStream fileStream = File.OpenRead(fileName))
            {
                foreach (byte hashByte in md5Hasher.ComputeHash(fileStream))
                {
                    sBuilder.Append(hashByte.ToString("x2").ToLower());
                }
            }

            md5Result = sBuilder.ToString();

            return md5Result;
        }

        public static string GetFileMD5HashBase64(string fileName)
        {
            MD5 md5Hasher = MD5.Create(); 
            return Convert.ToBase64String(md5Hasher.ComputeHash(File.ReadAllBytes(fileName)));
        }

        /// <summary>
        ///   Check if the given hash equals to the hash of the given string
        /// </summary>
        /// <param name = "input">String</param>
        /// <param name = "hash">MD5 hash to check</param>
        /// <returns></returns>
        public static bool VerifyMD5Hash(string input, string hash)
        {
            string hashOfInput = GetMD5Hash(input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(hashOfInput, hash) == 0;
        }

        #endregion

        #region RSA

        public static string EncryptRSA(string value, RSAParameters param)
        {
            RSACryptoServiceProvider rsa = new();
            rsa.ImportParameters(param);

            byte[] bytesToEncrypt = Encoding.UTF8.GetBytes(value);
            byte[] bytesEncrypted = rsa.Encrypt(bytesToEncrypt, false);
            string encryptedValue = Convert.ToBase64String(bytesEncrypted);

            return encryptedValue;
        }

        public static string DecryptRSA(byte[] value, RSAParameters param)
        {
            RSACryptoServiceProvider rsa = new();
            rsa.ImportParameters(param);

            string decryptedValue = Encoding.UTF8.GetString(rsa.Decrypt(value, false));

            return decryptedValue;
        }

        #endregion
    }
}
