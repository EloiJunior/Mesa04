using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mesa04.Models;
using Mesa04.Services;
using System.Collections.Generic;
using Mesa04.Services.Exceptions;
using System;

namespace Mesa04.Services
{
    public class TipoOperacaoService
    {
        private readonly Mesa04Context _context;

        public TipoOperacaoService(Mesa04Context context)
        {
            _context = context;
        }

        // GET: TipoOperacaos
        public async Task<List<TipoOperacao>> FindAllAsync()
        {
            return await _context.TipoOperacao.OrderBy(x => x.Id).ToListAsync();
        }

        // GET: TipoOperacaos/Details/5
        public async Task<TipoOperacao> FindByIdAsync(int? id)
        {
            return await _context.TipoOperacao
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        // GET: TipoOperacaos/Create


        // POST: TipoOperacaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task InsertAsync([Bind("Id,Nome")] TipoOperacao tipoOperacao)
        {
                _context.TipoOperacao.Add(tipoOperacao);
                await _context.SaveChangesAsync();
        }


        // GET: TipoOperacaos/Edit/5


        // POST: TipoOperacaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task UpdateAsync([Bind("Id,Nome")] TipoOperacao tipoOperacao)
        {
            if (!await _context.TipoOperacao.AnyAsync(x => x.Id == tipoOperacao.Id))
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(tipoOperacao);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!TipoOperacaoExists(tipoOperacao.Id))
                {
                    throw new NotFoundException("Id not found");
                }
                else
                {
                    throw new NotFoundException(e.Message);
                }
            }
        }


        // GET: TipoOperacaos/Delete/5


        // POST: TipoOperacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task RemoveAsync(int id)
        {
            try
            {
                var tipoOperacao = await _context.TipoOperacao.FindAsync(id);
                _context.TipoOperacao.Remove(tipoOperacao);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }

        }


        private bool TipoOperacaoExists(int id)
        {
            return _context.TipoOperacao.Any(e => e.Id == id);
        }

    }
}
