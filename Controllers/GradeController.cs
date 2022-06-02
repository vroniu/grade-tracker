using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using grade_tracker.Models;

namespace grade_tracker.Controllers
{
    public class GradeController : Controller
    {
        private readonly SqliteDbContext _context;

        public GradeController(SqliteDbContext context)
        {
            _context = context;
        }

        // GET: Grade
        public async Task<IActionResult> Index()
        {
            var sqliteDbContext = _context.Grades.Include(g => g.Student).Include(g => g.Subject).Include(g => g.Teacher);
            return View(await sqliteDbContext.ToListAsync());
        }

        // GET: Grade/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Grades == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades
                .Include(g => g.Student)
                .Include(g => g.Subject)
                .Include(g => g.Teacher)
                .FirstOrDefaultAsync(m => m.GradeId == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // GET: Grade/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "FullName");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "Name");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "FullName");
            return View();
        }

        // POST: Grade/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GradeId,AssignedGrade,AdditionalComment,SubjectId,StudentId,TeacherId")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "FullName", grade.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "Name", grade.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "FullName", grade.TeacherId);
            return View(grade);
        }

        // GET: Grade/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Grades == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "FullName", grade.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "Name", grade.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "FullName", grade.TeacherId);
            return View(grade);
        }

        // POST: Grade/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GradeId,AssignedGrade,AdditionalComment,SubjectId,StudentId,TeacherId")] Grade grade)
        {
            if (id != grade.GradeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeExists(grade.GradeId))
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
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "FullName", grade.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "Name", grade.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "FullName", grade.TeacherId);
            return View(grade);
        }

        // GET: Grade/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Grades == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades
                .Include(g => g.Student)
                .Include(g => g.Subject)
                .Include(g => g.Teacher)
                .FirstOrDefaultAsync(m => m.GradeId == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // POST: Grade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Grades == null)
            {
                return Problem("Entity set 'SqliteDbContext.Grades'  is null.");
            }
            var grade = await _context.Grades.FindAsync(id);
            if (grade != null)
            {
                _context.Grades.Remove(grade);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeExists(int id)
        {
          return (_context.Grades?.Any(e => e.GradeId == id)).GetValueOrDefault();
        }
    }
}
