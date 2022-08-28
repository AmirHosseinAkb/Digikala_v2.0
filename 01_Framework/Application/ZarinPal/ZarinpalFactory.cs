using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace _01_Framework.Application.ZarinPal
{
    public class ZarinpalFactory:IZarinpalFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ZarinpalFactory(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public PaymentResponse CreatePaymentRequest(long transactionId, int amount, string description)
        {
            var payment = new ZarinpalSandbox.Payment(amount);
            var response = payment.PaymentRequest(description,
                "https://localhost:7169/UserPanel/Wallet/OnlinePayment/" + transactionId);
            return new PaymentResponse()
            {
                Authority = response.Result.Authority,
                Status = response.Result.Status
            };
        }

        public VerificationResponse CreateVerificationRequest(int amount,string authority)
        {
            var payment = new ZarinpalSandbox.Payment(amount);
            var response = payment.Verification(authority).Result;

            return new VerificationResponse()
            {
                Status = response.Status,
                RefId = response.RefId
            };
        }
    }
}
