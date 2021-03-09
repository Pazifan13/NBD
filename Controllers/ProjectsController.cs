using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NBD.Data;
using NBD.Models;
using NBD.ViewModels;

namespace NBD.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly NBDContext _context;

        public ProjectsController(NBDContext context)
        {
            _context = context;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var projects = from p in _context.Projects
                .Include(p => p.Client)
                .Include (p=>p.ProjectLabours)
                .ThenInclude(pl => pl.LabourRequirement)
                .Include(p => p.ProjectLabours)
                .ThenInclude(pl => pl.LabourRequirement)
                .ThenInclude(l => l.Task)
                .Include(p=>p.ProjectLabours)
                .ThenInclude(pl=>pl.LabourRequirement)
                .ThenInclude(l=>l.Team)
                select p;
            
            return View(await projects.ToListAsync());
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Client)
                 .Include(p => p.ProjectLabours)
                .ThenInclude(pl => pl.LabourRequirement)
                .Include(p => p.ProjectLabours)
                .ThenInclude(pl => pl.LabourRequirement)
                .ThenInclude(l => l.Task)
                .Include(p => p.ProjectLabours)
                .ThenInclude(pl => pl.LabourRequirement)
                .ThenInclude(l => l.Team)
                .Include(p=>p.ProjectMaterials)
                .ThenInclude(pm=>pm.MaterialRequirement)
                .ThenInclude(m=>m.Inventory)
                .ThenInclude(i=>i.Material)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (project == null)
            {
                return NotFound();
            }
            PopulateAssignedLaborData(project);
            PopulateAssignedMaterialData(project);
            return View(project);
        }

        [Authorize(Roles = "Admin,Manager")]
        // GET: Projects/Create
        public IActionResult Create()
        {
            Project project = new Project();

            PopulateAssignedLaborData(project);
            PopulateAssignedMaterialData(project);
            ViewData["ClientID"] = new SelectList(_context.Clients, "ID", "FullName");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Create([Bind("ID,Name,ProjSite,ProjBidDate,EstStartDate,EstEndDate,StartDate,EndDate,ActAmount,EstAmount,ClientApproval,AdminApproval,ProjCurrentPhase,ClientID,ProjIsFlagged")] Project project, string [] selectedLabors, string[] selectedMaterials)
        {
            try
            {
                UpdateProjectLabours(selectedLabors, project);
                UpdateProjectMaterials(selectedMaterials, project);
                if (ModelState.IsValid)
                {
                    _context.Add(project);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(RetryLimitExceededException /* dex */)
                {
                    ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
                }

            PopulateAssignedLaborData(project);
            PopulateAssignedMaterialData(project);
            ViewData["ClientID"] = new SelectList(_context.Clients, "ID", "FullName", project.ClientID);
            return View(project);
        }

        // GET: Projects/Edit/5
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Client)
                .Include(p => p.ProjectLabours)
                .ThenInclude(p => p.LabourRequirement)
                .ThenInclude(p=>p.Task)
                .Include(p => p.ProjectLabours)
                .ThenInclude(p => p.LabourRequirement)
                .ThenInclude(p => p.Team)
                .Include(p=>p.ProjectMaterials)
                .ThenInclude(pm => pm.MaterialRequirement)
                .ThenInclude(m => m.Inventory)
                .ThenInclude(i => i.Material)
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.ID == id);
            if (project == null)
            {
                return NotFound();
            }

            PopulateAssignedMaterialData(project);
            PopulateAssignedLaborData(project);
            ViewData["ClientID"] = new SelectList(_context.Clients, "ID", "FullName", project.ClientID);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,ProjSite,ProjBidDate,EstStartDate,EstEndDate,StartDate,EndDate,ActAmount,EstAmount,ClientApproval,AdminApproval,ProjCurrentPhase,ClientID,ProjIsFlagged")] Project project, string[] selectedLabors, string[] selectedMaterials)
        {
            var projectToUpdate = await _context.Projects
                .Include(p => p.Client)
                .Include(p => p.ProjectLabours)
                .ThenInclude(p => p.LabourRequirement)
                 .Include(p => p.ProjectLabours)
                .ThenInclude(pl => pl.LabourRequirement)
                .ThenInclude(l => l.Task)
                .Include(p => p.ProjectLabours)
                .ThenInclude(pl => pl.LabourRequirement)
                .ThenInclude(l => l.Team)
                .Include(p => p.ProjectMaterials)
                .ThenInclude(pm => pm.MaterialRequirement)
                .ThenInclude(m => m.Inventory)
                .ThenInclude(i => i.Material)

                .SingleOrDefaultAsync(p => p.ID == id);

            if(projectToUpdate == null)
            {
                return NotFound();
            }

            UpdateProjectLabours(selectedLabors, projectToUpdate);
            UpdateProjectMaterials(selectedMaterials, projectToUpdate);

            if(await TryUpdateModelAsync<Project>(projectToUpdate,"",
                    p=>p.Name, p=>p.ProjSite,p=>p.ProjBidDate,p=>p.EstStartDate,
                    p=>p.EstEndDate, p => p.StartDate, p => p.EndDate, p => p.ActAmount,
                    p => p.EstAmount, p => p.ClientApproval, p => p.AdminApproval,
                    p => p.ProjCurrentPhase, p => p.ProjIsFlagged))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(projectToUpdate.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }


            if (id != project.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ID))
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
            PopulateAssignedLaborData(projectToUpdate);
            PopulateAssignedMaterialData(projectToUpdate);
            ViewData["ClientID"] = new SelectList(_context.Clients, "ID", "FullName", project.ClientID);
            return View(projectToUpdate);
        }

        // GET: Projects/Delete/5
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Client)
                .Include(p => p.ProjectLabours)
                .ThenInclude(p => p.LabourRequirement)
                .Include(p => p.ProjectLabours)
                .ThenInclude(pl => pl.LabourRequirement)
                .ThenInclude(l => l.Task)
                .Include(p => p.ProjectLabours)
                .ThenInclude(pl => pl.LabourRequirement)
                .ThenInclude(l => l.Team)
                .Include(p => p.ProjectMaterials)
                .ThenInclude(pm => pm.MaterialRequirement)
                .ThenInclude(m => m.Inventory)
                .ThenInclude(i => i.Material)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void PopulateAssignedLaborData(Project project)
        {
            var allLabors = _context.LabourRequirements
                .Include(l=>l.Task)
                .Include(l=>l.Team);
            var projLabors = new HashSet<int?>
                (project.ProjectLabours.Select(p => p.LabourReqID));
            var selectedLabor = new List<LabourReqVM>();
            var availableLabor = new List<LabourReqVM>();

            foreach (var l in allLabors)
            {
                if(projLabors.Contains(l.ID))
                {
                    selectedLabor.Add(new LabourReqVM
                    {
                        ID = l.ID,
                        DisplayText = l.Task.Description
                        + ", " + l.EstHours.ToString()
                        + ",  " + l.EstDate.ToString
                        ()
                        + ", " + l.Team.Phase
                        + (string.IsNullOrEmpty(l.Date.ToString())? " " : (" " + l.Date.ToString()))
                        + (string.IsNullOrEmpty(l.Hours.ToString()) ? " " : (" " + l.Hours.ToString()))
                        + (string.IsNullOrEmpty(l.Comments) ? " " : (" " + l.Comments))
                       
                    });
                }
                else
                {
                    availableLabor.Add(new LabourReqVM
                    {
                        ID = l.ID,
                        DisplayText = l.Task.Description
                        + ", " + l.EstHours.ToString()
                        + ",  " + l.EstDate.ToString()
                        + ", " + l.Team.Phase
                        + (string.IsNullOrEmpty(l.Date.ToString()) ? " " : (" " + l.Date.ToString()))
                        + (string.IsNullOrEmpty(l.Hours.ToString()) ? " " : (" " + l.Hours.ToString()))
                        + (string.IsNullOrEmpty(l.Comments) ? " " : (" " + l.Comments))
                    });
                }
            }
            ViewData["selLabors"] = new MultiSelectList(selectedLabor.OrderBy(s => s.DisplayText), "ID", "DisplayText");
            ViewData["availLabors"] = new MultiSelectList(availableLabor.OrderBy(s => s.DisplayText), "ID", "DisplayText");
        }
        private void UpdateProjectLabours(string[] selectedLabors, Project projectToUpdate)
        {
            if(selectedLabors == null)
            {
                projectToUpdate.ProjectLabours = new List<ProjectLabour>();
                return;

            }
            var selectedLaborsHS = new HashSet<string>(selectedLabors);
                    var projLabors = new HashSet<int?>
                                     (projectToUpdate.ProjectLabours.Select(p => p.LabourReqID));

                    foreach(var l in _context.LabourRequirements
                                             .Include(l => l.Task)
                                             .Include(l => l.Team))
            {
                if (selectedLaborsHS.Contains(l.ID.ToString()))
                {
                    if (!projLabors.Contains(l.ID))
                    {
                        projectToUpdate.ProjectLabours.Add(new ProjectLabour
                        {
                            LabourReqID = l.ID,
                            ProjectID = projectToUpdate.ID

                        });
                    }
                }
                else
                {
                    if (projLabors.Contains(l.ID))
                    {
                        ProjectLabour labourToRemove = projectToUpdate.ProjectLabours.SingleOrDefault(p => p.LabourReqID == l.ID);
                        _context.Remove(labourToRemove);
                       
                    }
                        
                }
                      
            }
            
        }

        private void PopulateAssignedMaterialData(Project project)
        {
            var allMaterials = _context.MaterialRequirements
                .Include(m=>m.Inventory)
                .ThenInclude(m=>m.Material);
            var projMaterials = new HashSet<int>
                (project.ProjectMaterials?.Select(p => p.MaterialReqID));
            var selectedMaterials = new List<MaterialReqVM>();
            var availableMaterials = new List<MaterialReqVM>();

            foreach (var m in allMaterials)
            {
                if (projMaterials.Contains(m.ID))
                {
                    selectedMaterials.Add(new MaterialReqVM
                    {
                        ID = m.ID,
                        DisplayText = m.Inventory.Material.Description
                        + ", " + m.DeliveryDate.ToString() +" @ "+ m.DeliveryTime.ToString()
                        + ",  " + m.InstallDate.ToString() + " @ " + m.InstallTime.ToString()
                        + ", " + m.EstQuantity.ToString()
                        + (string.IsNullOrEmpty(m.Quantity.ToString()) ? " " : (" " + m.Quantity.ToString()))
                        

                    });
                }
                else
                {
                    availableMaterials.Add(new MaterialReqVM
                    {
                        ID = m.ID,
                        DisplayText = m.Inventory.Material.Description
                        + ", " + m.DeliveryDate.ToString() + " @ " + m.DeliveryTime.ToString()
                        + ",  " + m.InstallDate.ToString() + " @ " + m.InstallTime.ToString()
                        + ", " + m.EstQuantity.ToString()
                        + (string.IsNullOrEmpty(m.Quantity.ToString()) ? " " : (" " + m.Quantity.ToString()))

                    });
                }
            }
            ViewData["selMaterials"] = new MultiSelectList(selectedMaterials.OrderBy(s => s.DisplayText), "ID", "DisplayText");
            ViewData["availMaterials"] = new MultiSelectList(availableMaterials.OrderBy(s => s.DisplayText), "ID", "DisplayText");
        }
        private void UpdateProjectMaterials(string[] selectedMaterials, Project projectToUpdate)
        {
            if (selectedMaterials == null)
            {
                projectToUpdate.ProjectMaterials = new List<ProjectMaterial>();
                return;

            }
            var selectedMaterialsHS = new HashSet<string>(selectedMaterials);
            var projMaterials = new HashSet<int>
                             (projectToUpdate.ProjectMaterials?.Select(p => p.MaterialReqID));

            foreach (var m in _context.MaterialRequirements 
                .Include(m=>m.Inventory) 
                .ThenInclude(i=>i.Material))
            {
                if (selectedMaterialsHS.Contains(m.ID.ToString()))
                {
                    if (!projMaterials.Contains(m.ID))
                    {
                        projectToUpdate.ProjectMaterials.Add(new ProjectMaterial
                        {
                            MaterialReqID = m.ID,
                            ProjectID = projectToUpdate.ID

                        });
                    }
                }
                else
                {
                    if (projMaterials.Contains(m.ID))
                    {
                        ProjectMaterial materialToRemove = projectToUpdate.ProjectMaterials.SingleOrDefault(p => p.MaterialReqID == m.ID);
                        _context.Remove(materialToRemove);

                    }

                }

            }

        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.ID == id);
        }
    }
}
