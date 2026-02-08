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
    public class OfficeController : Controller
    {
        private readonly IOfficeService _officeService;
        public OfficeController(IOfficeService officeService)
        {
            _officeService = officeService;
        }

        public async Task<IActionResult> Index()
        {
            var offices = await _officeService.GetAllAsync();
            return View(offices);
        }

        public async Task<IActionResult> Details(int id)
        {
            var office = await _officeService.GetByIdAsync(id);
            if (office == null)
            {
                return NotFound();
            }
            return View(office);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Office office)
        {
            if (ModelState.IsValid)
            {
                await _officeService.AddAsync(office);

                TempData["Mensaje"] = "La oficina se ha creado correctamente.";
                TempData["Tipo"] = "success"; // success, error, warning, info

                return RedirectToAction(nameof(Index));
            }
            return View(office);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var office = await _officeService.GetByIdAsync(id);
            if (office == null)
            {
                return NotFound();
            }
            return View(office);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Office office)
        {
            if (id != office.Nid_office)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _officeService.UpdateAsync(office);

                TempData["Mensaje"] = "La oficina se ha actualizado correctamente.";
                TempData["Tipo"] = "success"; // success, error, warning, info

                return RedirectToAction(nameof(Index));
            }
            return View(office);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var office = await _officeService.GetByIdAsync(id);
            if (office == null)
            {
                return NotFound();
            }
            return View(office);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _officeService.DeleteAsync(id);

            TempData["Mensaje"] = "La oficina se ha eliminado correctamente.";
            TempData["Tipo"] = "success"; // success, error, warning, info

            return RedirectToAction(nameof(Index));
        }
    }
}