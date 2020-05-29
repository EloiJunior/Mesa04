using Mesa04.Models;
using System.Collections.Generic; // para usar o list
using System.Linq;
using System.Threading.Tasks;     // para usar o Task, do processamento assyncrono
using Microsoft.EntityFrameworkCore; //para usar o ToListAsync()
using System;                        //para usar o Not Implemented Exception
using Microsoft.AspNetCore.Mvc;

namespace Mesa04.Services
{
    public class MeService
    {
        private readonly Mesa04Context _context;

        public MeService(Mesa04Context context)
        {
            _context = context;
        }

        //GET: Departamentos
        //vamos fazer o mesmo do bloco comentado acima, mas transformando para processamento assyncrono usando Tasks: que é um objeto que encapsula o processamento assyncrono
        public async Task<List<Me>> FindAllAsync()
        {
            return await _context.Me.OrderBy(x => x.Nome).ToListAsync(); //OrderBy = ordenar, linq(x => x.Nome) = ordenar pelo nome

        }


        // GET: Departamentos/Details/5
        public async Task<Me> FindByIdAsync(int id)
        {
            return await _context.Me.FirstOrDefaultAsync(m => m.Id == id);
        }
        // POST: Operadors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task InsertAsync(Me me)
        {
            _context.Me.Add(me);
            await _context.SaveChangesAsync();
        }

        //Metodo Update
        public async Task UpdateAsync(Me me)
        {
            if (!await _context.Me.AnyAsync(x => x.Id == me.Id))
            {
                throw new NotImplementedException();
            }
            try
            {
                _context.Update(me);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new NotImplementedException();
            }
        }



        //Metodo Remove
        public async Task RemoveAsync(int id)
        {
            var me = await _context.Me.FindAsync(id);
            _context.Me.Remove(me);
            await _context.SaveChangesAsync();
        }

        //metodo criado pelo framework no controlador, migrei para o serviço
        public async Task<bool> MeExists(int id)
        {
            return await _context.Me.AnyAsync(e => e.Id == id);
        }

    }
}
