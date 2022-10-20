
namespace ShopManagement.Domain.Services
{
    public  interface IShopTransactionAcl
    {
        long AddTransaction(int amount,long orderId);
        void confirmTransaction(long transactionId);
        long GetUserWalletBallance();
        void AddtransactionForPayOrder(int amount,long orderId);
    }
}
