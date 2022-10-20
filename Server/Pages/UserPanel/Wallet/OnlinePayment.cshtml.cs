using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserManagement.Application.Contracts.Transaction;

namespace Server.Pages.UserPanel.Wallet
{
    public class OnlinePaymentModel : PageModel
    {
        private readonly ITransactionApplication _transactionApplication;

        public OnlinePaymentModel(ITransactionApplication transactionApplication)
        {
            _transactionApplication = transactionApplication;
        }
        public void OnGet(long transactionId)
        {
            var requestQuery = HttpContext.Request.Query;
            if (requestQuery["Status"] != ""
                && requestQuery["Status"].ToString().ToLower() == "ok"
                && requestQuery["Authority"] != "")
            {
                var verificationResponse = _transactionApplication.TransactionVerification(transactionId, requestQuery["Authority"]);
                if (verificationResponse.Status == 100)
                {
                    _transactionApplication.ConfirmTransacttion(transactionId);
                    ViewData["RefId"] = verificationResponse.RefId;
                    ViewData["PaymentSuccess"] = true;
                }
            }
        }
    }
}
