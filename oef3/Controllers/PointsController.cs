using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using oef3.Data;

namespace oef3.Controllers
{
    public class PointsController : Controller
    {
        private readonly oef3Context _context;

        public PointsController(oef3Context context)
        {
            _context = context;
        }

        // GET: Points
        public async Task<IActionResult> Index()
        {
            var oef3Context = _context.Points.Include(p => p.Course).Include(p => p.Student);
            return View(await oef3Context.ToListAsync());
        }

        // GET: Points/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Points == null)
            {
                return NotFound();
            }

            var points = await _context.Points
                .Include(p => p.Course)
                .Include(p => p.Student)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (points == null)
            {
                return NotFound();
            }

            return View(points);
        }

        // GET: Points/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Course.OrderBy(s => s.Name), "Id", "Name");
            ViewData["StudentId"] = new SelectList(_context.Student.OrderBy(s=>s.Name), "Id", "Name");
            return View();
        }

        // POST: Points/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,CourseId,Grade")] Points points)
        {
            if (PointsExists(points))
            {
                ModelState.AddModelError("Grade", "Student already has a grade for this course"); 
            }

            if (ModelState.IsValid)
            {
                _context.Add(points);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name", points.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name", points.StudentId);
            return View(points);
        }

        // GET: Points/Edit/5
        public async Task<IActionResult> Edit(int? StudentId, int? CourseId)
        {
            if (StudentId == null || _context.Points == null || CourseId == null)
            {
                return NotFound();
            }

            var points = await _context.Points.FindAsync(StudentId, CourseId);
            if (points == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Id", points.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name", points.StudentId);
            return View(points);
        }

        // POST: Points/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? StudentId, int? CourseId, [Bind("StudentId,CourseId,Grade")] Points points)
        {
            if (StudentId != points.StudentId || CourseId != points.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(points);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PointsExists(points))
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
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name", points.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name", points.StudentId);
            return View(points);
        }

        // GET: Points/Delete/5
        public async Task<IActionResult> Delete(int? StudentId , int? CourseId)
        {
            if (StudentId == null || _context.Points == null || CourseId == null)
            {
                return NotFound();
            }

            var points = await _context.Points
                .Include(p => p.Course)
                .Include(p => p.Student)
                .FirstOrDefaultAsync(m => m.StudentId == StudentId && m.CourseId == CourseId);
            if (points == null)
            {
                return NotFound();
            }

            return View(points);
        }

        // POST: Points/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? StudentId, int? CourseId)
        {
            if (_context.Points == null)
            {
                return Problem("Entity set 'oef3Context.Points'  is null.");
            }
            var points = await _context.Points.FindAsync(StudentId,CourseId);
            if (points != null)
            {
                _context.Points.Remove(points);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PointsExists(Points points)
        {
          return (_context.Points?.Any(e => e.StudentId == points.StudentId && e.CourseId == points.CourseId)).GetValueOrDefault();
        }
    }
}
