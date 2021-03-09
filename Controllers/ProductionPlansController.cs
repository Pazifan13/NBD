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
    public class ProductionPlansController : Controller
    {
        private readonly NBDContext _context;

        public ProductionPlansController(NBDContext context)
        {
            _context = context;
        }

        // GET: ProductionPlans
        public async Task<IActionResult> Index()
        {
            var productionPlans = from pp in _context.ProductionPlans
                .Include(p => p.Project)
                .Include(p => p.Team)
                .Include(p => p.ProdPlanLabours)
                .ThenInclude(pl => pl.LabourRequirement)
                .ThenInclude(l => l.Task)
                .Include(p => p.ProdPlanMaterials)
                .ThenInclude(pm => pm.MaterialRequirement)
                .ThenInclude(m => m.Inventory)
                .ThenInclude(i => i.Material)
                select pp;
            
            return View(await productionPlans.ToListAsync());
        }

        // GET: ProductionPlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productionPlan = await _context.ProductionPlans
                .Include(p => p.Project)
                .Include(p => p.Team)
                .Include(p => p.ProdPlanLabours)
                .ThenInclude(pl => pl.LabourRequirement)
                .ThenInclude(l => l.Task)
                .Include(p => p.ProdPlanMaterials)
                .ThenInclude(pm => pm.MaterialRequirement)
                .ThenInclude(m => m.Inventory)
                .ThenInclude(i => i.Material)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (productionPlan == null)
            {
                return NotFound();
            }

            PopulateAssignedLaborData(productionPlan);
            PopulateAssignedMaterialData(productionPlan);
            return View(productionPlan);
        }

        [Authorize(Roles = "Admin,Manager")]
        // GET: ProductionPlans/Create
        public IActionResult Create()
        {
            ProductionPlan productionPlan = new ProductionPlan();

            PopulateAssignedLaborData(productionPlan);
            PopulateAssignedMaterialData(productionPlan);
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "Name");
            ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "TeamName");
            return View();
        }

        // POST: ProductionPlans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Create([Bind("ID,ProjectID,TeamID")] ProductionPlan productionPlan, string[] selectedProdLabors, string[] selectedProdMaterials)
        {
            try
            {
                UpdateProdLabors(selectedProdLabors, productionPlan);
                UpdateProdMaterials(selectedProdMaterials, productionPlan);
                if (ModelState.IsValid)
                {
                    _context.Add(productionPlan);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
            }

            PopulateAssignedLaborData(productionPlan);
            PopulateAssignedMaterialData(productionPlan);
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "Name", productionPlan.ProjectID);
            ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "TeamName", productionPlan.TeamID);
            return View(productionPlan);
        }

        [Authorize(Roles = "Admin,Manager")]
        // GET: ProductionPlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productionPlan = await _context.ProductionPlans
                .Include(p => p.Project)
                .Include(p => p.Team)
                .Include(p => p.ProdPlanLabours)
                .ThenInclude(pl => pl.LabourRequirement)
                .ThenInclude(l => l.Task)
                .Include(p => p.ProdPlanMaterials)
                .ThenInclude(pm => pm.MaterialRequirement)
                .ThenInclude(m => m.Inventory)
                .ThenInclude(i => i.Material)
                .AsNoTracking()
                .SingleOrDefaultAsync(d => d.ID == id);
                
            if (productionPlan == null)
            {
                return NotFound();
            }

            PopulateAssignedLaborData(productionPlan);
            PopulateAssignedMaterialData(productionPlan);
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "Name", productionPlan.ProjectID);
            ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "TeamName", productionPlan.TeamID);
            return View(productionPlan);
        }

        // POST: ProductionPlans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ProjectID,TeamID")] ProductionPlan productionPlan, string[] selectedProdLabors, string[] selectedProdMaterials)
        {
            var productionPlanToUpdate = await _context.ProductionPlans
                .Include(p => p.Project)
                .Include(p => p.Team)
                .Include(p => p.ProdPlanLabours)
                .ThenInclude(pl => pl.LabourRequirement)
                .ThenInclude(l => l.Task)
                .Include(p => p.ProdPlanMaterials)
                .ThenInclude(pm => pm.MaterialRequirement)
                .ThenInclude(m => m.Inventory)
                .ThenInclude(i => i.Material)
                .SingleOrDefaultAsync(d => d.ID == id);
            if(productionPlanToUpdate == null)
            {
                return NotFound();
            }
            UpdateProdLabors(selectedProdLabors, productionPlanToUpdate);
            UpdateProdMaterials(selectedProdMaterials, productionPlanToUpdate);

            if(await TryUpdateModelAsync<ProductionPlan> 
                (productionPlanToUpdate,"",
                p=>p.ProjectID, p=>p.TeamID
                ))
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
                    if (!ProductionPlanExists(productionPlanToUpdate.ID))
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
            if (id != productionPlan.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productionPlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductionPlanExists(productionPlan.ID))
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
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "Name", productionPlan.ProjectID);
            ViewData["TeamID"] = new SelectList(_context.Teams, "ID", "TeamName", productionPlan.TeamID);
            return View(productionPlan);
        }

        // GET: ProductionPlans/Delete/5
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productionPlan = await _context.ProductionPlans
                .Include(p => p.Project)
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (productionPlan == null)
            {
                return NotFound();
            }

            return View(productionPlan);
        }

        // POST: ProductionPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productionPlan = await _context.ProductionPlans.FindAsync(id);
            _context.ProductionPlans.Remove(productionPlan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void PopulateAssignedLaborData(ProductionPlan productionPlan)
        {
            var allLabors = _context.LabourRequirements
                .Include(l => l.Task)
                .Include(l => l.Team);
            var prodLabors = new HashSet<int>
                (productionPlan.ProdPlanLabours.Select(p => p.LabourReqID));
            var selectedProdLabor = new List<LabourReqVM>();
            var availableProdLabor = new List<LabourReqVM>();

            foreach (var l in allLabors)
            {
                if (prodLabors.Contains(l.ID))
                {
                    selectedProdLabor.Add(new LabourReqVM
                    {
                        ID = l.ID,
                        DisplayText = l.Task.Description
                        + ", " + l.EstHours.ToString()
                        + ",  " + l.EstDate.ToString
                        ()
                        + ", " + l.Team.Phase
                        + (string.IsNullOrEmpty(l.Date.ToString()) ? " " : (" " + l.Date.ToString()))
                        + (string.IsNullOrEmpty(l.Hours.ToString()) ? " " : (" " + l.Hours.ToString()))
                        + (string.IsNullOrEmpty(l.Comments) ? " " : (" " + l.Comments))

                    });
                }
                else
                {
                    availableProdLabor.Add(new LabourReqVM
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
            ViewData["selProdLabors"] = new MultiSelectList(selectedProdLabor.OrderBy(s => s.DisplayText), "ID", "DisplayText");
            ViewData["availProdLabors"] = new MultiSelectList(availableProdLabor.OrderBy(s => s.DisplayText), "ID", "DisplayText");
        }
        private void UpdateProdLabors(string[] selectedProdLabors, ProductionPlan productionPlanToUpdate)
        {
            if (selectedProdLabors == null)
            {
                productionPlanToUpdate.ProdPlanLabours = new List<ProdPlanLabour>();
                return;

            }
            var selectedProdLaborsHS = new HashSet<string>(selectedProdLabors);
            var prodLabors = new HashSet<int>
                             (productionPlanToUpdate.ProdPlanLabours.Select(p => p.LabourReqID));

            foreach (var l in _context.LabourRequirements)
            {
                if (selectedProdLaborsHS.Contains(l.ID.ToString()))
                {
                    if (!prodLabors.Contains(l.ID))
                    {
                        productionPlanToUpdate.ProdPlanLabours.Add(new ProdPlanLabour
                        {
                            LabourReqID = l.ID,
                            ProdPlanID = productionPlanToUpdate.ID

                        });
                    }
                }
                else
                {
                    if (prodLabors.Contains(l.ID))
                    {
                        ProdPlanLabour labourToRemove = productionPlanToUpdate.ProdPlanLabours.SingleOrDefault(p => p.LabourReqID == l.ID);
                        _context.Remove(labourToRemove);

                    }

                }

            }

        }

        private void PopulateAssignedMaterialData(ProductionPlan productionPlan)
        {
            var allMaterials = _context.MaterialRequirements
                .Include(m => m.Inventory)
                .ThenInclude(m => m.Material);
            var prodMaterials = new HashSet<int>
                (productionPlan.ProdPlanMaterials.Select(p => p.MaterialReqID));
            var selectedProdMaterials = new List<MaterialReqVM>();
            var availableProdMaterials = new List<MaterialReqVM>();

            foreach (var m in allMaterials)
            {
                if (prodMaterials.Contains(m.ID))
                {
                    selectedProdMaterials.Add(new MaterialReqVM
                    {
                        ID = m.ID,
                        DisplayText = m.Inventory.Material.Description
                        + ", " + m.DeliveryDate.ToString() + " @ " + m.DeliveryTime.ToString()
                        + ",  " + m.InstallDate.ToString() + " @ " + m.InstallTime.ToString()
                        + ", " + m.EstQuantity.ToString()
                        + (string.IsNullOrEmpty(m.Quantity.ToString()) ? " " : (" " + m.Quantity.ToString()))


                    });
                }
                else
                {
                    availableProdMaterials.Add(new MaterialReqVM
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
            ViewData["selProdMaterials"] = new MultiSelectList(selectedProdMaterials.OrderBy(s => s.DisplayText), "ID", "DisplayText");
            ViewData["availProdMaterials"] = new MultiSelectList(availableProdMaterials.OrderBy(s => s.DisplayText), "ID", "DisplayText");
        }
        private void UpdateProdMaterials(string[] selectedProdMaterials, ProductionPlan productionPlanToUpdate)
        {
            if (selectedProdMaterials == null)
            {
                productionPlanToUpdate.ProdPlanMaterials = new List<ProdPlanMaterial>();
                return;

            }
            var selectedProdMaterialsHS = new HashSet<string>(selectedProdMaterials);
            var prodMaterials = new HashSet<int>
                             (productionPlanToUpdate.ProdPlanMaterials.Select(p => p.MaterialReqID));

            foreach (var m in _context.MaterialRequirements
                .Include(m => m.Inventory)
                .ThenInclude(i => i.Material))
            {
                if (selectedProdMaterialsHS.Contains(m.ID.ToString()))
                {
                    if (!prodMaterials.Contains(m.ID))
                    {
                        productionPlanToUpdate.ProdPlanMaterials.Add(new ProdPlanMaterial
                        {
                            MaterialReqID = m.ID,
                            ProdPlanID = productionPlanToUpdate.ID

                        });
                    }
                }
                else
                {
                    if (prodMaterials.Contains(m.ID))
                    {
                        ProdPlanMaterial materialToRemove = productionPlanToUpdate.ProdPlanMaterials.SingleOrDefault(p => p.MaterialReqID == m.ID);
                        _context.Remove(materialToRemove);

                    }

                }

            }

        }

        private bool ProductionPlanExists(int id)
        {
            return _context.ProductionPlans.Any(e => e.ID == id);
        }
    }
}
