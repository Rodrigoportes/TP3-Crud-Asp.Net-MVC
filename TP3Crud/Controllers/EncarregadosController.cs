using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TP3Crud.Data;
using TP3Crud.Models;

namespace TP3Crud.Controllers
{
    public class EncarregadosController : Controller
    {
        private readonly masterContext _context;

        public EncarregadosController(masterContext context)
        {
            _context = context;
        }

        // GET: Encarregados
        public async Task<IActionResult> Index()
        {
              return _context.Encarregado != null ? 
                          View(await _context.Encarregado.ToListAsync()) :
                          Problem("Entity set 'masterContext.Encarregado'  is null.");
        }

        // GET: Encarregados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Encarregado == null)
            {
                return NotFound();
            }

            var encarregado = await _context.Encarregado
                .FirstOrDefaultAsync(m => m.EncarregadoId == id);
            if (encarregado == null)
            {
                return NotFound();
            }

            return View(encarregado);
        }

        // GET: Encarregados/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Encarregados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EncarregadoId,Nome,DataContratacao,Email")] Encarregado encarregado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(encarregado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(encarregado);
        }

        // GET: Encarregados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Encarregado == null)
            {
                return NotFound();
            }

            var encarregado = await _context.Encarregado.FindAsync(id);
            if (encarregado == null)
            {
                return NotFound();
            }
            return View(encarregado);
        }

        // POST: Encarregados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EncarregadoId,Nome,DataContratacao,Email")] Encarregado encarregado)
        {
            if (id != encarregado.EncarregadoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(encarregado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EncarregadoExists(encarregado.EncarregadoId))
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
            return View(encarregado);
        }

        // GET: Encarregados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Encarregado == null)
            {
                return NotFound();
            }

            var encarregado = await _context.Encarregado
                .FirstOrDefaultAsync(m => m.EncarregadoId == id);
            if (encarregado == null)
            {
                return NotFound();
            }

            return View(encarregado);
        }

        // POST: Encarregados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Encarregado == null)
            {
                return Problem("Entity set 'masterContext.Encarregado'  is null.");
            }
            var encarregado = await _context.Encarregado.FindAsync(id);
            if (encarregado != null)
            {
                _context.Encarregado.Remove(encarregado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EncarregadoExists(int id)
        {
          return (_context.Encarregado?.Any(e => e.EncarregadoId == id)).GetValueOrDefault();
        }
    }
}
