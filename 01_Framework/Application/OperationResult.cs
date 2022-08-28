using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Framework.Application
{
    public class OperationResult
    {
        public bool IsSucceeded { get; set; }
        public string Message { get; set; }
        public bool IsNull { get; set; }

        public OperationResult()
        {
            IsSucceeded = false;
            IsNull = false;
        }

        public OperationResult Succeeded(string message="عملیات با موفقیت انجام شد")
        {
            IsSucceeded = true;
            Message = message;
            return this;
        }

        public OperationResult Failed(string message)
        {
            IsSucceeded = false;
            Message = message;
            return this;
        }

        public OperationResult NullResult(string message="نتیجه ای یافت نشد")
        {
            IsNull = true;
            Message = message;
            return this;
        }

    }
}
