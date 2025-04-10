using Microsoft.AspNetCore.Mvc;
using Model_View_Control.Data;
using Model_View_Control.Models;
using Model_View_Control.Models.Entity;

namespace Model_View_Control.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            var student = new Student
            {
                Name = viewModel.Name,
                email = viewModel.email,
                Phone = viewModel.Phone,
                Subscribed = viewModel.Subscribed
            };
             await _context.Students.AddAsync(student);
             await  _context.SaveChangesAsync();
             return View();
        }
    }
}
