using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ODataAPI.Models;
using Simple.OData.Client;

namespace ODataAPI.Controllers
{
    // This OData Client API Controller meant to be a base ASP .NET Core controller to get/fetch or edit data from 
    // an OData-based API, so here we are using OData Client syntax to fetch or edit data there
    // see the documentation for Simple OData Client for more info
    
    // Here this is the example that we use to fetch or edit data from OData API controller that written in Controllers/ODataAPiController.cs
    [Route("api/[controller]")]
    [ApiController]
    public class ODataClientController : ControllerBase
    {
        private readonly ODataClient _client;
        public ODataClientController()
        {
            _client = new ODataClient("<input the URL here>");
        }

        // You can define as much endpoints as needed to give maximum usage that can be achieved
        // But here I give you some basic endpoint examples, as written below 

        // GET: api/<ODataClientController>
        [HttpGet]
        [ActionName("GetAll")]
        public async Task<ActionResult<IEnumerable<TemplateClass>>> Get()
        {
            var entities = await _client
                                .For<TemplateClass>()
                                .FindEntriesAsync();
            return new ActionResult<IEnumerable<TemplateClass>>(entities);
        }

        // GET api/<ODataClientController>/5
        [HttpGet("{id}")]
        [ActionName("Get")]
        public async Task<ActionResult<TemplateClass>> Get(int id)
        {
            var TemplateClass = await _client
                                .For<TemplateClass>()
                                .Filter(x => x.Id == id)
                                .FindEntryAsync();
            return new ActionResult<TemplateClass>(TemplateClass);
        }

        // POST api/<ODataClientController>
        [HttpPost]
        public async Task<ActionResult<TemplateClass>> Post([FromBody] TemplateClass entity)
        {
            try
            {
                var client = _client
                                    .For<TemplateClass>()
                                    .Set(entity);
                var addTemplateClass = await client
                                    .InsertEntryAsync();
            }
            catch (Microsoft.OData.ODataException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return StatusCode(402);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return StatusCode(402);
            }

            return CreatedAtAction("GetAll", new { }, null);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<TemplateClass>> UpdateName(int id, [FromBody] string name)
        {
            try
            {
                await _client
                        .For<TemplateClass>()
                        .Key(id)
                        .Set(new { Name = name })
                        .UpdateEntryAsync();

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return StatusCode(402);
            }
            return RedirectToAction("Get", new { id });
        }

        // PUT api/<ODataClientController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ODataClientController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}