using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NBD.Data;
using NBD.Models;

namespace NBD.Controllers
{
    public class LabourRequirementsController : Controller
    {
        private readonly NBDContext _context;

        public LabourRequirementsController(NBDContext context)
        {
            _context = context;
        }

        // GET: LabourRequirements
        public async Task<IActionResult> Index()
        {
            var nBDContext = _context.LabourRequirements.Include(l => l.Task).Include(l => l.Team);
            return View(await nBDContext.ToListAsync());
        }

        // GET: LabourRequirements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labourRequirement = await _context.LabourRequirements
                .Include(l => l.Task)
                .Include(l => l.Team)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (labourRequirement == null)
            {
                return NotFound();
            }

            return View(labourRequirement);
        }

        // GET: LabourRequirements/Create
        public IActionResult Create()
        {
            ViewData["TaskID"] = new SelectList(_context.Tasks, "ID", "ID");
            ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "TeamName");
            return View();
        }

        // POST: LabourRequirements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,EstDate,EstHours,Date,Hours,Comments,TeamID,TaskID")] LabourRequirement labourRequirement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(labourRequirement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TaskID"] = new SelectList(_context.Tasks, "ID", "ID", labourRequirement.TaskID);
            ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "TeamName", labourRequirement.TeamID);
            return View(labourRequirement);
        }

        // GET: LabourRequirements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labourRequirement = await _context.LabourRequirements.FindAsync(id);
            if (labourRequirement == null)
            {
                return NotFound();
            }
            ViewData["TaskID"] = new SelectList(_context.Tasks, "ID", "ID", labourRequirement.TaskID);
            ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "TeamName", labourRequirement.TeamID);
            return View(labourRequirement);
        }

        // POST: LabourRequirements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,EstDate,EstHours,Date,Hours,Comments,TeamID,TaskID")] LabourRequirement labourRequirement)
        {
            if (id != labourRequirement.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(labourRequirement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabourRequirementExists(labourRequirement.ID))
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
            ViewData["TaskID"] = new SelectList(_context.Tasks, "ID", "ID", labourRequirement.TaskID);
            ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "TeamName", labourRequirement.TeamID);
            return View(labourRequirement);
        }

        // GET: LabourRequirements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labourRequirement = await _context.LabourRequirements
                .Include(l => l.Task)
                .Include(l => l.Team)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (labourRequirement == null)
            {
                return NotFound();
            }

            return View(labourRequirement);
        }

        // POST: LabourRequirements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var labourRequirement = await _context.LabourRequirements.FindAsync(id);
            _context.LabourRequirements.Remove(labourRequirement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LabourRequirementExists(int id)
        {
            return _context.LabourRequirements.Any(e => e.ID == id);
        }
    }
}
