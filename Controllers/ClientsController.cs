using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NBD.Data;
using NBD.Models;

namespace NBD.Controllers
{
    public class ClientsController : Controller
    {
        private readonly NBDContext _context;

        public ClientsController(NBDContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            var nBDContext = _context.Clients.Include(c => c.City);
            return View(await nBDContext.ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.City)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        [Authorize(Roles = "Admin,Manager")]
        // GET: Clients/Create
        public IActionResult Create()
        {
            
            ViewData["CityID"] = new SelectList(_context.Cities, "ID", "Name");
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]

        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,CompanyName,Position,PhoneNumber,Address,CityID,Province,PostalCode,Email")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityID"] = new SelectList(_context.Cities, "ID", "ID", client.CityID);
            return View(client);
        }

        [Authorize(Roles = "Admin,Manager")]
        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            ViewData["CityID"] = new SelectList(_context.Cities, "ID", "Name", client.CityID);
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,CompanyName,Position,PhoneNumber,Address,CityID,Province,PostalCode,Email")] Client client)
        {
            if (id != client.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ID))
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
            ViewData["CityID"] = new SelectList(_context.Cities, "ID", "Name", client.CityID);
            return View(client);
        }

        // GET: Clients/Delete/5
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.City)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void PopulateDropDownLists(Client client = null)
        {
            var cquery = from c in _context.Cities
                         orderby c.Name
                         select c;
            ViewData["CityID"] = new SelectList(cquery, "ID", "Name", client?.CityID);
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.ID == id);
        }
    }
}
