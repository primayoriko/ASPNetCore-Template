using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCApp.Models;

namespace MVCApp.Controllers
{
    public class TemplateController : Controller
    {
        private readonly TemplateDbContext _context;

        public TemplateController(TemplateDbContext context)
        {
            _context = context;
        }

        // GET: entitys
        public async Task<IActionResult> Index()
        {
            var context = _context.TemplateClasses.Where(s => true);
            return View(await context.ToListAsync());
        }

        // GET: entitys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _context.TemplateClasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // GET: entitys/Create
        public IActionResult Create()
        {
            ViewData["Grade"] = new SelectList(_context.Class, "Grade", "Grade");
            ViewData["ClassNumber"] = new SelectList(_context.Class, "ClassNumber", "ClassNumber");
            return View();
        }

        // POST: entitys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("entityId,Name,ClassNumber,Grade")] entity entity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Grade"] = new SelectList(_context.Class, "Grade", "Grade", entity.Grade);
            ViewData["ClassNumber"] = new SelectList(_context.Class, "ClassNumber", "ClassNumber");
            return View(entity);
        }

        // GET: entitys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _context.entity.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            ViewData["Grade"] = new SelectList(_context.Class, "Grade", "Grade", entity.Grade);
            ViewData["ClassNumber"] = new SelectList(_context.Class, "ClassNumber", "ClassNumber", entity.ClassNumber);
            return View(entity);
        }

        // POST: entitys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("entityId,Name,ClassNumber,Grade")] entity entity)
        {
            if (id != entity.entityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!entityExists(entity.entityId))
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
            ViewData["Grade"] = new SelectList(_context.Class, "Grade", "Grade", entity.Grade);
            return View(entity);
        }

        // GET: entitys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _context.entity
                .Include(s => s.CurrentClass)
                .FirstOrDefaultAsync(m => m.entityId == id);
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // POST: entitys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entity = await _context.entity.FindAsync(id);
            _context.entity.Remove(entity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool entityExists(int id)
        {
            return _context.entity.Any(e => e.entityId == id);
        }
    }
}
