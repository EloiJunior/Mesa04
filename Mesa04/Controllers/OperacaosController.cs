﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mesa04.Models;
using Mesa04.Services;
using Mesa04.Models.ViewModels;
using Mesa04.Services.Exceptions;
using System.Diagnostics;
using System.Collections;

namespace Mesa04.Controllers
{
    public class OperacaosController : Controller
    {
        /*
        private readonly Mesa04Context _context;
        */
        private readonly OperacaoService _operacaoService;
        private readonly TipoOperacaoService _tipoOperacaoService;
        private readonly OperadorService _operadorService;
        private readonly ClienteService _clienteService;
        private readonly BancoMeService _bancoMeService;
        private readonly OperacaoStatusService _operacaoStatusService;
        private readonly MeService _meService;
        /*
        public OperacaosController(Mesa04Context context)
        {
            _context = context;
        }
        */
        public OperacaosController(OperacaoService operacaoService, TipoOperacaoService tipoOperacaoService, OperadorService operadorService, ClienteService clienteService, BancoMeService bancoMeService, OperacaoStatusService operacaoStatusService, MeService meService)
        {
            _operacaoService = operacaoService;
            _tipoOperacaoService = tipoOperacaoService;
            _operadorService = operadorService;
            _clienteService = clienteService;
            _bancoMeService = bancoMeService;
            _operacaoStatusService = operacaoStatusService;
            _meService = meService;
        }

        // GET: Operacaos
        public async Task<IActionResult> Index()
        {
            /*
            return View(await _context.Operacao.ToListAsync());
            */
            var list = await _operacaoService.FindAllAsync();
            return View(list);
        }

        //Metodo Simple Search
        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue) // if criado para colocar o primeiro dia do ano atual quando não for preenchido o campo do SimpleSearch
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }

            if (!maxDate.HasValue) // if criado para colocar a data atual quando não for preenchido o campo do SimpleSearch
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd"); //linha criada para passar o valor do minDate para o campo do SimpleSearch
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd"); //linha criada para passar o valor do maxDate para o campo do SimpleSearch

            var list = await _operacaoService.FindByDateAsync(minDate, maxDate);
            return View(list);
        }


        //Metodo Grouping Serch
        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue) // if criado para colocar o primeiro dia do ano atual quando não for preenchido o campo do SimpleSearch
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }

            if (!maxDate.HasValue) // if criado para colocar a data atual quando não for preenchido o campo do SimpleSearch
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd"); //linha criada para passar o valor do minDate para o campo do SimpleSearch
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd"); //linha criada para passar o valor do maxDate para o campo do SimpleSearch

            var list = await _operacaoService.FindByDateGroupingAsync(minDate, maxDate);
            return View(list);
        }

        // GET: Operacaos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var operacao = await _operacaoService.FindByIdAsync(id.Value);

            if (operacao == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(operacao);
        }

        // GET: Operacaos/Create
        public async Task<IActionResult> Create()
        {
            var tipoOperacaos = await _tipoOperacaoService.FindAllAsync();                //codigo para chamar uma lista de departamentos do DepartamentoService, e guardar essa lista na variavel departamentos
            var operadores = await _operadorService.FindAllAsync();                //codigo para chamar uma lista de departamentos do DepartamentoService, e guardar essa lista na variavel departamentos
            var clientes = await _clienteService.FindAllAsync();                //codigo para chamar uma lista de departamentos do DepartamentoService, e guardar essa lista na variavel departamentos
            var bancoMes = await _bancoMeService.FindAllAsync();                //codigo para chamar uma lista de departamentos do DepartamentoService, e guardar essa lista na variavel departamentos
            var operacaoStatuss = await _operacaoStatusService.FindAllAsync();                //codigo para chamar uma lista de departamentos do DepartamentoService, e guardar essa lista na variavel departamentos
            var mes = await _meService.FindAllAsync();                //codigo para chamar uma lista de departamentos do DepartamentoService, e guardar essa lista na variavel departamentos
            var viewModel = new OperacaoFormViewModel { TipoOperacaos = tipoOperacaos, Operadores = operadores, Clientes = clientes, BancoMes = bancoMes, OperacaoStatuss = operacaoStatuss, Mes = mes };  //codigo para instanciar um novo OperadorFormViewModel já começando com a lista de departamentos acima, e chamando esse formulario de viewModel
            return View(viewModel);                                                       //codigo que manda esse novo formulario já com a lista de departamentos criada para a View
        }

        // POST: Operacaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("Id,TipoOperacaoId,OperadorId,Data,ClienteId,Valor,Taxa,Despesa,FluxoMn,FluxoMe,BancoMeId,OperacaoStatusId")]*/ Operacao operacao)
        {
            //if (ModelState.IsValid)
            //{
                await _operacaoService.InsertAsync(operacao);
                return RedirectToAction(nameof(Index));
            //}
            //return View(operacao);
        }

        // GET: Operacaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var operacao = await _operacaoService.FindByIdAsync(id.Value);

            if (operacao == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<TipoOperacao> tipoOperacaos= await _tipoOperacaoService.FindAllAsync();                //codigo para chamar uma lista de departamentos do DepartamentoService, e guardar essa lista na variavel departamentos
            List<Operador> operadores = await _operadorService.FindAllAsync();                //codigo para chamar uma lista de departamentos do DepartamentoService, e guardar essa lista na variavel departamentos
            List<Cliente> clientes = await _clienteService.FindAllAsync();                //codigo para chamar uma lista de departamentos do DepartamentoService, e guardar essa lista na variavel departamentos
            List<BancoMe> bancoMes = await _bancoMeService.FindAllAsync();                //codigo para chamar uma lista de departamentos do DepartamentoService, e guardar essa lista na variavel departamentos
            List<OperacaoStatus> operacaoStatuss = await _operacaoStatusService.FindAllAsync();                //codigo para chamar uma lista de departamentos do DepartamentoService, e guardar essa lista na variavel departamentos
            List<Me> mes = await _meService.FindAllAsync();                //codigo para chamar uma lista de departamentos do DepartamentoService, e guardar essa lista na variavel departamentos
            OperacaoFormViewModel viewModel = new OperacaoFormViewModel { Operacao = operacao, TipoOperacaos = tipoOperacaos, Operadores = operadores, Clientes = clientes, BancoMes = bancoMes, OperacaoStatuss = operacaoStatuss, Mes = mes };  //codigo para instanciar um novo OperadorFormViewModel já começando com a lista de departamentos acima, e chamando esse formulario de viewModel
            return View(viewModel);                                                       //codigo que manda esse novo formulario já com a lista de departamentos criada para a View



        }

        // POST: Operacaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, /*[Bind("Id,Data,Valor,Taxa,Despesa,FluxoMn,FluxoMe,BancoMe,OperacaoStatus")]*/ Operacao operacao)
        {
            if (id != operacao.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _operacaoService.UpdateAsync(operacao);
                }
                catch (DbUpdateConcurrencyException e)
                {
                    if(operacao == null)
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
            return View(operacao);
        }

        // GET: Operacaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var operacao = await _operacaoService.FindByIdAsync(id.Value); //preciso usar o "Value" pois como o argumento "id" é opcional, precisamos colocar o Value para pegar o valor caso seja informado

            if (operacao == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(operacao);
        }

        // POST: Operacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _operacaoService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });

            }
        }


        //Metodo: Error
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
