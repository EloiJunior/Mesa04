using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mesa04.Models;
using Mesa04.Services;
using Mesa04.Services.Exceptions;
using Mesa04.Models.ViewModels;
using System.Diagnostics;

namespace Mesa04.Controllers
{
    public class TipoOperacaosController : Controller
    {
        private readonly TipoOperacaoService _tipoOperacaoService;

        public TipoOperacaosController(TipoOperacaoService tipoOperacaoService)
        {
            _tipoOperacaoService = tipoOperacaoService;
        }

        // GET: TipoOperacaos
        public async Task<IActionResult> Index()
        {
            var list = await _tipoOperacaoService.FindAllAsync();
            return View(list);
        }

        // GET: TipoOperacaos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoOperacao = await _tipoOperacaoService.FindByIdAsync(id.Value);

            if (tipoOperacao == null)
            {
                return NotFound();
            }

            return View(tipoOperacao);
        }

        // GET: TipoOperacaos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoOperacaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] TipoOperacao tipoOperacao)
        {
            if (ModelState.IsValid)
            {
                await _tipoOperacaoService.InsertAsync(tipoOperacao);
                return RedirectToAction(nameof(Index));
            }
            return View(tipoOperacao);
        }

        // GET: TipoOperacaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoOperacao = await _tipoOperacaoService.FindByIdAsync(id);
            if (tipoOperacao == null)
            {
                return NotFound();
            }
            return View(tipoOperacao);
        }

        // POST: TipoOperacaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] TipoOperacao tipoOperacao)
        {
            if (id != tipoOperacao.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _tipoOperacaoService.UpdateAsync(tipoOperacao);
                }
                catch (DbUpdateConcurrencyException e)
                {
                    //if (!TipoOperacaoExists(tipoOperacao.Id))
                    if (tipoOperacao == null)
                    {
                        return RedirectToAction(nameof(Error), new { message = "Id not found" });
                    }
                    else
                    {
                        return RedirectToAction(nameof(Error), new { message = e.Message });
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tipoOperacao);
        }

        // GET: TipoOperacaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            var tipoOperacao = await _tipoOperacaoService.FindByIdAsync(id.Value);

            if (tipoOperacao == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(tipoOperacao);
        }

        // POST: TipoOperacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _tipoOperacaoService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        /*
        private bool TipoOperacaoExists(int id)
        {
            return _context.TipoOperacao.Any(e => e.Id == id);
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
