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
    public class CityController : Controller
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        // GET: City (list all cities)
        public async Task<IActionResult> Index()
        {
            var cities = await _cityService.GetAllAsync();
            return View(cities);
        }

        // GET: City/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var city = await _cityService.GetByIdAsync(id.Value);
            if (city == null) return NotFound();

            return View(city);
        }

        // GET: City/Create (show empty form)
        public IActionResult Create()
        {
            return View();
        }

        // POST: City/Create (receive form data)
        [HttpPost]
        [ValidateAntiForgeryToken] // Security against CSRF attacks
        public async Task<IActionResult> Create(City city)
        {
            if (ModelState.IsValid)
            {
                await _cityService.AddAsync(city);

                TempData["Mensaje"] = "La ciudad se ha creado correctamente.";
                TempData["Tipo"] = "success"; // success, error, warning, info

                return RedirectToAction(nameof(Index));
            }
            return View(city);
        }

        //POST: City/Edit/5 (show form with data)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var city = await _cityService.GetByIdAsync(id.Value);
            if (city == null) return NotFound();

            return View(city);
        }

        // POST: City/Edit/5 (receive form data)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, City city)
        {
            if (id != city.Nid_city) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _cityService.UpdateAsync(city);

                    TempData["Mensaje"] = "La ciudad se ha actualizado correctamente.";
                    TempData["Tipo"] = "success"; // success, error, warning, info
                }
                catch (Exception)
                {
                    // En una prueba real, aquí podrías verificar si la ciudad existe
                    // Pero para este CRUD rápido, si falla retornamos la vista
                    if (await _cityService.GetByIdAsync(id) == null)
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
            return View(city);
        }

        // GET: City/Delete/5 (ask if sure)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var city = await _cityService.GetByIdAsync(id.Value);
            if (city == null) return NotFound();

            return View(city);
        }

        // POST: City/Delete/5 (confirm deletion)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _cityService.DeleteAsync(id);

            TempData["Mensaje"] = "La ciudad se ha eliminado correctamente.";
            TempData["Tipo"] = "success"; // success, error, warning, info

            return RedirectToAction(nameof(Index));
        }
    }
}