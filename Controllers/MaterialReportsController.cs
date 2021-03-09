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
    [Authorize(Roles = "Admin,Manager,Designer,SalesPerson")]
    public class MaterialReportsController : Controller
    {
        private readonly NBDContext _context;

        public MaterialReportsController(NBDContext context)
        {
            _context = context;
        }

        // GET: MaterialReports
        public async Task<IActionResult> Index()
        {
            var nBDContext = _context.MaterialReports
                .OrderBy(m=>m.ProjectID)
                .Include(m => m.Employee)
                .Include(m => m.Material)
                .Include(m => m.Project);
            return View(await nBDContext.ToListAsync());
        }

        // GET: MaterialReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materialReport = await _context.MaterialReports
                .Include(m => m.Employee)
                .Include(m => m.Material)
                .Include(m => m.Project)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (materialReport == null)
            {
                return NotFound();
            }

            return View(materialReport);
        }

        // GET: MaterialReports/Create
        public IActionResult Create()
        {
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "FullName");
            ViewData["MaterialID"] = new SelectList(_context.Materials, "ID", "Description");
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "Name");
            return View();
        }

        // POST: MaterialReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Quantity,Costs,Date,EmployeeID,ProjectID,MaterialID")] MaterialReport materialReport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(materialReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "FullName", materialReport.EmployeeID);
            ViewData["MaterialID"] = new SelectList(_context.Materials, "ID", "Description", materialReport.MaterialID);
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "Name", materialReport.ProjectID);
            return View(materialReport);
        }
        [Authorize(Roles = "Admin,Manager,Designer")]
        // GET: MaterialReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materialReport = await _context.MaterialReports.FindAsync(id);
            if (materialReport == null)
            {
                return NotFound();
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "FullName", materialReport.EmployeeID);
            ViewData["MaterialID"] = new SelectList(_context.Materials, "ID", "Description", materialReport.MaterialID);
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "Name", materialReport.ProjectID);
            return View(materialReport);
        }
        [Authorize(Roles = "Admin,Manager,Designer")]
        // POST: MaterialReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Quantity,Costs,Date,EmployeeID,ProjectID,MaterialID")] MaterialReport materialReport)
        {
            if (id != materialReport.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materialReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterialReportExists(materialReport.ID))
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
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "Email", materialReport.EmployeeID);
            ViewData["MaterialID"] = new SelectList(_context.Materials, "ID", "Description", materialReport.MaterialID);
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "Name", materialReport.ProjectID);
            return View(materialReport);
        }
        [Authorize(Roles = "Admin,Manager")]
        // GET: MaterialReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materialReport = await _context.MaterialReports
                .Include(m => m.Employee)
                .Include(m => m.Material)
                .Include(m => m.Project)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (materialReport == null)
            {
                return NotFound();
            }

            return View(materialReport);
        }
        [Authorize(Roles = "Admin,Manager")]
        // POST: MaterialReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var materialReport = await _context.MaterialReports.FindAsync(id);
            _context.MaterialReports.Remove(materialReport);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaterialReportExists(int id)
        {
            return _context.MaterialReports.Any(e => e.ID == id);
        }
    }
}
