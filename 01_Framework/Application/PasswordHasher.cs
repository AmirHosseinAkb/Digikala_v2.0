using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _01_Framework.Application
{
    public class PasswordHasher:IPasswordHasher
    {
        public string HashMD5(string? password)
        {
            if (password == null)
                return null;
            byte[] originalPassword;
            byte[] encodedPassword;
            MD5 md5=new MD5CryptoServiceProvider();

            originalPassword = ASCIIEncoding.Default.GetBytes(password);
            encodedPassword = md5.ComputeHash(originalPassword);
            return BitConverter.ToString(encodedPassword);
        }
    }
}
