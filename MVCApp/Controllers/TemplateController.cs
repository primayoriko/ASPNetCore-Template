using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        // GET: template
        public async Task<IActionResult> Index()
        {
            var context = _context.TemplateClasses.Where(s => true);
            return View(await context.ToListAsync());
        }

        // GET: template/Details/5
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

        // GET: template/Create
        public IActionResult Create()
        {
            ViewData["Grade"] = new SelectList(_context.Template2Classes, "Grade", "Grade");
            return View();
        }

        // POST: template/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Grade")] TemplateClass entity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Grade"] = new SelectList(_context.Template2Classes, "Grade", "Grade");
            return View(entity);
        }

        // GET: template/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _context.TemplateClasses.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            ViewData["Grade"] = new SelectList(_context.TemplateClasses, "Grade", "Grade", entity.Grade);
            return View(entity);
        }

        // POST: template/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Grade")] TemplateClass entity)
        {
            if (id != entity.Id)
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
                catch (DbUpdateConcurrencyException e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                    if (!EntityExists(entity.Id))
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
            ViewData["Grade"] = new SelectList(_context.TemplateClasses, "Grade", "Grade", entity.Grade);
            return View(entity);
        }

        // GET: template/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: template/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entity = await _context.TemplateClasses.FindAsync(id);
            _context.TemplateClasses.Remove(entity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntityExists(int id)
        {
            return _context.TemplateClasses.Any(e => e.Id == id);
        }
    }
}
