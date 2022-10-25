using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using onlineshopping.Models;
using onlineshopping.Srvices.Entites;

namespace onlineshopping.Controllers
{
    #region Ctor
    public class StudentController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StudentController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
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
            return View(new StudentModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentModel model)
        {

           var url=await Upload(model.ProfilePicture);



            var student = new Student
            {
                Address = model.Address,
                BirthDate = model.BirthDate,
                CellPhone = model.CellPhone,
                FatherName = model.FatherName,
                ImageUrl=url
            };
            _appDbContext.Students.Add(student);
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

        [HttpGet]
        public IActionResult Sample()
        {
            return View();
        }
       
        private async Task<string> Upload(IFormFile myfile)
        {
            try
            {
                string dir = Path.Combine(_webHostEnvironment.WebRootPath, "rezafiles");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                if (myfile.Length > 0)
                {
                    string filePath = Path.Combine(dir, myfile.FileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await myfile.CopyToAsync(fileStream);
                        return filePath;
                    }
                }
                return "";
            }
            catch (Exception e)
            {

                return "";
            }

            
        }

    }
}
