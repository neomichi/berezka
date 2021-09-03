using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Collections;
using System.Linq;
using System.Globalization;
using Berezka.Data.Service;
using Berezka.Data;

namespace Berezka.Data
{
    public static class RSATool
    {




        public static string Encrypt(string text, string publicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024);
            rsa.FromXmlString(publicKey);

            byte[] byteText = Encoding.UTF8.GetBytes(text);
            byte[] byteEntry = rsa.Encrypt(byteText, false);

            return Convert.ToBase64String(byteEntry);
        }

        public static string Decrypt(string text, string privateKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024);
            rsa.FromXmlString(privateKey);

            byte[] byteEntry = Convert.FromBase64String(text);
            byte[] byteText = rsa.Decrypt(byteEntry, false);

            return Encoding.UTF8.GetString(byteText);
        }
                  
    }
}
