using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Framework.Application
{
    public interface IPasswordHasher
    {
        string HashMD5(string password);
    }
}
