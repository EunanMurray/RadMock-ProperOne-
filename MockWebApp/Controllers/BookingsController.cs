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
    public class BookingsController : Controller
    {
        private readonly FlightContext _context;

        public BookingsController(FlightContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var flightContext = _context.Bookings.Include(b => b.Flight).Include(b => b.Passenger);
            return View(await flightContext.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings
                .Include(b => b.Flight)
                .Include(b => b.Passenger)
                .FirstOrDefaultAsync(m => m.PassengerID == id);
            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }

        // GET: Bookings/Create
        public async Task<IActionResult> Create()
        {
            // Get list of flights and passengers for dropdowns
            ViewData["FlightID"] = new SelectList(_context.Flights, "FlightID", "FlightNumber");
            ViewData["PassengerID"] = new SelectList(_context.Passengers, "PassengerID", "Name");
            return View();
        }

        // POST: Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PassengerID,FlightID,TicketType,TicketCost,BaggageCharge")] Bookings booking)
        {
            if (ModelState.IsValid)
            {
                // Check if booking already exists
                var existingBooking = await _context.Bookings
                    .FirstOrDefaultAsync(b => b.PassengerID == booking.PassengerID && b.FlightID == booking.FlightID);

                if (existingBooking != null)
                {
                    ModelState.AddModelError("", "This passenger is already booked on this flight.");
                    ViewData["FlightID"] = new SelectList(_context.Flights, "FlightID", "FlightNumber");
                    ViewData["PassengerID"] = new SelectList(_context.Passengers, "PassengerID", "Name");
                    return View(booking);
                }

                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["FlightID"] = new SelectList(_context.Flights, "FlightID", "FlightNumber");
            ViewData["PassengerID"] = new SelectList(_context.Passengers, "PassengerID", "Name");
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings.FindAsync(id);
            if (bookings == null)
            {
                return NotFound();
            }
            ViewData["FlightID"] = new SelectList(_context.Flights, "FlightID", "Country", bookings.FlightID);
            ViewData["PassengerID"] = new SelectList(_context.Passengers, "PassengerID", "Name", bookings.PassengerID);
            return View(bookings);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TicketType,TicketCost,BaggageCharge,FlightID,PassengerID")] Bookings bookings)
        {
            if (id != bookings.PassengerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingsExists(bookings.PassengerID))
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
            ViewData["FlightID"] = new SelectList(_context.Flights, "FlightID", "Country", bookings.FlightID);
            ViewData["PassengerID"] = new SelectList(_context.Passengers, "PassengerID", "Name", bookings.PassengerID);
            return View(bookings);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings
                .Include(b => b.Flight)
                .Include(b => b.Passenger)
                .FirstOrDefaultAsync(m => m.PassengerID == id);
            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookings = await _context.Bookings.FindAsync(id);
            if (bookings != null)
            {
                _context.Bookings.Remove(bookings);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingsExists(int id)
        {
            return _context.Bookings.Any(e => e.PassengerID == id);
        }
    }
}
