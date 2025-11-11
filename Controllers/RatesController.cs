using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Skynetz.Data;
using Project_Skynetz.Models;

namespace Project_Skynetz.Controllers
{
    public class RatesController : Controller
    {
        private readonly SkynetzContext _context;

        public RatesController(SkynetzContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Rates.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var rate = await _context.Rates.FirstOrDefaultAsync(m => m.Id == id);
            if (rate == null) return NotFound();
            return View(rate);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, OriginCode, DestinationCode, PricePerMinute")] Rate rate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rate);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var rate = await _context.Rates.FindAsync(id);
            if (rate == null) return NotFound();
            return View(rate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, OriginCode, DestinationCode, PricePerMinute")] Rate rate)
        {
            if (id != rate.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RateExists(rate.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(rate);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var rate = await _context.Rates.FirstOrDefaultAsync(m => m.Id == id);
            if (rate == null) return NotFound();
            return View(rate);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rate = await _context.Rates.FindAsync(id);
            if (rate != null)
            {
                _context.Rates.Remove(rate);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RateExists(int id)
        {
            return _context.Rates.Any(e => e.Id == id);
        }
    }
}
