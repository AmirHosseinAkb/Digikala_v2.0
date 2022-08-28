using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
