using System;
using System.Text;
using System.Security.Cryptography;
using System.Runtime.Intrinsics.Arm;

namespace Lab1
{
    public class SHA256Hash : IHashFunction
    {
        public string GetHash(string data)
        {
            var hash = SHA256.HashData(Encoding.UTF8.GetBytes(data));
            return string.Concat(hash.Select(x => $"{x:x2}"));
        }
    }
}
