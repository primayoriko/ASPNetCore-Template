using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ODataAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;

namespace ODataAPI.Controllers
{
    // OData API Controller must contain at least for Http Method type for OData Client fully functioned 
    // that is : GET, POST, DELETE, and PATCH
    
    // Here I give some basic template to make the controller fulfill that purpose 
    [ApiController]
    [Route("odata/[controller]")]
    public class ODataAPIController : ODataController
    {
        private readonly TemplateDbContext _context;
        public ODataAPIController(TemplateDbContext context)
        {
            _context = context;
        }

        [Route("template")]
        [ActionName("Get")]
        [ODataRoute("template")]
        [EnableQuery]
        public IActionResult Get()
        {
            var entities = _context.TemplateClasses;
            return Ok(entities);
        }

        [HttpPost]
        [Route("template")]
        [ODataRoute("template")]
        public async Task<ActionResult<TemplateClass>> Post([FromBody] TemplateClass entity)
        {
            // Add your code below, this is an example for basic implementation
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.TemplateClasses.Add(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return StatusCode(401);
            }
            return StatusCode(201);
        }

        [HttpPut]
        [Route("template({id})")]
        [ODataRoute("template({id})")]
        public async Task<ActionResult<TemplateClass>> Put(int id, TemplateClass entity)
        {
            // Add your code below, this is an example for basic implementation
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                _context.Update(entity);
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return StatusCode(401);
            }

            return StatusCode(201);
        }


        [HttpPatch]
        [Route("template({id})")]
        [ODataRoute("template({id})")]
        public async Task<ActionResult<TemplateClass>> Patch([FromODataUri] int id, [FromBody] JsonPatchDocument<TemplateClass> entityPatch)
        {
            // Add your code below, this is an example for basic implementation
            if (entityPatch != null && ModelState.IsValid)
            {
                TemplateClass entity;
                try
                {
                    entity = await _context.TemplateClasses.FindAsync(id);
                    entityPatch.ApplyTo(entity, ModelState);
                    _context.Update(entity);
                    _context.Entry(entity).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                    return StatusCode(401);
                }
                return StatusCode(201);
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("template({id})")]
        [ODataRoute("template({id})")]
        public async Task<ActionResult<TemplateClass>> Delete([FromODataUri] int id)
        {
            // Add your code below, this is an example for basic implementation
            var entity = await _context.TemplateClasses.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.TemplateClasses.Remove(entity);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }
    }
}