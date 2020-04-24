using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mesa04.Models;

namespace Mesa04.Controllers
{
    public class OperacaosController : Controller
    {
        private readonly Mesa04Context _context;

        public OperacaosController(Mesa04Context context)
        {
            _context = context;
        }

        // GET: Operacaos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Operacao.ToListAsync());
        }

        // GET: Operacaos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operacao = await _context.Operacao
                .FirstOrDefaultAsync(m => m.Id == id);
            if (operacao == null)
            {
                return NotFound();
            }

            return View(operacao);
        }

        // GET: Operacaos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Operacaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Data,Valor,Taxa,Despesa,Fluxo,Banco,OperacaoStatus")] Operacao operacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(operacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(operacao);
        }

        // GET: Operacaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operacao = await _context.Operacao.FindAsync(id);
            if (operacao == null)
            {
                return NotFound();
            }
            return View(operacao);
        }

        // POST: Operacaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Data,Valor,Taxa,Despesa,Fluxo,Banco,OperacaoStatus")] Operacao operacao)
        {
            if (id != operacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(operacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperacaoExists(operacao.Id))
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
            return View(operacao);
        }

        // GET: Operacaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operacao = await _context.Operacao
                .FirstOrDefaultAsync(m => m.Id == id);
            if (operacao == null)
            {
                return NotFound();
            }

            return View(operacao);
        }

        // POST: Operacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var operacao = await _context.Operacao.FindAsync(id);
            _context.Operacao.Remove(operacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OperacaoExists(int id)
        {
            return _context.Operacao.Any(e => e.Id == id);
        }
    }
}
