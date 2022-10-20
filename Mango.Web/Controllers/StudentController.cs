using Microsoft.AspNetCore.Mvc;
using onlineshopping.Models;
using onlineshopping.Srvices.Entites;

namespace onlineshopping.Controllers
{
    #region Ctor
    public class StudentController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public StudentController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        #endregion

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var studentList = _appDbContext.Students.ToList();
            return View(studentList);
        }

        #endregion

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Student());
        }

        [HttpPost]
        public IActionResult Create(Student model)
        {
            _appDbContext.Students.Add(model);
            var result = _appDbContext.SaveChanges();
            if (result > 0)
                return RedirectToAction(nameof(Index));
            return View(model);
        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Edit(int StudentId)
        {
            var model = _appDbContext.Students.FirstOrDefault(x => x.StudentId == StudentId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student model)
        {
            _appDbContext.Students.Update(model);
            var result = _appDbContext.SaveChanges();
            if (result > 0)
                return RedirectToAction("Index");
            return View(model);
        }
        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var model = _appDbContext.Students.FirstOrDefault(x => x.StudentId == id);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Student model)
        {
            _appDbContext.Students.Remove(model);
            var result = _appDbContext.SaveChanges();
            if (result > 0)
                return RedirectToAction("Index");
            return View(model);
        }
        #endregion
    }
}
