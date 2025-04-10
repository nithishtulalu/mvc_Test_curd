using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            await _context.SaveChangesAsync();
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            var data = await _context.Students.ToListAsync();
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var data = await _context.Students.FindAsync(id);

            return View(data);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(Student view)
        {
            var data = await _context.Students.FindAsync(view.Id);
            if (data is not null)
            {
                data.Name = view.Name;
                data.email = view.email;
                data.Phone = view.Phone;
                data.Subscribed = view.Subscribed;
                await _context.SaveChangesAsync();

            }
            return RedirectToAction("List", "Students");

        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Student view)
        {
            var data = await _context.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Id == view.Id);

            if (data is not null)
            {
                _context.Students.Remove(view);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("List", "Students");
        }
    }

 }
