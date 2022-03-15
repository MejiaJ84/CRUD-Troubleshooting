using CPW219_CRUD_Troubleshooting.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPW219_CRUD_Troubleshooting.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext context;

        public StudentsController(SchoolContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Student> students = StudentDb.GetStudents(context);
            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student s)
        {
            if (ModelState.IsValid)
            {
                StudentDb.Add(s, context);
                ViewData["Message"] = $"{s.Name} was added!";
                return View();
            }

            //Show web page with errors
            return View(s);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            //get the product by id
            Student s = StudentDb.GetStudent(context, id);
            if(s == null)
            {
                return NotFound();
            }
            //show it on web page
            return View(s);
        }

        [HttpPost]
        public IActionResult Edit(Student p)
        {
            if (ModelState.IsValid)
            {
                StudentDb.Update(context, p);
                TempData["Message"] = $"{p.Name} was updated successfully!";
                return RedirectToAction("Index");
            }
            //return view with errors
            return View(p);
        }

        public IActionResult Delete(int id)
        {
            Student p = StudentDb.GetStudent(context, id);
            if (p == null)
            {
                return NotFound();
            }
            return View(p);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            //Get Product from database
            Student p = StudentDb.GetStudent(context, id);
            if (p != null)
            {
                StudentDb.Delete(context, p);
                TempData["Message"] = $"{p.Name} was deleted successfully!";
                return RedirectToAction("Index");
            }
            TempData["Message"] = "This Student was already deleted or not in the database.";
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            Student s = StudentDb.GetStudent(context, id);
            if (s == null)
            {
                return NotFound();
            }
            return View(s);
        }
    }
}
