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
    public class EspecializacaoController : Controller
    {
        private readonly masterContext _context;

        public EspecializacaoController(masterContext context)
        {
            _context = context;
        }

        // GET: Especializacao
        public async Task<IActionResult> Index()
        {
            var masterContext = _context.Especializacao.Include(e => e.Encarregado);
            return View(await masterContext.ToListAsync());
        }

        // GET: Especializacao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Especializacao == null)
            {
                return NotFound();
            }

            var especializacao = await _context.Especializacao
                .Include(e => e.Encarregado)
                .FirstOrDefaultAsync(m => m.EspecializacaoId == id);
            if (especializacao == null)
            {
                return NotFound();
            }

            return View(especializacao);
        }

        // GET: Especializacao/Create
        public IActionResult Create()
        {
            ViewData["EncarregadoId"] = new SelectList(_context.Encarregado, "EncarregadoId", "EncarregadoId");
            return View();
        }

        // POST: Especializacao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EspecializacaoId,Nome,Horas,EncarregadoId")] Especializacao especializacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(especializacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EncarregadoId"] = new SelectList(_context.Encarregado, "EncarregadoId", "EncarregadoId", especializacao.EncarregadoId);
            return View(especializacao);
        }

        // GET: Especializacao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Especializacao == null)
            {
                return NotFound();
            }

            var especializacao = await _context.Especializacao.FindAsync(id);
            if (especializacao == null)
            {
                return NotFound();
            }
            ViewData["EncarregadoId"] = new SelectList(_context.Encarregado, "EncarregadoId", "EncarregadoId", especializacao.EncarregadoId);
            return View(especializacao);
        }

        // POST: Especializacao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EspecializacaoId,Nome,Horas,EncarregadoId")] Especializacao especializacao)
        {
            if (id != especializacao.EspecializacaoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(especializacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EspecializacaoExists(especializacao.EspecializacaoId))
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
            ViewData["EncarregadoId"] = new SelectList(_context.Encarregado, "EncarregadoId", "EncarregadoId", especializacao.EncarregadoId);
            return View(especializacao);
        }

        // GET: Especializacao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Especializacao == null)
            {
                return NotFound();
            }

            var especializacao = await _context.Especializacao
                .Include(e => e.Encarregado)
                .FirstOrDefaultAsync(m => m.EspecializacaoId == id);
            if (especializacao == null)
            {
                return NotFound();
            }

            return View(especializacao);
        }

        // POST: Especializacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Especializacao == null)
            {
                return Problem("Entity set 'masterContext.Especializacao'  is null.");
            }
            var especializacao = await _context.Especializacao.FindAsync(id);
            if (especializacao != null)
            {
                _context.Especializacao.Remove(especializacao);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EspecializacaoExists(int id)
        {
          return (_context.Especializacao?.Any(e => e.EspecializacaoId == id)).GetValueOrDefault();
        }
    }
}
