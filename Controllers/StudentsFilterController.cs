using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCStudentReportGenaration.Context;
using MVCStudentReportGenaration.Models;

namespace MVCStudentReportGenaration.Controllers
{
    public class StudentsFilterController : Controller
    {
        private readonly StudentDBContext _context;
        

        public StudentsFilterController(StudentDBContext context)
        {
            _context = context;
        }

        // GET: StudentsFilter
        public async Task<IActionResult> Index(string filterFN, string filterLN, string filterPlace, string filterE, int? filterAge)
        {
            // Default query
            //var studentsQuery = _context.Students.AsQueryable();
            var studentsQuery = from s in _context.Students
                                join c in _context.Courses on s.CourseId equals c.Id
                                select new Student
                                {
                                    Id = s.Id,
                                    FirstName = s.FirstName,
                                    LastName = s.LastName,
                                    DateOfBirth = s.DateOfBirth,
                                    Place = s.Place,
                                    Ethnicity = s.Ethnicity,
                                    IsDomestic = s.IsDomestic,
                                    CourseId = s.CourseId,
                                    Course = c
                                };


            // Apply filters if they are not null or empty
            if (!string.IsNullOrEmpty(filterFN))
            {
                //studentsQuery = studentsQuery.Where(s => s.FirstName.Contains(filterFN));
                studentsQuery = studentsQuery.Where(s => s.FirstName != null && s.FirstName.Contains(filterFN ?? string.Empty));

            }
            if (!string.IsNullOrEmpty(filterLN))
            {
                 
                studentsQuery = studentsQuery.Where(s => s.LastName != null && s.LastName.Contains(filterLN ?? string.Empty));
            }
            if (!string.IsNullOrEmpty(filterE))
            {    studentsQuery = studentsQuery.Where(s => s.Ethnicity != null && s.Ethnicity.Contains(filterE ?? string.Empty));
            }
            if (!string.IsNullOrEmpty(filterPlace))
            { 
                studentsQuery = studentsQuery.Where(s => s.Place != null && s.Place.Contains(filterPlace ?? string.Empty));
            }
            if (filterAge.HasValue)
            {
                var today = DateTime.Today;
                var minDateOfBirth = today.AddYears(-filterAge.Value);

                studentsQuery = studentsQuery.Where(s => s.DateOfBirth <= minDateOfBirth);
            }

            var viewModel = new StudentViewModel
            {
                Students = await studentsQuery.ToListAsync(),
                Courses = await _context.Courses.ToListAsync(), // Fetching all courses
                FilterFN = filterFN ?? string.Empty,
                FilterLN = filterLN ?? string.Empty,
                FilterPlace = filterPlace ?? string.Empty,
                FilterE = filterE ?? string.Empty,
                FilterAge = filterAge,
                Places = await _context.Students.Select(s => s.Place).Distinct().ToListAsync() ?? new List<string>(),
                Ethnicities = await _context.Students.Select(s => s.Ethnicity).Distinct().ToListAsync() ?? new List<string>()

            };

            return View(viewModel);
        }


        // GET: StudentsFilter/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: StudentsFilter/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id");
            return View();
        }

        // POST: StudentsFilter/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,DateOfBirth,Place,Ethnicity,IsDomestic,CourseId")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", student.CourseId);
            return View(student);
        }

        // GET: StudentsFilter/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", student.CourseId);
            return View(student);
        }

        // POST: StudentsFilter/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,DateOfBirth,Place,Ethnicity,IsDomestic,CourseId")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", student.CourseId);
            return View(student);
        }

        // GET: StudentsFilter/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: StudentsFilter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
