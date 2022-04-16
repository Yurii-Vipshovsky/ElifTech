using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankWEB.Data;
using BankWEB.Models;
using System.Security.Claims;

namespace BankWEB.Controllers
{
    public class BankModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BankModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BankModels
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _context.BankModel.Where(d => d.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value).ToListAsync());
            }
            else
            {
                return View();
            }
        }

        // GET: BankModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankModel = await _context.BankModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bankModel == null)
            {
                return NotFound();
            }

            return View(bankModel);
        }

        // GET: BankModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BankModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,InterestRate,MaximumLoan,MinimumDownPayment,LoanTerm")] BankModel bankModel)
        {
            bankModel.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (ModelState.IsValid)
            {
                _context.Add(bankModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bankModel);
        }

        // GET: BankModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankModel = await _context.BankModel.FindAsync(id);
            if (bankModel == null)
            {
                return NotFound();
            }
            return View(bankModel);
        }

        // POST: BankModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,InterestRate,MaximumLoan,MinimumDownPayment,LoanTerm")] BankModel bankModel)
        {
            
            if (id != bankModel.Id)
            {
                return NotFound();
            }
            bankModel.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bankModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankModelExists(bankModel.Id))
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
            return View(bankModel);
        }

        // GET: BankModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankModel = await _context.BankModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bankModel == null)
            {
                return NotFound();
            }

            return View(bankModel);
        }

        // POST: BankModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bankModel = await _context.BankModel.FindAsync(id);
            _context.BankModel.Remove(bankModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Calculate()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Calculate([Bind("InitialLoan,NumberOfPayments,BankId")] Mortgage mortgage)
        {
            BankModel bank = _context.BankModel.Find(mortgage.BankId);
            double M = (mortgage.InitialLoan * (bank.InterestRate / 12) * Math.Pow(1 + bank.InterestRate / 12, mortgage.NumberOfPayments)) / (Math.Pow(1 + bank.InterestRate / 12, mortgage.NumberOfPayments) - 1);
            return Content(M.ToString());
        }
        private bool BankModelExists(int id)
        {
            return _context.BankModel.Any(e => e.Id == id);
        }
    }
}
