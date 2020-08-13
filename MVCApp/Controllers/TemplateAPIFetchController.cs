using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCApp.Models;
using Newtonsoft.Json;

namespace MVCApp.Controllers
{
    public class TemplateAPIFetchController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public TemplateAPIFetchController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "<API Link and Path>");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string jsonData = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<IEnumerable<TemplateClass>>(jsonData);
                return View(data);
            }
            else
            {
                return NotFound();
            }
        }
        // GET: template/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = new HttpRequestMessage(HttpMethod.Get,
               $"<API Link and Path>/{id}");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string jsonData = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<TemplateClass>(jsonData);
                if (data == null)
                {
                    return NotFound();
                }
                return View(data);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: template/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: template/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Grade")] TemplateClass entity)
        {
            if (ModelState.IsValid)
            {
                var entityJson = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize<TemplateClass>(entity),
                    Encoding.UTF8, "application/json");

                var client = _clientFactory.CreateClient();

                var response = await client.PostAsync("<API Link and Path>/", entityJson);

                var success = true;

                try
                {
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                    success = false;
                }

                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
                //_context.Add(entity);
                //await _context.SaveChangesAsync();
            }
            return View(entity);
        }

        // GET: template/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = new HttpRequestMessage(HttpMethod.Get,
               $"<API Link and Path>/{id}");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string jsonData = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<TemplateClass>(jsonData);
                if (data == null)
                {
                    return NotFound();
                }
                return View(data);
            }
            else
            {
                return NotFound();
            }
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
                var entityJson = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize<TemplateClass>(entity),
                    Encoding.UTF8,
                    "application/json");

                var client = _clientFactory.CreateClient();

                var response = await client.PutAsync($"<API Link and Path>/{id}",
                    entityJson);
                var success = true;

                try
                {
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                    success = false;
                }
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(entity);
        }

        // GET: template/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = new HttpRequestMessage(HttpMethod.Get,
               $"<API Link and Path>/{id}");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string jsonData = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<TemplateClass>(jsonData);
                if (data == null)
                {
                    return NotFound();
                }
                return View(data);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: template/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.DeleteAsync($"<API Link and Path>/{id}");

            response.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(Index));
        }
    }
}
