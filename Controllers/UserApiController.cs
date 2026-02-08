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
    public class UserApiController : Controller
    {
        private readonly IUserApiService _userApiService;

        public UserApiController(IUserApiService userApiService)
        {
            _userApiService = userApiService;
        }
        // GET: Users (Listar todos)
        public async Task<IActionResult> Index()
        {
            var users = await _userApiService.GetAllAsync();
            return View(users);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var user = await _userApiService.GetByIdAsync(id.Value);

            if (user == null) return NotFound();

            return View(user);
        }

        // GET: Users/Create (Mostrar formulario vacío)
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create (Recibir datos del formulario)
        [HttpPost]
        [ValidateAntiForgeryToken] // Seguridad contra ataques CSRF
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                await _userApiService.AddAsync(user);

                TempData["Mensaje"] = "El usuario se ha creado correctamente.";
                TempData["Tipo"] = "success"; // success, error, warning, info

                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5 (Mostrar formulario con datos)
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userApiService.GetByIdAsync(id);
            if (user == null) return NotFound();

            return View(user);
        }

        // POST: Users/Edit/5 (Guardar cambios)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (id != user.Nid_user) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _userApiService.UpdateAsync(user);

                    TempData["Mensaje"] = "El usuario se ha actualizado correctamente.";
                    TempData["Tipo"] = "success"; // success, error, warning, info
                }
                catch (Exception)
                {
                    // En una prueba real, aquí podrías verificar si el usuario existe
                    // Pero para este CRUD rápido, si falla retornamos la vista
                    if (await _userApiService.GetByIdAsync(id) == null)
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
            return View(user);
        }

        // GET: Users/Delete/5 (Preguntar si está seguro)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var user = await _userApiService.GetByIdAsync(id.Value);
            if (user == null) return NotFound();

            return View(user);
        }

        // POST: Users/Delete/5 (Confirmar borrado)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userApiService.DeleteAsync(id);

            TempData["Mensaje"] = "El usuario se ha eliminado correctamente.";
            TempData["Tipo"] = "success"; // success, error, warning, info

            return RedirectToAction(nameof(Index));
        }
    }
}