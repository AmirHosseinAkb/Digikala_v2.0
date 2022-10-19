
namespace ShopManagement.Domain.Services
{
    public  interface IShopTransactionAcl
    {
        long AddTransaction(int amount,long orderId);
        void confirmTransaction(long transactionId);
        long GetUserWalletBallance();
    }
}
