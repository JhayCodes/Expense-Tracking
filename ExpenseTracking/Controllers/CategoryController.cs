using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracking.Controllers
{
    public class CategoryController : Controller
    {
       
        public CategoryController()
        {
            
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
