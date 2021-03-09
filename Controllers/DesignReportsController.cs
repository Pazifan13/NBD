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
    [Authorize(Roles = "Admin,Manager,Designer")]
    public class DesignReportsController : Controller
    {
        private readonly NBDContext _context;

        public DesignReportsController(NBDContext context)
        {
            _context = context;
        }

        // GET: DesignReports
        public async Task<IActionResult> Index()
        {
            var nBDContext = _context.DesignReports.Include(d => d.Employee).Include(d => d.Project).Include(d => d.Stage).Include(d => d.Task);
            return View(await nBDContext.ToListAsync());
        }

        // GET: DesignReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designReport = await _context.DesignReports
                .Include(d => d.Employee)
                .Include(d => d.Project)
                .Include(d => d.Stage)
                .Include(d => d.Task)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (designReport == null)
            {
                return NotFound();
            }

            return View(designReport);
        }

        // GET: DesignReports/Create
        public IActionResult Create()
        {
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "FullName");
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "Name");
            ViewData["StageID"] = new SelectList(_context.Stages, "ID", "Name");
            ViewData["TaskID"] = new SelectList(_context.Tasks, "ID", "Description");
            return View();
        }

        // POST: DesignReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Hour,Date,EmployeeID,ProjectID,TaskID,StageID")] DesignReport designReport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(designReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "FullName", designReport.EmployeeID);
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "Name", designReport.ProjectID);
            ViewData["StageID"] = new SelectList(_context.Stages, "ID", "Name", designReport.StageID);
            ViewData["TaskID"] = new SelectList(_context.Tasks, "ID", "Description", designReport.TaskID);
            return View(designReport);
        }

        // GET: DesignReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designReport = await _context.DesignReports.FindAsync(id);
            if (designReport == null)
            {
                return NotFound();
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "FullName", designReport.EmployeeID);
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "Name", designReport.ProjectID);
            ViewData["StageID"] = new SelectList(_context.Stages, "ID", "Name", designReport.StageID);
            ViewData["TaskID"] = new SelectList(_context.Tasks, "ID", "Description", designReport.TaskID);
            return View(designReport);
        }

        // POST: DesignReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Hour,Date,EmployeeID,ProjectID,TaskID,StageID")] DesignReport designReport)
        {
            if (id != designReport.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(designReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DesignReportExists(designReport.ID))
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
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "FullName", designReport.EmployeeID);
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "Name", designReport.ProjectID);
            ViewData["StageID"] = new SelectList(_context.Stages, "ID", "Name", designReport.StageID);
            ViewData["TaskID"] = new SelectList(_context.Tasks, "ID", "Description", designReport.TaskID);
            return View(designReport);
        }
        [Authorize(Roles = "Admin,Manager")]
        // GET: DesignReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designReport = await _context.DesignReports
                .Include(d => d.Employee)
                .Include(d => d.Project)
                .Include(d => d.Stage)
                .Include(d => d.Task)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (designReport == null)
            {
                return NotFound();
            }

            return View(designReport);
        }
        [Authorize(Roles = "Admin,Manager")]
        // POST: DesignReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var designReport = await _context.DesignReports.FindAsync(id);
            _context.DesignReports.Remove(designReport);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DesignReportExists(int id)
        {
            return _context.DesignReports.Any(e => e.ID == id);
        }
    }
}
