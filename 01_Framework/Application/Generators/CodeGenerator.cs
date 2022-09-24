using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Security;

namespace _01_Framework.Application
{
    public  class CodeGenerator
    {
        public static string GenerateUniqName()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        public static int GenerateRandomNumber(int from=10000, int to=99999)
        {
            var random = new Random();
            return random.Next(from, to);
        }

        public static string GenerateRandomString()
        {
            const int length = 3;
            var stringBuilder = new StringBuilder();
            var random = new Random();
            for (int i = 1; i <=length; i++)
            {
                var flt = random.NextDouble();
                var shift = Convert.ToInt16(Math.Floor(25 * flt));
                var letter = Convert.ToChar(65 + shift);
                stringBuilder.Append(letter);
            }
            return stringBuilder.ToString();
        }

        public static string GenerateTrackingNumber()
        {
            const string symbol="S";
            return $"{symbol}{GenerateRandomString()}{GenerateRandomNumber()}";
        }
    }
}
