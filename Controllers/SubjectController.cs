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
    public class SubjectController : Controller
    {
        private readonly SqliteDbContext _context;

        public SubjectController(SqliteDbContext context)
        {
            _context = context;
        }

        // GET: Subject
        public async Task<IActionResult> Index()
        {
              return _context.Subjects != null ? 
                          View(await _context.Subjects.ToListAsync()) :
                          Problem("Entity set 'SqliteDbContext.Subjects'  is null.");
        }

        // GET: Subject/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Subjects == null)
            {
                return NotFound();
            }
            
            var found = _context.Subjects.Any(subject1 => subject1.SubjectId == id);
            if (!found)
            {
                return NotFound();
            }

            var subject = _context.Subjects
                .Include(subject1 => subject1.Students)
                .Include(subject1 => subject1.Teachers)
                .First(subject1 => subject1.SubjectId == id);
            return View(subject);
        }

        // GET: Subject/Create
        public IActionResult Create()
        {
            ViewBag.Students = _context.Students;
            ViewBag.Teachers = _context.Teachers;
            return View();
        }

        // POST: Subject/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubjectId,Name")] Subject subject, int[] SelectedStudents, int[] SelectedTeachers)
        
        {
            if (ModelState.IsValid)
            {
                var SelectedStudentEntities = new List<Student>();
                foreach (var StudentId in SelectedStudents)
                {
                    var Student = _context.Students.Single(student => student.StudentId == StudentId);
                    SelectedStudentEntities.Add(Student);
                }

                subject.Students = SelectedStudentEntities;
            
                var SelectedTeacherEntities = new List<Teacher>();
                foreach (var TeacherId in SelectedTeachers)
                {
                    var Teacher = _context.Teachers.Single(teacher => teacher.TeacherId == TeacherId);
                    SelectedTeacherEntities.Add(Teacher);
                }

                subject.Teachers = SelectedTeacherEntities;
                _context.Add(subject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        // GET: Subject/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Subjects == null)
            {
                return NotFound();
            }

            var subject = _context.Subjects
                .Include(x => x.Students)
                .Include(x => x.Teachers)
                .FirstOrDefault(x => x.SubjectId == id);
            if (subject == null)
            {
                return NotFound();
            }

            // TODO Wysadzić w powietrze
            ViewBag.Students = _context.Students.Where(x => !x.SubjectsToLearn.Contains(subject));
            ViewBag.Teachers = _context.Teachers.Where(x => !x.SubjectsToTeach.Contains(subject));
            return View(subject);
        }

        // POST: Subject/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubjectId,Name")] Subject subject, int[] SelectedStudents, int[] SelectedTeachers)
        {
            if (id != subject.SubjectId)
            {
                return NotFound();
            }

            var SelectedStudentEntities = new List<Student>();
            foreach (var StudentId in SelectedStudents)
            {
                var Student = _context.Students.Single(student => student.StudentId == StudentId);
                SelectedStudentEntities.Add(Student);
            }

            subject.Students = SelectedStudentEntities;
            
            var SelectedTeacherEntities = new List<Teacher>();
            foreach (var TeacherId in SelectedTeachers)
            {
                var Teacher = _context.Teachers.Single(teacher => teacher.TeacherId == TeacherId);
                SelectedTeacherEntities.Add(Teacher);
            }

            subject.Teachers = SelectedTeacherEntities;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectExists(subject.SubjectId))
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
            return View(subject);
        }

        // GET: Subject/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Subjects == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects
                .FirstOrDefaultAsync(m => m.SubjectId == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // POST: Subject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Subjects == null)
            {
                return Problem("Entity set 'SqliteDbContext.Subjects'  is null.");
            }
            var subject = await _context.Subjects.FindAsync(id);
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectExists(int id)
        {
          return (_context.Subjects?.Any(e => e.SubjectId == id)).GetValueOrDefault();
        }
    }
}
