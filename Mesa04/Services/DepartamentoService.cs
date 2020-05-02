using Mesa04.Models;
using System.Collections.Generic; // para usar o list
using System.Linq;
using System.Threading.Tasks;     // para usar o Task, do processamento assyncrono
using Microsoft.EntityFrameworkCore; //para usar o ToListAsync()
using System;                        //para usar o Not Implemented Exception
using Microsoft.AspNetCore.Mvc;

namespace Mesa04.Services
{
    public class DepartamentoService
    {
        private readonly Mesa04Context _context;

        public DepartamentoService(Mesa04Context context)
        {
            _context = context;
        }

        //GET: Departamentos
        //vamos fazer o mesmo do bloco comentado acima, mas transformando para processamento assyncrono usando Tasks: que é um objeto que encapsula o processamento assyncrono
        public async Task<List<Departamento>> FindAllAsync()
        {
            return await _context.Departamento.OrderBy(x => x.Nome).ToListAsync(); //OrderBy = ordenar, linq(s => x.Nome) = ordenar pelo nome
        }


        // GET: Departamentos/Details/5
        public async Task<Departamento> FindByIdAsync(int id)
        {
            return await _context.Departamento.FirstOrDefaultAsync(m => m.Id == id);
        }
        // POST: Operadors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task InsertAsync(Departamento departamento)
        {
            _context.Departamento.Add(departamento);
            await _context.SaveChangesAsync();
        }

        //Metodo Update
        public async Task UpdateAsync(Departamento departamento)
        {
            if (! await _context.Departamento.AnyAsync(x => x.Id == departamento.Id))
            {
                throw new NotImplementedException();
            }
            try
            {
                _context.Update(departamento);
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
            var departamento = await _context.Departamento.FindAsync(id);
            _context.Departamento.Remove(departamento);
            await _context.SaveChangesAsync();
        }

        //metodo criado pelo framework no controlador, migrei para o serviço
        public async Task<bool> DepartamentoExists(int id)
        {
            return await _context.Departamento.AnyAsync(e => e.Id == id);
        }

    }
}
