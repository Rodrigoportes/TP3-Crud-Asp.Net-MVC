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
    public class TarefasController : Controller
    {
        private readonly masterContext _context;

        public TarefasController(masterContext context)
        {
            _context = context;
        }

        // GET: Tarefas
        public async Task<IActionResult> Index()
        {
            var masterContext = _context.Tarefa.Include(t => t.Especializacao).Include(t => t.Funcionario);
            return View(await masterContext.ToListAsync());
        }

        // GET: Tarefas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tarefa == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefa
                .Include(t => t.Especializacao)
                .Include(t => t.Funcionario)
                .FirstOrDefaultAsync(m => m.TarefaId == id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }

        // GET: Tarefas/Create
        public IActionResult Create()
        {
            ViewData["EspecializacaoId"] = new SelectList(_context.Especializacao, "EspecializacaoId", "EspecializacaoId");
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "FuncionarioId", "FuncionarioId");
            return View();
        }

        // POST: Tarefas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TarefaId,Nome,DataInicio,DataFim,FuncionarioId,EspecializacaoId")] Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarefa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EspecializacaoId"] = new SelectList(_context.Especializacao, "EspecializacaoId", "EspecializacaoId", tarefa.EspecializacaoId);
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "FuncionarioId", "FuncionarioId", tarefa.FuncionarioId);
            return View(tarefa);
        }

        // GET: Tarefas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tarefa == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefa.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            ViewData["EspecializacaoId"] = new SelectList(_context.Especializacao, "EspecializacaoId", "EspecializacaoId", tarefa.EspecializacaoId);
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "FuncionarioId", "FuncionarioId", tarefa.FuncionarioId);
            return View(tarefa);
        }

        // POST: Tarefas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TarefaId,Nome,DataInicio,DataFim,FuncionarioId,EspecializacaoId")] Tarefa tarefa)
        {
            if (id != tarefa.TarefaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarefa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarefaExists(tarefa.TarefaId))
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
            ViewData["EspecializacaoId"] = new SelectList(_context.Especializacao, "EspecializacaoId", "EspecializacaoId", tarefa.EspecializacaoId);
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "FuncionarioId", "FuncionarioId", tarefa.FuncionarioId);
            return View(tarefa);
        }

        // GET: Tarefas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tarefa == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefa
                .Include(t => t.Especializacao)
                .Include(t => t.Funcionario)
                .FirstOrDefaultAsync(m => m.TarefaId == id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }

        // POST: Tarefas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tarefa == null)
            {
                return Problem("Entity set 'masterContext.Tarefa'  is null.");
            }
            var tarefa = await _context.Tarefa.FindAsync(id);
            if (tarefa != null)
            {
                _context.Tarefa.Remove(tarefa);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TarefaExists(int id)
        {
          return (_context.Tarefa?.Any(e => e.TarefaId == id)).GetValueOrDefault();
        }
    }
}
