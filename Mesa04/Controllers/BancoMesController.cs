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
    public class BancoMesController : Controller
    {
        /*
        private readonly Mesa04Context _context;
        */
        private readonly BancoMeService _bancoMeService;

        public BancoMesController(BancoMeService bancoMeService)
        {
            _bancoMeService = bancoMeService;
        }

        // GET: BancoMes
        public async Task<IActionResult> Index()
        {
            /*
            return View(await _context.BancoMe.ToListAsync());
            */
            var list = await _bancoMeService.FindAllAsync();
            return View(list);
        }

        // GET: BancoMes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            /*
            var bancoMe = await _context.BancoMe
                .FirstOrDefaultAsync(m => m.Id == id);
                */
            var bancoMe = await _bancoMeService.FindByIdAsync(id.Value);

            return View(bancoMe);
        }

        // GET: BancoMes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BancoMes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] BancoMe bancoMe)
        {
            if (ModelState.IsValid)
            {
                /*
                _context.Add(bancoMe);
                await _context.SaveChangesAsync();
                */
                await _bancoMeService.InsertAsync(bancoMe);
                return RedirectToAction(nameof(Index));
            }
            return View(bancoMe);
        }

        // GET: BancoMes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            /*
            var bancoMe = await _context.BancoMe.FindAsync(id);
            */
            var bancoMe = await _bancoMeService.FindByIdAsync(id.Value);

            return View(bancoMe);
        }

        // POST: BancoMes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] BancoMe bancoMe)
        {
            if (id != bancoMe.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    /*
                    _context.Update(bancoMe);
                    await _context.SaveChangesAsync();
                    */
                    await _bancoMeService.UpdateAsync(bancoMe);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _bancoMeService.BancoMeExists(bancoMe.Id))
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
            return View(bancoMe);
        }

        // GET: BancoMes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            /*
            var bancoMe = await _context.BancoMe
                .FirstOrDefaultAsync(m => m.Id == id);
                */
            var bancoMe = await _bancoMeService.FindByIdAsync(id.Value);

            if (bancoMe == null)
            {
                return NotFound();
            }

            return View(bancoMe);
        }

        // POST: BancoMes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*
            var bancoMe = await _context.BancoMe.FindAsync(id);
            _context.BancoMe.Remove(bancoMe);
            await _context.SaveChangesAsync();
            */
            await _bancoMeService.RemoveAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> Exists(int id)
        {
            return await _bancoMeService.BancoMeExists(id);
        }
    }
}
