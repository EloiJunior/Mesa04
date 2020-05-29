using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mesa04.Models;
using Mesa04.Services;

namespace Mesa04.Controllers
{
    public class MesController : Controller
    {
        /*
        private readonly Mesa04Context _context;
        */
        private readonly MeService _meService;

        public MesController(MeService meService)
        {
            _meService = meService;
        }

        // GET: Mes
        public async Task<IActionResult> Index()
        {
            /*
            return View(await _context.Me.ToListAsync());
            */
            var list = await _meService.FindAllAsync();
            return View(list);
        }

        // GET: Mes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            /*
            var me = await _context.Me
                .FirstOrDefaultAsync(m => m.Id == id);
                */
            var me = await _meService.FindByIdAsync(id.Value);

            return View(me);
        }

        // GET: Mes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Codigo,Abreviacao")] Me me)
        {
            if (ModelState.IsValid)
            {
                /*
                _context.Add(me);
                await _context.SaveChangesAsync();
                */
                await _meService.InsertAsync(me);
                return RedirectToAction(nameof(Index));
            }
            return View(me);
        }

        // GET: Mes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            /*
            var me = await _context.Me.FindAsync(id);
            */
            var me = await _meService.FindByIdAsync(id.Value);

            return View(me);
        }

        // POST: Mes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Codigo,Abreviacao")] Me me)
        {
            if (id != me.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    /*
                    _context.Update(me);
                    await _context.SaveChangesAsync();
                    */
                    await _meService.UpdateAsync(me);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _meService.MeExists(me.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(me);
        }

        // GET: Mes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            /*
            var me = await _context.Me
                .FirstOrDefaultAsync(m => m.Id == id);
                */
            var me = await _meService.FindByIdAsync(id.Value);

            if (me == null)
            {
                return NotFound();
            }

            return View(me);
        }

        // POST: Mes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*
            var me = await _context.Me.FindAsync(id);
            _context.Me.Remove(me);
            await _context.SaveChangesAsync();
            */
            await _meService.RemoveAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> Exists(int id)
        {
            return await _meService.MeExists(id);
        }
    }
}
