using Crud_MVC.Data;
using Crud_MVC.Models;
using Crud_MVC.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crud_MVC.Controllers
{
    public class StudentsController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;

        public StudentsController(MVCDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var students = await mvcDemoDbContext.Students.ToListAsync();
            return View(students);
        }
        

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel addStudentRequest)
        {
            var student = new Student()
            {
                Id = Guid.NewGuid(),
                Name = addStudentRequest.Name,
                Email = addStudentRequest.Email,
                StudentId = addStudentRequest.StudentId,
                Department = addStudentRequest.Department,
                DateOfBirth = addStudentRequest.DateOfBirth,
            };

            await mvcDemoDbContext.Students.AddAsync(student);
            await mvcDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]

        public async Task<IActionResult> View(Guid id)
        {
            var student =await mvcDemoDbContext.Students.FirstOrDefaultAsync(x => x.Id == id);

            if (student != null)
            {
                var viewModel = new UpdateStudentViewModel()
                {
                    Id = Guid.NewGuid(),
                    Name = student.Name,
                    Email = student.Email,
                    StudentId = student.StudentId,
                    Department = student.Department,
                    DateOfBirth = student.DateOfBirth,
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateStudentViewModel model)
        {
            var student = await mvcDemoDbContext.Students.FindAsync(model.Id);

            if (student != null)
            {
                student.Name= model.Name;
                student.Email = model.Email;
                student.StudentId = model.StudentId;
                student.DateOfBirth = model.DateOfBirth;
                student.Department = model.Department;

                await mvcDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateStudentViewModel model)
        {
            var student = await mvcDemoDbContext.Students.FindAsync(model.Id);

            if (student != null)
            {
                mvcDemoDbContext.Students.Remove(student);
                await mvcDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
