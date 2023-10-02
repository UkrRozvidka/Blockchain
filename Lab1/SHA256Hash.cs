using System;
using System.Text;
using System.Security.Cryptography;
using System.Runtime.Intrinsics.Arm;
using System.Text.Json;

namespace Lab1
{
    public class SHA256Hash : IHashFunction
    {
        public string GetHash(Block block)
        {
            var hash = SHA256.HashData(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(block)));
            return string.Concat(hash.Select(x => $"{x:x2}"));
        }
    }
}
