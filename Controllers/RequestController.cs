using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication_aspnet.Interfaces;
using WebApplication_aspnet.Models;

namespace WebApplication_aspnet.Controllers
{
    public class RequestController : Controller
    {
        private readonly IRequestService _requestService;
        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        // GET: Request (list all requests)
        public async Task<IActionResult> Index()
        {
            var requests = await _requestService.GetAllAsync();
            return View(requests);
        }

        // GET: Request/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var request = await _requestService.GetByIdAsync(id.Value);
            if (request == null) return NotFound();

            return View(request);
        }

        // GET: Request/Create (show empty form)
        public IActionResult Create()
        {
            return View();
        }

        // POST: Request/Create (receive form data)
        [HttpPost]
        [ValidateAntiForgeryToken] // Security against CSRF attacks
        public async Task<IActionResult> Create(Request request)
        {
            if (ModelState.IsValid)
            {
                await _requestService.AddAsync(request);
                return RedirectToAction(nameof(Index));
            }
            return View(request);
        }

        //POST: Request/Edit/5 (show form with data)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var request = await _requestService.GetByIdAsync(id.Value);
            if (request == null) return NotFound();

            return View(request);
        }

        // POST: Request/Edit/5 (receive form data)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Request request)
        {
            if (id != request.Id_request) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _requestService.UpdateAsync(request);
                }
                catch (Exception)
                {
                    // En una prueba real, aquí podrías verificar si la request existe
                    // Pero para este CRUD rápido, si falla retornamos la vista
                    if (await _requestService.GetByIdAsync(id) == null)
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
            return View(request);
        }

        // GET: Request/Delete/5 (ask if sure)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var request = await _requestService.GetByIdAsync(id.Value);
            if (request == null) return NotFound();

            return View(request);
        }

        // POST: Request/Delete/5 (confirm deletion)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _requestService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}