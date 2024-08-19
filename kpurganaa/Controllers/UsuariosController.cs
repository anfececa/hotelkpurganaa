using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using kpurganaa.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace kpurganaa.Controllers
{
   
    public class UsuariosController : Controller
    {
        private readonly kapurganaaContext _context;

        public UsuariosController(kapurganaaContext context)
        {
            _context = context;
        }

        
        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var kapurganaaContext = _context.Usuarios.Include(u => u.IdPersonasNavigation).Include(u => u.IdRolNavigation);
            return View(await kapurganaaContext.ToListAsync());
        }
        [Authorize(Roles = "administrador")]
        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.IdPersonasNavigation)
                .Include(u => u.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }
            
        // GET: Usuarios/Create
        public IActionResult Create()
        {
            ViewData["IdPersonas"] = new SelectList(_context.Personas, "IdPersonas", "IdPersonas");
            ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "IdRol");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuario,CorreoUsuario,ClaveUsuario,IdPersonas,IdRol")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPersonas"] = new SelectList(_context.Personas, "IdPersonas", "IdPersonas", usuario.IdPersonas);
            ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "IdRol", usuario.IdRol);
            return View(usuario);
        }
        [Authorize(Roles = "administrador")]
        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["IdPersonas"] = new SelectList(_context.Personas, "IdPersonas", "IdPersonas", usuario.IdPersonas);
            ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "IdRol", usuario.IdRol);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuario,CorreoUsuario,ClaveUsuario,IdPersonas,IdRol")] Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.IdUsuario))
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
            ViewData["IdPersonas"] = new SelectList(_context.Personas, "IdPersonas", "IdPersonas", usuario.IdPersonas);
            ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "IdRol", usuario.IdRol);
            return View(usuario);
        }
        [Authorize(Roles = "administrador")]
        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.IdPersonasNavigation)
                .Include(u => u.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'kapurganaaContext.Usuarios'  is null.");
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
          return (_context.Usuarios?.Any(e => e.IdUsuario == id)).GetValueOrDefault();
        }

        // GET: Usuarios/IniciarSesion
        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IniciarSesion(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                // Busca el usuario con el correo y contraseña proporcionados
                var user = await _context.Usuarios
                    .Include(u => u.IdRolNavigation) // Incluye el rol para los Claims
                    .FirstOrDefaultAsync(u => u.CorreoUsuario == usuario.CorreoUsuario && u.ClaveUsuario == usuario.ClaveUsuario);

                if (user != null)
                {
                    // Verificar si IdRolNavigation es null
                    var role = user.IdRolNavigation;
                    if (role == null)
                    {
                        ModelState.AddModelError("", "El usuario no tiene un rol asignado.");
                        return View(usuario);
                    }
                    // Crear una identidad de usuario con Claims    
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.CorreoUsuario),
                        new Claim(ClaimTypes.NameIdentifier, user.IdUsuario.ToString()),
                        new Claim(ClaimTypes.Role, user.IdRolNavigation.Nombre) // Asumiendo que tu modelo `Role` tiene una propiedad `Nombre`
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // Crear el principal de usuario
                    var authProperties = new AuthenticationProperties
                    {
                        // Puedes agregar propiedades adicionales aquí si es necesario
                        IsPersistent = true, // Para mantener la sesión activa incluso después de cerrar el navegador
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30) // Configura el tiempo de expiración de la cookie
                    };

                    // Autenticar al usuario
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    // Redirigir a la página principal u otra página después de iniciar sesión
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    // Si no se encuentra el usuario, muestra un mensaje de error
                    ModelState.AddModelError("", "Correo o contraseña incorrectos.");
                }
            }

            // Si llegamos aquí, algo falló; vuelve a mostrar el formulario
            return View(usuario);

        }
        public async Task<IActionResult> Salir()
        {
            // Cierra la sesión del usuario actual
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirige a la acción "IniciarSesion" en el controlador "Usuarios"
            return RedirectToAction("IniciarSesion", "Usuarios");
        }


    }
}
