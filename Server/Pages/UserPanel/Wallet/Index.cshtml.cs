using _01_Framework.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UserManagement.Application;
using UserManagement.Application.Contracts.Transaction;
using UserManagement.Application.Contracts.User;

namespace Server.Pages.UserPanel.Wallet
{
    public class IndexModel : PageModel
    {
        private readonly ITransactionApplication _transactionApplication;
        public List<TransactionViewModel> TransactionVMs { get; set; }
        [BindProperty]
        public TransactionCommand TransactionCommand { get; set; }
        public IndexModel(ITransactionApplication transactionApplication)
        {
            _transactionApplication=transactionApplication;
        }

        public void OnGet()
        {
            TransactionVMs = _transactionApplication.GetUserTransactionsForShow();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return RedirectToPage();
            var paymentResponse=_transactionApplication.TransactionPayment(TransactionCommand.Amount);

            if (paymentResponse.Status == 100)
                return Redirect("https://SandBox.Zarinpal.com/pg/StartPay/" + paymentResponse.Authority);

            return RedirectToPage();
        }

       
    }
}
