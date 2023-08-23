using ExpenseTracking.Data;
using ExpenseTracking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracking.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _db;
        public TransactionController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {

            IEnumerable<Transaction> objList = _db.Transactions.Include(t=> t.Category);
            return View(objList);
        }

        public IActionResult AddOrEdit(int id = 0)
        {
            populateCategory();
            if (id == 0)
            { 
                return View(new Transaction());
            }
            return View(_db.Transactions.Find(id));
        }

        //Post-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(Transaction obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.TransactionId == 0)
                {
                    _db.Transactions.Add(obj);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Transaction");
                }
                _db.Transactions.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index", "Transaction");
            }
            populateCategory();
            return View(obj);
        }

        //Post-Delete
        public IActionResult Delete(int? id)
        {
            var obj = _db.Transactions.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Transactions.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index", "Transaction");
        }

        [NonAction]
        public void populateCategory()
        {
            var categoryDropdown = _db.Categories.ToList();
            Category DefaultCategory = new Category() { CategoryId = 0, Title = "Choose a Category" };
            categoryDropdown.Insert(0, DefaultCategory);
            ViewBag.Category = categoryDropdown;
        }

    }
}
