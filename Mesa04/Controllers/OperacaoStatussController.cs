//using System; //excluir
//using System.Collections.Generic; //excluir
//using System.Linq; //excluir
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering; //excluir
using Microsoft.EntityFrameworkCore;
using Mesa04.Models;
using Mesa04.Services;//incluir para usar service
using Mesa04.Services.Exceptions;//incluir para usar o IntegrityException no DeleteConfirmed
using Mesa04.Models.ViewModels;//incluir para usar o ErrorViewModel no metodo Error
using System.Diagnostics; // incluir para usar o Activity no metodo Error


namespace Mesa04.Controllers
{
    public class OperacaoStatussController : Controller
    {
        /*
        private readonly Mesa04Context _context;
        */

        private readonly OperacaoStatusService _operacaoStatusService;

        /*
        public OperacaoStatussController(Mesa04Context context)
        */
        public OperacaoStatussController(OperacaoStatusService operacaoStatusService)
        {
            /*
            _context = context;
            */
            _operacaoStatusService = operacaoStatusService;
        }

        // GET: OperacaoStatuss
        public async Task<IActionResult> Index()
        {
            /*
            return View(await _context.OperacaoStatus.ToListAsync());
            */
            var list = await _operacaoStatusService.FindAllAsync();
            return View(list);

        }

        // GET: OperacaoStatuss/Details/5
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
            var operacaoStatus = await _context.OperacaoStatus
                .FirstOrDefaultAsync(m => m.Id == id);
                */
            var operacaoStatus = await _operacaoStatusService.FindByIdAsync(id.Value);

            if (operacaoStatus == null)
            {
                /*
                return NotFound();
                */
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(operacaoStatus);
        }

        // GET: OperacaoStatuss/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OperacaoStatuss/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        /*
        public async Task<IActionResult> Create([Bind("Id,Nome")] OperacaoStatus operacaoStatus)
        */
        public async Task<IActionResult> Create([Bind("Id,Nome")] OperacaoStatus operacaoStatus)

        {
            if (ModelState.IsValid)
            {
                /*
                _context.Add(operacaoStatus);
                await _context.SaveChangesAsync();
                */
                await _operacaoStatusService.InsertAsync(operacaoStatus);
                return RedirectToAction(nameof(Index));
            }
            return View(operacaoStatus);
        }

        // GET: OperacaoStatuss/Edit/5
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
            var operacaoStatus = await _context.OperacaoStatus.FindAsync(id);
            */
            var operacaoStatus = await _operacaoStatusService.FindByIdAsync(id.Value); // o id.Value é porque lá encima o argumento está como opcional

            if (operacaoStatus == null)
            {
                /*
                return NotFound();
                */
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(operacaoStatus);
        }

        // POST: OperacaoStatuss/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        /*
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] OperacaoStatus operacaoStatus)
        */
        public async Task<IActionResult> Edit(int id, OperacaoStatus operacaoStatus)

        {
            if (id != operacaoStatus.Id)
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
                    _context.Update(operacaoStatus);
                    await _context.SaveChangesAsync();
                    */
                    await _operacaoStatusService.UpdateAsync(operacaoStatus);

                }
                /*
                catch (DbUpdateConcurrencyException)
                */
                catch (DbUpdateConcurrencyException e)
                {
                    /*
                    if (!OperacaoStatusExists(operacaoStatus.Id))
                    */
                    if (operacaoStatus == null)
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
            return View(operacaoStatus);
        }

        // GET: OperacaoStatuss/Delete/5
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
            var operacaoStatus = await _context.OperacaoStatus
                .FirstOrDefaultAsync(m => m.Id == id);
            */
            var operacaoStatus = await _operacaoStatusService.FindByIdAsync(id.Value); //preciso usar o "Value" pois como o argumento "id" é opcional, precisamos colocar o Value para pegar o valor caso seja informado

            if (operacaoStatus == null)
            {
                /*
                return NotFound();
                */
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(operacaoStatus);
        }

        // POST: OperacaoStatuss/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*
            var operacaoStatus = await _context.OperacaoStatus.FindAsync(id);
            _context.OperacaoStatus.Remove(operacaoStatus);
            await _context.SaveChangesAsync();
            */
            try
            {
                await _operacaoStatusService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

        }

        /*
        private bool OperacaoStatusExists(int id)
        {
            return _context.OperacaoStatus.Any(e => e.Id == id);
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
