using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MockClassLibrary.Models;
using MockExamConsoleApp.Data;

namespace MockWebApp.Controllers
{
    public class PassengersController : Controller
    {
        private readonly FlightContext _context;

        public PassengersController(FlightContext context)
        {
            _context = context;
        }

        // GET: Passengers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Passengers
                .Include(p => p.Bookings)
                .ToListAsync());

        }

        // GET: Passengers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passengers = await _context.Passengers
                .Include(p => p.Bookings)
                .ThenInclude(p => p.Flight)
                .FirstOrDefaultAsync(m => m.PassengerID == id);

            if (passengers == null)
            {
                return NotFound();
            }

            return View(passengers);
        }

        // GET: Passengers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Passengers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PassengerID,Name,PassportNumber")] Passengers passengers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(passengers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(passengers);
        }

        // GET: Passengers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passengers = await _context.Passengers.FindAsync(id);
            if (passengers == null)
            {
                return NotFound();
            }
            return View(passengers);
        }

        // POST: Passengers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PassengerID,Name,PassportNumber")] Passengers passengers)
        {
            if (id != passengers.PassengerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(passengers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PassengersExists(passengers.PassengerID))
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
            return View(passengers);
        }

        // GET: Passengers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passengers = await _context.Passengers
                .FirstOrDefaultAsync(m => m.PassengerID == id);
            if (passengers == null)
            {
                return NotFound();
            }

            return View(passengers);
        }

        // POST: Passengers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var passengers = await _context.Passengers.FindAsync(id);
            if (passengers != null)
            {
                _context.Passengers.Remove(passengers);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PassengersExists(int id)
        {
            return _context.Passengers.Any(e => e.PassengerID == id);
        }
    }
}
