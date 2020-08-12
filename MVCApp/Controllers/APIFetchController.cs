using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVCApp.Controllers
{
    public class APIFetchController : Controller
    {
        // GET: APIFetchController
        public ActionResult Index()
        {
            return View();
        }

        // GET: APIFetchController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: APIFetchController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: APIFetchController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: APIFetchController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: APIFetchController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: APIFetchController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: APIFetchController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
