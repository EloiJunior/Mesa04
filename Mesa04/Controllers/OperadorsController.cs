using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mesa04.Models;
using Mesa04.Services;          // para criar injeção de dependencia com o OperadorService
using Mesa04.Models.ViewModels; // para usar o OperadorFormViewModel no metodo Create
using System.Diagnostics;  // para usar o Activity no Metodo Error
using Mesa04.Services.Exceptions;// para usar o IntegrityException no metodo Delete Post

namespace Mesa04.Controllers
{
    public class OperadorsController : Controller
    {
        private readonly OperadorService _operadorService;
        private readonly DepartamentoService _departamentoService;              // dependencia do serviço criada para usar a lista de departamentos no OperadorFormView

        public OperadorsController(OperadorService operadorService, DepartamentoService departamentoService)
        {
            _operadorService = operadorService;
            _departamentoService = departamentoService;                          // dependencia do serviço criada para usar a lista de departamentos no OperadorFormView

        }



        // GET: Operadors
        public async Task<IActionResult> Index()
        {
            /*
            return View(await _context.Operador.ToListAsync());
            */
            var list = await _operadorService.FindAllAsync();

            return View(list);

        }
        

        
        // GET: Operadors/Details/5
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
            var operador = await _context.Operador
                .FirstOrDefaultAsync(m => m.Id == id);
            */

            var operador = await _operadorService.FindByIdAsync(id.Value);

            if (operador == null)
            {
                /*
                return NotFound();
                */
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(operador);
        }
        

        
        // GET: Operadors/Create
        public async Task<IActionResult> Create()
        {
            /*
            return View();
            */

            var departamentos = await _departamentoService.FindAllAsync();                //codigo para chamar uma lista de departamentos do DepartamentoService, e guardar essa lista na variavel departamentos
            var viewModel = new OperadorFormViewModel { Departamentos = departamentos };  //codigo para instanciar um novo OperadorFormViewModel já começando com a lista de departamentos acima, e chamando esse formulario de viewModel
            return View(viewModel);                                                       //codigo que manda esse novo formulario já com a lista de departamentos criada para a View

        }


        
        // POST: Operadors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        /*
        public async Task<IActionResult> Create([Bind("Id,Nome,Email,Aniversario,SalarioBase")] Operador operador)
        */
        public async Task<IActionResult> Create(Operador operador)
        {
            if (ModelState.IsValid)
            {
                /*
                _context.Add(operador);
                await _context.SaveChangesAsync();
                */
                await _operadorService.InsertAsync(operador);
                return RedirectToAction(nameof(Index));
            }
            return View(operador);
        }
        

        
        // GET: Operadors/Edit/5
        public async Task<IActionResult> Edit(int? id) //esse id opcional colocado aqui é somente para não dar erro, pois na verdade o id é obrigatorio
        {
            if (id == null)
            {
                /*
                return NotFound();
                */
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            /*
            var operador = await _context.Operador.FindAsync(id);
            */

            var operador = await _operadorService.FindByIdAsync(id.Value); // o id.Value é porque lá encima o argumento está como opcional

            if (operador == null)
            {
                /*
                return NotFound();
                */
                return RedirectToAction(nameof(Error), new { message = "Id not found" });

            }

            List<Departamento> departamentos = await _departamentoService.FindAllAsync();
            OperadorFormViewModel viewModel = new OperadorFormViewModel { Operador = operador, Departamentos = departamentos };
            /*
            return View(operador);
            */
            return View(viewModel);

        }
        


        
        // POST: Operadors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        /*
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email,Aniversario,SalarioBase")] Operador operador)
        */
        public async Task<IActionResult> Edit(int id, Operador operador)
        {
            if (id != operador.Id)
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
                    _context.Update(operador);
                    await _context.SaveChangesAsync();
                    */
                    await _operadorService.UpdateAsync(operador);
                    
                }
                catch (DbUpdateConcurrencyException e)
                {
                    /*
                    if (!OperadorExists(operador.Id))
                    */
                    if(operador == null)
                    {
                        /*
                        return NotFound();
                        */
                        return RedirectToAction(nameof(Error), new { message = "Id not found" });

                    }
                    else
                    {
                        /*
                        throw ;
                        */
                        return RedirectToAction(nameof(Error), new { message = e.Message });

                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(operador);
        }
        

        
        // GET: Operadors/Delete/5
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
            var operador = await _context.Operador
                .FirstOrDefaultAsync(m => m.Id == id);
            */

            var operador = await _operadorService.FindByIdAsync(id.Value); //preciso usar o "Value" pois como o argumento "id" é opcional, precisamos colocar o Value para pegar o valor caso seja informado

            if (operador == null)
            {
                /*
                return NotFound();
                */
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(operador);
        }
        

        
        // POST: Operadors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*
            var operador = await _context.Operador.FindAsync(id);
            _context.Operador.Remove(operador);
            await _context.SaveChangesAsync();
            */
            try
            {
                await _operadorService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });

            }
        }

        

        /*
        private bool OperadorExists(int id)
        {
            return _context.Operador.Any(e => e.Id == id);
        }
        */

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
