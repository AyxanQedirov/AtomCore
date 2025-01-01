using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AtomCore.Hashing
{
    public static class Hasher
    {
        public static HashResponse GeneratePasswordHash(string password)
        {
            using HMACSHA512 algoritm = new HMACSHA512();

            HashResponse response = new();
            response.Salt = algoritm.Key;
            response.Hash = algoritm.ComputeHash(Encoding.UTF8.GetBytes(password));

            return response;
        }

        public static bool VeriftPasswordHash(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            using HMACSHA512 algoritm = new HMACSHA512(passwordSalt);
            byte[] computedHash = algoritm.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != passwordHash[i])
                    return false;
            }

            return true;
        }

    }
}
