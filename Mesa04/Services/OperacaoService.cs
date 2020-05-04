using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mesa04.Models;
using Mesa04.Services.Exceptions;

namespace Mesa04.Services
{
    public class OperacaoService
    {
        private readonly Mesa04Context _context;

        public OperacaoService(Mesa04Context context)
        {
            _context = context;
        }

        // GET: Operacaos
        public async Task<List<Operacao>> FindAllAsync()
        {
            return await _context.Operacao.ToListAsync();
        }

        // GET: Operacaos/Details/5
        public async Task<Operacao> FindByIdAsync(int? id)
        {
            return await _context.Operacao.Include(m => m.Tipo).FirstOrDefaultAsync(m => m.Id == id);
        }

        /*
        // GET: Operacaos/Create
        public IActionResult Create()
        {
            return View();
        }
        */

        // POST: Operacaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task InsertAsync(/*[Bind("Id,Data,Valor,Taxa,Despesa,FluxoMn,FluxoMe,Banco,OperacaoStatus")]*/ Operacao operacao)
        {
                _context.Add(operacao);
                await _context.SaveChangesAsync();
        }


        // POST: Operacaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task UpdateAsync(/*int id, [Bind("Id,Data,Valor,Taxa,Despesa,FluxoMn,FluxoMe, Banco,OperacaoStatus")]*/ Operacao operacao)
        {
            if (!_context.Operacao.Any(x => x.Id == operacao.Id))
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                    _context.Update(operacao);
                    await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!OperacaoExists(operacao.Id))
                {
                        throw new NotFoundException("Id not found");
                }
                else
                {
                        throw new DbConcurrencyException(e.Message);
                }
            }
            
        }


        // POST: Operacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task RemoveAsync(int id)
        {
            try
            {
                var operacao = await _context.Operacao.FindAsync(id);
                _context.Operacao.Remove(operacao);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        private bool OperacaoExists(int id)
        {
            return _context.Operacao.Any(e => e.Id == id);
        }

    }
}
