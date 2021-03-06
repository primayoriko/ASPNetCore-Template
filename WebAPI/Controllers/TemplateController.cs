﻿using System;
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

        // You can define as much endpoints as needed to give maximum usage that can be achieved
        // But here I give you some basic endpoint examples, as written below 

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TemplateEntity>>> GetEn()
        {
            return await _context.TemplateClasses.ToListAsync();
        }

        // GET: api/Templates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TemplateEntity>> GetEntity(int id)
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
        public async Task<IActionResult> PutEntity(int id, TemplateEntity entity)
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
            catch (DbUpdateConcurrencyException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
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
        public async Task<ActionResult<TemplateEntity>> PostEntity(TemplateEntity entity)
        {
            _context.TemplateClasses.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
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
        public async Task<ActionResult<TemplateEntity>> DeleteEntity(int id)
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
        public async Task<ActionResult<TemplateEntity>> Patch(int id, [FromBody] JsonPatchDocument<TemplateEntity> EntityPatch)
        {
            if (EntityPatch != null && ModelState.IsValid)
            {
                TemplateEntity entity = null;
                try
                {
                    entity = await _context.TemplateClasses.FindAsync(id);
                    EntityPatch.ApplyTo(entity);
                    _context.Update(entity);
                    _context.Entry(entity).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
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

