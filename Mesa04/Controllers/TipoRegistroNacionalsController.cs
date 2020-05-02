using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mesa04.Models;
using Mesa04.Services;//inclui para usar service
using Mesa04.Services.Exceptions;//inclui para usar o IntegrityException no DeleteConfirmed
using Mesa04.Models.ViewModels;//inclui para usar o ErrorViewModel no metodo Error
using System.Diagnostics; // inclui para usar o Activity no metodo Error

namespace Mesa04.Controllers
{
    public class TipoRegistroNacionalsController : Controller
    {
        /*
        private readonly Mesa04Context _context;
        */

        private readonly TipoRegistroNacionalService _tipoRegistroNacionalService;

        public TipoRegistroNacionalsController(TipoRegistroNacionalService tipoRegistroNacionalService)
        {
            _tipoRegistroNacionalService = tipoRegistroNacionalService;
        }

        // GET: TipoRegistroNacionals
        public async Task<IActionResult> Index()
        {
            /*
            return View(await _context.TipoRegistroNacional.ToListAsync());
            */

            var list = await _tipoRegistroNacionalService.FindAllAsync();

            return View(list);


        }

        // GET: TipoRegistroNacionals/Details/5
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
            var tipoRegistroNacional = await _context.TipoRegistroNacional
                .FirstOrDefaultAsync(m => m.Id == id);
            */

            var tipoRegistroNacional = await _tipoRegistroNacionalService.FindByIdAsync(id.Value);

            if (tipoRegistroNacional == null)
            {
                /*
                return NotFound();
                */
                return RedirectToAction(nameof(Error), new { message = "Id not found" });

            }

            return View(tipoRegistroNacional);
        }


        // GET: TipoRegistroNacionals/Create
        public IActionResult Create()
        
        {
            return View();
        }


        // POST: TipoRegistroNacionals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        /*
        public async Task<IActionResult> Create([Bind("Id,Nome")] TipoRegistroNacional tipoRegistroNacional)
        */
        public async Task<IActionResult> Create([Bind("Id,Nome")] TipoRegistroNacional tipoRegistroNacional)
        {
            if (ModelState.IsValid)
            {
                /*
                _context.Add(tipoRegistroNacional);
                await _context.SaveChangesAsync();
                */
                await _tipoRegistroNacionalService.InsertAsync(tipoRegistroNacional);
                return RedirectToAction(nameof(Index));
            }
            return View(tipoRegistroNacional);
        }

        // GET: TipoRegistroNacionals/Edit/5
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
            var tipoRegistroNacional = await _context.TipoRegistroNacional.FindAsync(id);
            */
            var tipoRegistroNacional = await _tipoRegistroNacionalService.FindByIdAsync(id.Value); // o id.Value é porque lá encima o argumento está como opcional

            if (tipoRegistroNacional == null)
            {
                /*
                return NotFound();
                */
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(tipoRegistroNacional);
        }

        // POST: TipoRegistroNacionals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        /*
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] TipoRegistroNacional tipoRegistroNacional)
        */
        public async Task<IActionResult> Edit(int id,TipoRegistroNacional tipoRegistroNacional)
        {
            if (id != tipoRegistroNacional.Id)
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
                    _context.Update(tipoRegistroNacional);
                    await _context.SaveChangesAsync();
                    */
                    await _tipoRegistroNacionalService.UpdateAsync(tipoRegistroNacional);
                }
                /*
                catch (DbUpdateConcurrencyException)
                */
                catch (DbUpdateConcurrencyException e)
                {
                    /*
                    if (!TipoRegistroNacionalExists(tipoRegistroNacional.Id))
                    */
                    if (tipoRegistroNacional == null)

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
            return View(tipoRegistroNacional);
        }


        // GET: TipoRegistroNacionals/Delete/5
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
            var tipoRegistroNacional = await _context.TipoRegistroNacional
                .FirstOrDefaultAsync(m => m.Id == id);
            */
            var tipoRegistroNacional = await _tipoRegistroNacionalService.FindByIdAsync(id.Value); //preciso usar o "Value" pois como o argumento "id" é opcional, precisamos colocar o Value para pegar o valor caso seja informado

            if (tipoRegistroNacional == null)
            {
                /*
                return NotFound();
                */
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(tipoRegistroNacional);
        }

        // POST: TipoRegistroNacionals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*
            var tipoRegistroNacional = await _context.TipoRegistroNacional.FindAsync(id);
            _context.TipoRegistroNacional.Remove(tipoRegistroNacional);
            await _context.SaveChangesAsync();
            */
            try
            {
                await _tipoRegistroNacionalService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            
        }

        /*
        private bool TipoRegistroNacionalExists(int id)
        {
            return _context.TipoRegistroNacional.Any(e => e.Id == id);
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
