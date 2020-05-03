using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mesa04.Models;
using System.Collections.Generic;
using Mesa04.Services.Exceptions;

namespace Mesa04.Services
{
    public class OperacaoStatusService
    {
        private readonly Mesa04Context _context;

        public OperacaoStatusService(Mesa04Context context)
        {
            _context = context;
        }

        // GET: TipoOperacaos
        public async Task<List<OperacaoStatus>> FindAllAsync()
        {
            return await _context.OperacaoStatus.OrderBy(x => x.Id).ToListAsync();
        }

        // GET: TipoOperacaos/Details/5
        public async Task<OperacaoStatus> FindByIdAsync(int? id)
        {
            return await _context.OperacaoStatus
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        // GET: TipoOperacaos/Create


        // POST: TipoOperacaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task InsertAsync([Bind("Id,Nome")] OperacaoStatus operacaoStatus)
        {
            _context.OperacaoStatus.Add(operacaoStatus);
            await _context.SaveChangesAsync();
        }


        // GET: TipoOperacaos/Edit/5


        // POST: TipoOperacaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task UpdateAsync([Bind("Id,Nome")] OperacaoStatus operacaoStatus)
        {
            if (!await _context.OperacaoStatus.AnyAsync(x => x.Id == operacaoStatus.Id))
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(operacaoStatus);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!OperacaoStatusExists(operacaoStatus.Id))
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
                var operacaoStatus = await _context.OperacaoStatus.FindAsync(id);
                _context.OperacaoStatus.Remove(operacaoStatus);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }

        }


        private bool OperacaoStatusExists(int id)
        {
            return _context.OperacaoStatus.Any(e => e.Id == id);
        }

    }
}
