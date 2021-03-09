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
    [Authorize (Roles ="Admin")]
    public class BidStageReportsController : Controller
    {
        private readonly NBDContext _context;

        public BidStageReportsController(NBDContext context)
        {
            _context = context;
        }

        // GET: BidStageReports
        public async Task<IActionResult> Index()
        {
            var nBDContext = _context.BidStageReports.Include(b => b.Project);
            return View(await nBDContext.ToListAsync());
        }

        // GET: BidStageReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bidStageReport = await _context.BidStageReports
                .Include(b => b.Project)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bidStageReport == null)
            {
                return NotFound();
            }

            return View(bidStageReport);
        }

        // GET: BidStageReports/Create
        public IActionResult Create()
        {
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "Name");
            return View();
        }

        // POST: BidStageReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,EstimatedBid,ActualDesingHours,EstimatedDesingHours,ActualDesingCost,EstimatedDesingCost,Hours,Remaining,ProjectID")] BidStageReport bidStageReport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bidStageReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "Name", bidStageReport.ProjectID);
            return View(bidStageReport);
        }

        // GET: BidStageReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bidStageReport = await _context.BidStageReports.FindAsync(id);
            if (bidStageReport == null)
            {
                return NotFound();
            }
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "Name", bidStageReport.ProjectID);
            return View(bidStageReport);
        }

        // POST: BidStageReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,EstimatedBid,ActualDesingHours,EstimatedDesingHours,ActualDesingCost,EstimatedDesingCost,Hours,Remaining,ProjectID")] BidStageReport bidStageReport)
        {
            if (id != bidStageReport.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bidStageReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BidStageReportExists(bidStageReport.ID))
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
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "Name", bidStageReport.ProjectID);
            return View(bidStageReport);
        }

        // GET: BidStageReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bidStageReport = await _context.BidStageReports
                .Include(b => b.Project)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bidStageReport == null)
            {
                return NotFound();
            }

            return View(bidStageReport);
        }

        // POST: BidStageReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bidStageReport = await _context.BidStageReports.FindAsync(id);
            _context.BidStageReports.Remove(bidStageReport);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BidStageReportExists(int id)
        {
            return _context.BidStageReports.Any(e => e.ID == id);
        }
    }
}
