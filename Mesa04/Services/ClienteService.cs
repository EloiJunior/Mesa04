using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;  //para usar o ".Include" no Metodo FindByIdAsync
using Mesa04.Models;
using Mesa04.Services.Exceptions; //para usar o erro personalizado no Metodo Update

namespace Mesa04.Services
{
    public class ClienteService
    {
        private readonly Mesa04Context _context;

        public ClienteService(Mesa04Context context)
        {
            _context = context;
        }



        // GET: Operadors
        /*
        public async Task<IActionResult> Index()
        */
        public async Task<List<Cliente>> FindAllAsync()
        {                 /////////////   ////////////  
            /*
            return View(await _context.Operador.ToListAsync());
            */

            return await _context.Cliente.OrderBy(x => x.Nome).Include(x => x.TipoRegistroNacional).ToListAsync();
        }        //                        //////////////////// ////////////////////////////



        // GET: Operadors/Details/5
        /*
        public async Task<IActionResult> Details(int? id)  
        */
        public async Task<Cliente> FindByIdAsync(int? id)
        {                 ////////  /////////////
                          /*
                          if (id == null)
                          {
                              return NotFound();
                          }
                          */

            /*
            var operador = await _context.Operador.FirstOrDefaultAsync(m => m.Id == id);
            */

            return await _context.Cliente.Include(m => m.TipoRegistroNacional).FirstOrDefaultAsync(m => m.Id == id);
            //////                        /////////////////////////////

            /*
            if (operador == null)
            {
                return NotFound();
            }
            */

            /*
            return View(operador);
            */
        }

        /*
        // GET: Operadors/Create
        public IActionResult Create()
        {
            return View();
        }
        */

        // POST: Operadors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        /*
        public async Task<IActionResult> Create([Bind("Id,Nome,Email,Aniversario,SalarioBase")] Operador operador)
        */
        public async Task InsertAsync(Cliente cliente)
        {
            /*
            if (ModelState.IsValid)
            {
            }
            */
            _context.Add(cliente);
            await _context.SaveChangesAsync();

            /*
            return RedirectToAction(nameof(Index));

        return View(operador);
        */
        }



        /*
        // GET: Operadors/Edit/5
        
        public async Task<IActionResult> Edit(int? id)
               
        {
            
            if (id == null)
            
            
            {
                
                return NotFound();
                
                //throw new NotFoundException("Id not found");
            }
            
            var operador = await _context.Operador.FindAsync(id);
            
            //_context.Update(operador);

            if (operador == null)
            {
                return NotFound();
            }
            return View(operador);
        }
        */



        // POST: Operadors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        /*
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email,Aniversario,SalarioBase")] Operador operador)
        */
        public async Task UpdateAsync(Cliente cliente)
        {
            /*
            if (id != operador.Id)
            */
            if (!_context.Cliente.Any(x => x.Id == cliente.Id))
            {
                throw new NotFoundException("Id not found");
            }

            /*
            if (ModelState.IsValid)
            {
            */
            try
            {
                _context.Update(cliente);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!ClienteExists(cliente.Id))
                {
                    throw new NotFoundException("Id not found");
                }
                else
                {
                    throw new DbConcurrencyException(e.Message);
                }
            }
            /*
                return RedirectToAction(nameof(Index));
            
            }
            
            return View(operador);
            */

        }


        /*
        // GET: Operadors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operador = await _context.Operador
                .FirstOrDefaultAsync(m => m.Id == id);
            if (operador == null)
            {
                return NotFound();
            }

            return View(operador);
        }
        */

        // POST: Operadors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        /*
          public async Task<IActionResult> DeleteConfirmed(int id)
        */
        public async Task RemoveAsync(int id)
        {
            try
            {
                var cliente = await _context.Cliente.FindAsync(id);
                _context.Cliente.Remove(cliente);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
            /*
            return RedirectToAction(nameof(Index));
            */

        }

        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.Id == id);
        }

    }
}
