using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mesa04.Models;
using Mesa04.Models.ViewModels;
using Mesa04.Services;
using System.Diagnostics;  // inclui para usar o Activity no metodo Error
using Mesa04.Services.Exceptions;

namespace Mesa04.Controllers
{
    public class ClientesController : Controller
    {
        /*
        private readonly Mesa04Context _context;
        */

        private readonly ClienteService _clienteService;
        private readonly TipoRegistroNacionalService _tipoRegistroNacionalService;


        public ClientesController (ClienteService  clienteService, TipoRegistroNacionalService tipoRegistroNacionalService)
        {
            _clienteService = clienteService;
            _tipoRegistroNacionalService = tipoRegistroNacionalService;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            /*
            return View(await _context.Cliente.ToListAsync());
            */
            var list = await _clienteService.FindAllAsync();
            return View(list);
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                /*
                return NotFound();
                */
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            /*
            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.Id == id);
                */
            var cliente = await _clienteService.FindByIdAsync(id.Value);

            if (cliente == null)
            {
                /*
                return NotFound();
                */
                return RedirectToAction(nameof(Error), new { message = "Id not found" });

            }

            return View(cliente);
        }
        
        // GET: Clientes/Create
        public async Task<IActionResult> Create()
        {
            /*
            //return View();
            */
            
            var tipoRegistroNacionals = await _tipoRegistroNacionalService.FindAllAsync();   //var tipoRegistroNacional = await _context.FindAllAsync(); //codigo para chamar uma lista de departamentos do DepartamentoService, e guardar essa lista na variavel departamentos
            var viewModel = new ClienteFormViewModel { TipoRegistroNacionals = tipoRegistroNacionals };  //codigo para instanciar um novo OperadorFormViewModel já começando com a lista de departamentos acima, e chamando esse formulario de viewModel
           return View(viewModel);                                                       //codigo que manda esse novo formulario já com a lista de departamentos criada para a View

        }
        

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[HttpPost, ActionName("Create")]
        //public async Task<IActionResult> Create([Bind("Id,Nome,Email,Aniversario,TipoRegistroNacional,RegistroNacional")] Cliente cliente)

        public async Task<IActionResult> Create([Bind("Id,Nome,Email,Aniversario,TipoRegistroNacionalId,RegistroNacional")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                /*
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                */
                await _clienteService.InsertAsync(cliente);
                return RedirectToAction(nameof(Index));

            }
            return View(cliente);
        }


        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                /*
                return NotFound();
                */
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            /*
            var cliente = await _context.Cliente.FindAsync(id);
            */
            var cliente = await _clienteService.FindByIdAsync(id.Value); // o id.Value é porque lá encima o argumento está como opcional

            if (cliente == null)
            {
                /*
                return NotFound();
                */
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            List<TipoRegistroNacional> tipoRegistroNacionals = await _tipoRegistroNacionalService.FindAllAsync();
            ClienteFormViewModel viewModel = new ClienteFormViewModel { Cliente= cliente, TipoRegistroNacionals = tipoRegistroNacionals };
            /*
            return View(cliente);
            */
            return View(viewModel);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        /*
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email,Aniversario,TipoRegistroNacional,RegistroNacional")] Cliente cliente)
        */
        public async Task<IActionResult> Edit(int id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                /*
                return NotFound();
                */
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    /*
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                    */
                    await _clienteService.UpdateAsync(cliente);

                }
                catch (DbUpdateConcurrencyException e)
                {
                    /*
                    if (!ClienteExists(cliente.Id))
                    */
                    if (cliente == null)
                    {
                        /*
                        return NotFound();
                        */
                        return RedirectToAction(nameof(Error), new { message = "Id not found" });
                    }
                    else
                    {
                        /*
                        throw;
                        */
                        return RedirectToAction(nameof(Error), new { message = e.Message });
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }


        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                /*
                return NotFound();
                */
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            /*
            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.Id == id);
            */
            var cliente = await _clienteService.FindByIdAsync(id.Value); //preciso usar o "Value" pois como o argumento "id" é opcional, precisamos colocar o Value para pegar o valor caso seja informado

            if (cliente == null)
            {
                /*
                return NotFound();
                */
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*
            var cliente = await _context.Cliente.FindAsync(id);
            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();
            */
            try
            {
                await _clienteService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }
            
        /*
        private bool ClienteExists(int id)
        {
        return _context.Cliente.Any(e => e.Id == id);
        }
        */

                
        //Metodo Error
        public IActionResult Error(string message) //metodo de erro, para personalizar na camada de serviço os erros
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }

    }
}
