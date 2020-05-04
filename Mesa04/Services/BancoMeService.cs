using Mesa04.Models;
using System.Collections.Generic; // para usar o list
using System.Linq;
using System.Threading.Tasks;     // para usar o Task, do processamento assyncrono
using Microsoft.EntityFrameworkCore; //para usar o ToListAsync()
using System;                        //para usar o Not Implemented Exception
using Microsoft.AspNetCore.Mvc;

namespace Mesa04.Services
{
    public class BancoMeService
    {
        private readonly Mesa04Context _context;

        public BancoMeService(Mesa04Context context)
        {
            _context = context;
        }

        //GET: Departamentos
        //vamos fazer o mesmo do bloco comentado acima, mas transformando para processamento assyncrono usando Tasks: que é um objeto que encapsula o processamento assyncrono
        public async Task<List<BancoMe>> FindAllAsync()
        {
            return await _context.BancoMe.OrderBy(x => x.Nome).ToListAsync(); //OrderBy = ordenar, linq(x => x.Nome) = ordenar pelo nome
            
        }


        // GET: Departamentos/Details/5
        public async Task<BancoMe> FindByIdAsync(int id)
        {
            return await _context.BancoMe.FirstOrDefaultAsync(m => m.Id == id);
        }
        // POST: Operadors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task InsertAsync(BancoMe bancoMe)
        {
            _context.BancoMe.Add(bancoMe);
            await _context.SaveChangesAsync();
        }

        //Metodo Update
        public async Task UpdateAsync(BancoMe bancoMe)
        {
            if (!await _context.BancoMe.AnyAsync(x => x.Id == bancoMe.Id))
            {
                throw new NotImplementedException();
            }
            try
            {
                _context.Update(bancoMe);
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
            var bancoMe = await _context.BancoMe.FindAsync(id);
            _context.BancoMe.Remove(bancoMe);
            await _context.SaveChangesAsync();
        }

        //metodo criado pelo framework no controlador, migrei para o serviço
        public async Task<bool> BancoMeExists(int id)
        {
            return await _context.BancoMe.AnyAsync(e => e.Id == id);
        }
    }
}
