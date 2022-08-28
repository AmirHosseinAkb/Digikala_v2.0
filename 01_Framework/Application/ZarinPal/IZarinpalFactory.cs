using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Framework.Application.ZarinPal
{
    public interface IZarinpalFactory
    {
        PaymentResponse CreatePaymentRequest(long transactionId,int amount, string description);
        VerificationResponse CreateVerificationRequest(int amount,string authority);
    }
}
