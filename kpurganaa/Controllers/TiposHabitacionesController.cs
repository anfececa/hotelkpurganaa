using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using kpurganaa.Models;
using Microsoft.AspNetCore.Authorization;

namespace kpurganaa.Controllers
{
    

    public class TiposHabitacionesController : Controller
    {
        private readonly kapurganaaContext _context;

        public TiposHabitacionesController(kapurganaaContext context)
        {
            _context = context;
        }

        // GET: TiposHabitaciones
        public async Task<IActionResult> Index()
        {
              return _context.TiposHabitaciones != null ? 
                          View(await _context.TiposHabitaciones.ToListAsync()) :
                          Problem("Entity set 'kapurganaaContext.TiposHabitaciones'  is null.");
        }

        // GET: TiposHabitaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TiposHabitaciones == null)
            {
                return NotFound();
            }

            var tiposHabitacione = await _context.TiposHabitaciones
                .FirstOrDefaultAsync(m => m.IdTipoHabitacion == id);
            if (tiposHabitacione == null)
            {
                return NotFound();
            }

            return View(tiposHabitacione);
        }

        // GET: TiposHabitaciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TiposHabitaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoHabitacion,Nombre,Descripcion")] TiposHabitacione tiposHabitacione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tiposHabitacione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tiposHabitacione);
        }
        
        // GET: TiposHabitaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TiposHabitaciones == null)
            {
                return NotFound();
            }

            var tiposHabitacione = await _context.TiposHabitaciones.FindAsync(id);
            if (tiposHabitacione == null)
            {
                return NotFound();
            }
            return View(tiposHabitacione);
        }

        // POST: TiposHabitaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipoHabitacion,Nombre,Descripcion")] TiposHabitacione tiposHabitacione)
        {
            if (id != tiposHabitacione.IdTipoHabitacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tiposHabitacione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TiposHabitacioneExists(tiposHabitacione.IdTipoHabitacion))
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
            return View(tiposHabitacione);
        }
        [Authorize(Roles = "administrador")]
        // GET: TiposHabitaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TiposHabitaciones == null)
            {
                return NotFound();
            }

            var tiposHabitacione = await _context.TiposHabitaciones
                .FirstOrDefaultAsync(m => m.IdTipoHabitacion == id);
            if (tiposHabitacione == null)
            {
                return NotFound();
            }

            return View(tiposHabitacione);
        }

        // POST: TiposHabitaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TiposHabitaciones == null)
            {
                return Problem("Entity set 'kapurganaaContext.TiposHabitaciones'  is null.");
            }
            var tiposHabitacione = await _context.TiposHabitaciones.FindAsync(id);
            if (tiposHabitacione != null)
            {
                _context.TiposHabitaciones.Remove(tiposHabitacione);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TiposHabitacioneExists(int id)
        {
          return (_context.TiposHabitaciones?.Any(e => e.IdTipoHabitacion == id)).GetValueOrDefault();
        }
    }
}
