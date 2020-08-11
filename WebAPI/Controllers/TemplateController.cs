using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace WebAPI.Controllers
{
    // For information, you can automatically genereate a controller using scaffolding feature in Visual Studio 
    // This is implementation of template of a controller of TemplateClass Entity and TemplateDbContext DBContext with EF Core
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateController : ControllerBase
    {
        private readonly TemplateDbContext _context;

        TemplateController(TemplateDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TemplateClass>>> GetEn()
        {
            return await _context.TemplateClasses.ToListAsync();
        }

        // GET: api/Templates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TemplateClass>> GetEntity(int id)
        {
            var entity = await _context.TemplateClasses.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return entity;
        }

        // PUT: api/Templates/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntity(int id, TemplateClass entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }

            try
            {
                _context.Update(entity);
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Templates
        [HttpPost]
        public async Task<ActionResult<TemplateClass>> PostEntity(TemplateClass entity)
        {
            _context.TemplateClasses.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EntityExists(entity.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEntity", new { id = entity.Id }, entity);
        }

        // DELETE: api/Templates/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TemplateClass>> DeleteEntity(int id)
        {
            var entity = await _context.TemplateClasses.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.TemplateClasses.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<TemplateClass>> Patch(int id, [FromBody] JsonPatchDocument<TemplateClass> EntityPatch)
        {
            if (EntityPatch != null && ModelState.IsValid)
            {
                TemplateClass entity = null;
                try
                {
                    entity = await _context.TemplateClasses.FindAsync(id);
                    EntityPatch.ApplyTo(entity);
                    _context.Update(entity);
                    _context.Entry(entity).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    return BadRequest(ModelState);
                }

                return CreatedAtAction("GetEntity", new { id }, entity);
            }
            return BadRequest();
        }

        private bool EntityExists(int id)
        {
            return _context.TemplateClasses.Any(e => e.Id == id);
        }
    }
}

