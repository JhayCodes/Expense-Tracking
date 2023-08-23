using ExpenseTracking.Data;
using ExpenseTracking.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracking.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;   
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objList = _db.Categories;
            return View(objList);
        }

        //Get-Create
        public IActionResult AddOrEdit(int id = 0)
        {
            if(id == 0)
            {
                var obj = new Category();
                return View(obj);
            }
            
            return View(_db.Categories.Find(id));
        }

        //Post-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(Category obj)
        {
            if(ModelState.IsValid)
            {
                if(obj.CategoryId == 0)
                {
                     _db.Categories.Add(obj);
                    _db.SaveChanges();
                    return RedirectToAction("Index","Category");
                }
                _db.Categories.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index","Category");   
            }
            return View(obj);
        }

        ////Get-Edit
        //public IActionResult Edit(int? id) 
        //{ 
        //    if(id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    var obj = _db.Categories.Find(id);
        //    if(obj == null)
        //    {
        //        return NotFound();
        //    }
        //    return View();
        //}  

        ////Post-Edit
        //public IActionResult Edit(Category obj)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        _db.Categories.Update(obj);
        //        _db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(obj);
        //}


        //Get-Delete
        //public IActionResult Delete(int? id)
        //{
        //    if(id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    var obj = _db.Categories.Find(id);
        //    if(obj == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(obj);
        //}

        //Post-Delete
        public IActionResult Delete(int? id)
        {
            var obj = _db.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index","Category");
        }
    }
}
