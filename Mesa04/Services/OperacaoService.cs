﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mesa04.Models;
using Mesa04.Services.Exceptions;



namespace Mesa04.Services
{
    public class OperacaoService
    {
        private readonly Mesa04Context _context;

        public OperacaoService(Mesa04Context context)
        {
            _context = context;
        }

        // GET: Operacaos ////.Include(x => x.Cliente)
        public async Task<List<Operacao>> FindAllAsync()
        {
            return await _context.Operacao.Include(x => x.BancoMe).Include(x => x.Cliente).Include(x => x.Me).Include(x => x.OperacaoStatus).ToListAsync();
        }

        // GET: Operacaos/Details/5
        public async Task<Operacao> FindByIdAsync(int? id)
        {
            return await _context.Operacao.Include(m => m.BancoMe).Include(m => m.OperacaoStatus).Include(m => m.Me).FirstOrDefaultAsync(m => m.Id == id);
        }

        //GET: Operacao/SimpleSearch
        public async Task<List<Operacao>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.Operacao select obj; //foi criada uma variavel, pois vamos transformar um resultado DBset que não podemos manipular para um resultado Iqueryble que é a variavel 

            if (minDate.HasValue)
            {
                result = result.Where(x => x.Data >= minDate.Value);//result recebe x tal que x.Data é maior ou igual o minDate informado
            }

            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Data <= maxDate.Value);//result recebe x tal que x.Data é menor ou igual o maxDate informado
            }

            return await result.Include(x => x.Operador).Include(x => x.Operador.Departamento).OrderByDescending(x => x.Data).ToListAsync();
        }

        //GET: Operacao/GroupingSearch
        public async Task<List<IGrouping<Departamento,Operacao>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.Operacao select obj; //foi criada uma variavel, pois vamos transformar um resultado DBset que não podemos manipular para um resultado Iqueryble que é a variavel 

            if (minDate.HasValue)
            {
                result = result.Where(x => x.Data >= minDate.Value);//result recebe x tal que x.Data é maior ou igual o minDate informado
            }

            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Data <= maxDate.Value);//result recebe x tal que x.Data é menor ou igual o maxDate informado
            }

            return await result
                .Include(x => x.Operador)
                .Include(x => x.Operador.Departamento)
                .OrderByDescending(x => x.Data)
                .GroupBy(x => x.Operador.Departamento)
                .ToListAsync();
        }

        /*
        // GET: Operacaos/Create
        public IActionResult Create()
        {
            return View();
        }
        */

        // POST: Operacaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task InsertAsync(/*[Bind("Id,Data,Valor,Taxa,Despesa,FluxoMn,FluxoMe,BancoMe,OperacaoStatus")]*/ Operacao operacao)
        {
                _context.Add(operacao);
                await _context.SaveChangesAsync();
        }


        // POST: Operacaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task UpdateAsync(Operacao operacao)
        {
            if (!_context.Operacao.Any(x => x.Id == operacao.Id))
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                    _context.Update(operacao);
                    await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!OperacaoExists(operacao.Id))
                {
                        throw new NotFoundException("Id not found");
                }
                else
                {
                        throw new DbConcurrencyException(e.Message);
                }
            }
            
        }


        // POST: Operacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task RemoveAsync(int id)
        {
            try
            {
                var operacao = await _context.Operacao.FindAsync(id);
                _context.Operacao.Remove(operacao);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        private bool OperacaoExists(int id)
        {
            return _context.Operacao.Any(e => e.Id == id);
        }

    }
}
