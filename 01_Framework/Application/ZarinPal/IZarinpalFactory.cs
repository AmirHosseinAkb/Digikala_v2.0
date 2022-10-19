using System.Reflection.Metadata;

namespace _01_Framework.Application.ZarinPal
{
    public interface IZarinpalFactory
    {
        PaymentResponse CreatePaymentRequest(long transactionId,int amount, string description);
        VerificationResponse CreateVerificationRequest(int amount,string authority);
        PaymentResponse CreateOrderPaymentRequest(long transactionId,long orderId, int amount, string description);
        VerificationResponse CreateOrderVerificationRequest(int amount, string authority);
    }
}
