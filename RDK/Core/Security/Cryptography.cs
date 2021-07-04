using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using RDK.Core.Helpers;

namespace RDK.Core.Security
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
            MD5 md5Hasher = MD5.Create();
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

#pragma warning disable CA1416 // Validate platform compatibility
        /// <summary>
        /// Encryptes a string using the supplied key. Encoding is done using RSA encryption.
        /// </summary>
        /// <param name="stringToEncrypt">String that must be encrypted.</param>
        /// <param name="key">Encryptionkey.</param>
        /// <returns>A string representing a byte array separated by a minus sign.</returns>
        /// <exception cref="ArgumentException">Occurs when stringToEncrypt or key is null or empty.</exception>
        public static string Encrypt(this string stringToEncrypt, string key)
        {
            if (string.IsNullOrEmpty(stringToEncrypt))
            {
                throw new ArgumentException("An empty string value cannot be encrypted.");
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Cannot encrypt using an empty key. Please supply an encryption key.");
            }

            CspParameters cspp = new()
            {
                KeyContainerName = key
            };

            RSACryptoServiceProvider rsa = new(cspp);
            rsa.PersistKeyInCsp = true;

            byte[] bytes = rsa.Encrypt(Encoding.UTF8.GetBytes(stringToEncrypt), true);

            return BitConverter.ToString(bytes);
        }

        /// <summary>
        /// Decryptes a string using the supplied key. Decoding is done using RSA encryption.
        /// </summary>
        /// <param name="stringToDecrypt">String that must be decrypted.</param>
        /// <param name="key">Decryptionkey.</param>
        /// <returns>The decrypted string or null if decryption failed.</returns>
        /// <exception cref="ArgumentException">Occurs when stringToDecrypt or key is null or empty.</exception>
        public static string Decrypt(this string stringToDecrypt, string key)
        {
            string result = null;

            if (string.IsNullOrEmpty(stringToDecrypt))
            {
                throw new ArgumentException("An empty string value cannot be encrypted.");
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Cannot decrypt using an empty key. Please supply a decryption key.");
            }

            try
            {
                CspParameters cspp = new()
                {
                    KeyContainerName = key
                };

                RSACryptoServiceProvider rsa = new(cspp);
                rsa.PersistKeyInCsp = true;

                string[] decryptArray = stringToDecrypt.Split(new string[] { "-" }, StringSplitOptions.None);
                byte[] decryptByteArray = Array.ConvertAll(decryptArray, (s => Convert.ToByte(byte.Parse(s, NumberStyles.HexNumber))));


                byte[] bytes = rsa.Decrypt(decryptByteArray, true);

                result = Encoding.UTF8.GetString(bytes);

            }
            finally
            {
                // no need for further processing
            }

            return result;
        }
#pragma warning restore CA1416 // Validate platform compatibility

        #endregion

        #region SHA256
        public static string GetSHA256(string input)
        {
            SHA256 sha256 = SHA256.Create();
            ASCIIEncoding encoding = new();
            byte[] stream;
            StringBuilder sBuilder = new();

            stream = sha256.ComputeHash(encoding.GetBytes(input));

            for (int i = 0; i < stream.Length; i++)
            {
                sBuilder.AppendFormat("{0:x2}", stream[i]);
            }

            return sBuilder.ToString();
        }

        public static bool VerifySHA256(string input, string sha256)
        {
            string shaOfInput = GetSHA256(input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(shaOfInput, sha256) == 0;
        }

        #endregion

        public static string GeneratePassword() => Convert.ToBase64String(Encoding.Default.GetBytes(MathHelper.CryptoRandom.Next(50000, int.MaxValue).ToString()));
    }
}
