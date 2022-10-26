using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ERC.Framework.Security {
    public class CryptographyHelper {
        public string CreateHash(string field) {
            return Encoding.ASCII.GetString((new MD5CryptoServiceProvider()).ComputeHash(System.Text.Encoding.ASCII.GetBytes(field)));
        }

        public bool MatchHash(string hashData, string field) {
            if (CreateHash(field) == hashData) {
                return true;
            } else {
                return false;
            }
        }
    }
}
