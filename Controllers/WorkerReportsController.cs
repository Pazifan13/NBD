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
    public class WorkerReportsController : Controller
    {
        private readonly NBDContext _context;

        public WorkerReportsController(NBDContext context)
        {
            _context = context;
        }

        // GET: WorkerReports
        public async Task<IActionResult> Index()
        {
            var nBDContext = _context.WorkerReports.Include(w => w.Employee).Include(w => w.Project).Include(w => w.Task);
            return View(await nBDContext.ToListAsync());
        }

        // GET: WorkerReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workerReport = await _context.WorkerReports
                .Include(w => w.Employee)
                .Include(w => w.Project)
                .Include(w => w.Task)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (workerReport == null)
            {
                return NotFound();
            }

            return View(workerReport);
        }

        // GET: WorkerReports/Create
        public IActionResult Create()
        {
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "FullName");
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "Name");
            ViewData["TaskID"] = new SelectList(_context.Tasks, "ID", "Description");
            return View();
        }

        // POST: WorkerReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Hours,Costs,Date,EmployeeID,ProjectID,TaskID")] WorkerReport workerReport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workerReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "FullName", workerReport.EmployeeID);
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "Name", workerReport.ProjectID);
            ViewData["TaskID"] = new SelectList(_context.Tasks, "ID", "Description", workerReport.TaskID);
            return View(workerReport);
        }

        // GET: WorkerReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workerReport = await _context.WorkerReports.FindAsync(id);
            if (workerReport == null)
            {
                return NotFound();
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "FullName", workerReport.EmployeeID);
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "Name", workerReport.ProjectID);
            ViewData["TaskID"] = new SelectList(_context.Tasks, "ID", "Description", workerReport.TaskID);
            return View(workerReport);
        }

        // POST: WorkerReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Hours,Costs,Date,EmployeeID,ProjectID,TaskID")] WorkerReport workerReport)
        {
            if (id != workerReport.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workerReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkerReportExists(workerReport.ID))
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
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "FullName", workerReport.EmployeeID);
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "Name", workerReport.ProjectID);
            ViewData["TaskID"] = new SelectList(_context.Tasks, "ID", "Description", workerReport.TaskID);
            return View(workerReport);
        }
        [Authorize(Roles = "Admin,Manager")]
        // GET: WorkerReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workerReport = await _context.WorkerReports
                .Include(w => w.Employee)
                .Include(w => w.Project)
                .Include(w => w.Task)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (workerReport == null)
            {
                return NotFound();
            }

            return View(workerReport);
        }
        [Authorize(Roles = "Admin,Manager")]
        // POST: WorkerReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workerReport = await _context.WorkerReports.FindAsync(id);
            _context.WorkerReports.Remove(workerReport);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkerReportExists(int id)
        {
            return _context.WorkerReports.Any(e => e.ID == id);
        }
    }
}
