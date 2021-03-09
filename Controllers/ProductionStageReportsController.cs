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
    [Authorize(Roles = "Admin")]
    public class ProductionStageReportsController : Controller
    {
        private readonly NBDContext _context;

        public ProductionStageReportsController(NBDContext context)
        {
            _context = context;
        }

        // GET: ProductionStageReports
        public async Task<IActionResult> Index()
        {
            var nBDContext = _context.ProductionStageReports.Include(p => p.Project);
            return View(await nBDContext.ToListAsync());
        }

        // GET: ProductionStageReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productionStageReport = await _context.ProductionStageReports
                .Include(p => p.ProductionPlan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productionStageReport == null)
            {
                return NotFound();
            }

            return View(productionStageReport);
        }

        // GET: ProductionStageReports/Create
        public IActionResult Create()
        {
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "Name");
            return View();
        }

        // POST: ProductionStageReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Bid,EstProdPlan,TotalCosttoDate,ActualMtl,EstimatedDesingCost,ActuLaborPro,EstLaborProdCost,ActuLaborDesingCost,EstLaborDesingCost,ProjectID")] ProductionStageReport productionStageReport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productionStageReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "Name", productionStageReport.ProjectID);
            return View(productionStageReport);
        }

        // GET: ProductionStageReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productionStageReport = await _context.ProductionStageReports.FindAsync(id);
            if (productionStageReport == null)
            {
                return NotFound();
            }
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "Name", productionStageReport.ProjectID);
            return View(productionStageReport);
        }

        // POST: ProductionStageReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Bid,EstProdPlan,TotalCosttoDate,ActualMtl,EstimatedDesingCost,ActuLaborPro,EstLaborProdCost,ActuLaborDesingCost,EstLaborDesingCost,ProjectID")] ProductionStageReport productionStageReport)
        {
            if (id != productionStageReport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productionStageReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductionStageReportExists(productionStageReport.Id))
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
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "Name", productionStageReport.ProjectID);
            return View(productionStageReport);
        }

        // GET: ProductionStageReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productionStageReport = await _context.ProductionStageReports
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productionStageReport == null)
            {
                return NotFound();
            }

            return View(productionStageReport);
        }

        // POST: ProductionStageReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productionStageReport = await _context.ProductionStageReports.FindAsync(id);
            _context.ProductionStageReports.Remove(productionStageReport);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductionStageReportExists(int id)
        {
            return _context.ProductionStageReports.Any(e => e.Id == id);
        }
    }
}
