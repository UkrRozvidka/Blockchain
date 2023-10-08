using System;
using System.Text;
using System.Security.Cryptography;

namespace Lab1
{
    public class KeyPair
    {
        public string PrivateKey;
        public string PublicKey;
    }

    public class Encrypt
    {
        public KeyPair GenerateKeyPair()
        { 
            using RSACryptoServiceProvider rsa = new();
            var keyPair = new KeyPair();
            keyPair.PrivateKey = Convert.ToBase64String(rsa.ExportCspBlob(true));
            keyPair.PublicKey = Convert.ToBase64String(rsa.ExportCspBlob(false));
            return keyPair;
        }

        public string SignData(string data, string privateKey)
        {
            using RSACryptoServiceProvider rsa = new();
            rsa.ImportCspBlob(Convert.FromBase64String(privateKey));
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            return Convert.ToBase64String(rsa.SignData(dataBytes, SHA256.Create()));
        }

        public bool VerifySign(string data, string publicKey, string sign)
        {
            using RSACryptoServiceProvider rsa = new();
            rsa.ImportCspBlob(Convert.FromBase64String(publicKey));
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            byte[] signBytes = Convert.FromBase64String(sign);
            return rsa.VerifyData(dataBytes, SHA256.Create(), signBytes);
        }
    }
}
