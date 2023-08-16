using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracking.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
