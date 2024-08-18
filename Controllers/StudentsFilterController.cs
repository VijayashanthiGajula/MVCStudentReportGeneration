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
        public async Task<IActionResult> Index(string filterFN, string filterLN, string filterPlace, string filterE, string action)
        {
            // Default query
            var studentsQuery = _context.Students.AsQueryable();

            // Check if the Reset button was pressed
            if (action == "Reset")
            {
                filterFN = null;
                filterLN = null;
                filterPlace = null;
                filterE = null;
            }

            // Apply filters if they are not null or empty
            if (!string.IsNullOrEmpty(filterFN))
            {
                studentsQuery = studentsQuery.Where(s => s.FirstName.Contains(filterFN));
            }
            if (!string.IsNullOrEmpty(filterLN))
            {
                studentsQuery = studentsQuery.Where(s => s.LastName.Contains(filterLN));
            }
            if (!string.IsNullOrEmpty(filterE))
            {
                studentsQuery = studentsQuery.Where(s => s.Ethnicity.Contains(filterE));
            }
            if (!string.IsNullOrEmpty(filterPlace))
            {
                studentsQuery = studentsQuery.Where(s => s.Place.Contains(filterPlace));
            }

            var viewModel = new StudentViewModel
            {
                Students = await studentsQuery.ToListAsync(),
                FilterFN = filterFN,
                FilterLN = filterLN,
                FilterPlace = filterPlace,
                FilterE = filterE,
                Places = await _context.Students
                    .Select(s => s.Place)
                    .Distinct()
                    .ToListAsync(),
                Ethnicities = await _context.Students
                    .Select(e => e.Ethnicity)
                    .Distinct()
                    .ToListAsync()
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
