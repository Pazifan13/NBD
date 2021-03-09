using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NBD.Models;

namespace NBD.Data
{
    public class NBDContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public string UserName
        {
            get; private set;
        }

        public NBDContext (DbContextOptions<NBDContext> options)
            : base(options)
        {
            UserName = "SeedData";
        }

        public NBDContext(DbContextOptions<NBDContext> options, IHttpContextAccessor httpContextAccessor)
           : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            UserName = _httpContextAccessor.HttpContext?.User.Identity.Name;
            //UserName = (UserName == null) ? "Unknown" : UserName;
            UserName = UserName ?? "Unknown";
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<TeamEmployee> TeamEmployees { get; set; }
        public DbSet<LabourSummary> LabourSummaries { get; set; }
        public DbSet<MaterialRequirement> MaterialRequirements { get; set; }
       
        public DbSet<Team> Teams { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Tool> Tools { get; set; }
        public DbSet<ProductionTool> ProductionTools { get; set; }
        public DbSet<LabourRequirement> LabourRequirements { get; set; }
        public DbSet<ProjectMaterial> ProjectMaterials { get; set; }
        public DbSet<ProjectLabour> ProjectLabours { get; set; }
        public DbSet<ProdPlanLabour> ProdPlanLabours { get; set; }
        public DbSet<ProdPlanMaterial> ProdPlanMaterials { get; set; }
        public DbSet<ProductionPlan> ProductionPlans { get; set; }

        public DbSet<WorkerReport> WorkerReports { get; set; }

        public DbSet<MaterialReport> MaterialReports { get; set; }

        public DbSet<ProductionStageReport> ProductionStageReports { get; set; }

        public DbSet<BidStageReport> BidStageReports { get; set; }

        public DbSet<Stage> Stages { get; set; }

        public DbSet<DesignReport> DesignReports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("MO");


            modelBuilder.Entity<WorkerReport>()
            .HasIndex(pt => new { pt.ProjectID, pt.TaskID, pt.EmployeeID })
            .IsUnique();

            modelBuilder.Entity<DesignReport>()
            .HasIndex(pt => new { pt.ProjectID, pt.TaskID, pt.EmployeeID, pt.StageID })
            .IsUnique();

            modelBuilder.Entity<MaterialReport>()
            .HasIndex(pt => new { pt.ProjectID, pt.MaterialID, pt.EmployeeID })
            .IsUnique();

            modelBuilder.Entity<ProductionStageReport>()
           .HasIndex(pt => new { pt.ProjectID, pt.ProductionPlanID, pt.Id})
           .IsUnique();

            modelBuilder.Entity<BidStageReport>()
           .HasIndex(pt => new { pt.ProjectID, pt.ID})
           .IsUnique();


            //Prevent Cascade Delete
            modelBuilder.Entity<Department>()
                .HasMany<Employee>(d => d.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Client>()
               .HasMany<Project>(c => c.Projects)
               .WithOne(p => p.Client)
               .HasForeignKey(p => p.ClientID)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Material>()
              .HasMany<Inventory>(m => m.Inventories)
              .WithOne(i => i.Material)
              .HasForeignKey(i => i.MaterialID)
              .OnDelete(DeleteBehavior.Restrict);
           
            modelBuilder.Entity<Team>()
              .HasMany<LabourRequirement>(t => t.LabourRequirements)
              .WithOne(l => l.Team)
              .HasForeignKey(l => l.TeamID)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Team>()
              .HasMany<ProductionPlan>(t => t.ProductionPlans)
              .WithOne(p => p.Team)
              .HasForeignKey(p => p.TeamID)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
              .HasMany<ProductionPlan>(pr => pr.ProductionPlans)
              .WithOne(p => p.Project)
              .HasForeignKey(p => p.ProjectID)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<NBD.Models.Task>()
              .HasMany<LabourRequirement>(t => t.LabourRequirements)
              .WithOne(l => l.Task)
              .HasForeignKey(l => l.TaskID)
              .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Team>()
               .HasMany<TeamEmployee>(t => t.TeamEmployees)
               .WithOne(l => l.Team)
               .HasForeignKey(l => l.TeamID)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasOne(t => t.Team)
                .WithMany(p => p.Projects)
                .HasForeignKey(p => p.TeamID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                        .HasMany<TeamEmployee>(p => p.TeamEmployees)
                        .WithOne(l => l.Employee)
                        .HasForeignKey(l => l.EmployeeID)
                        .OnDelete(DeleteBehavior.Restrict);

            //unique Email Vlaues
            modelBuilder.Entity<Employee>()
             .HasIndex(e => new { e.Email })
             .IsUnique();

            modelBuilder.Entity<Client>()
            .HasIndex(c => new { c.Email })
            .IsUnique();

            //Many-To-Many Relationships
            modelBuilder.Entity<ProdPlanMaterial>()
           .HasKey(ls => new { ls.MaterialReqID, ls.ProdPlanID });
            
            modelBuilder.Entity<ProdPlanLabour>()
           .HasKey(t => new { t.ProdPlanID, t.LabourReqID });

            modelBuilder.Entity<ProjectLabour>()
           .HasKey(m => new { m.ProjectID, m.LabourReqID });

            modelBuilder.Entity<ProjectMaterial>()
            .HasKey(pt => new { pt.ProjectID, pt.MaterialReqID });

            modelBuilder.Entity<TeamEmployee>()
            .HasKey(t => new { t.TeamID, t.EmployeeID });


        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }


        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is IAuditable trackable)
                {
                    var now = DateTime.UtcNow;
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackable.UpdatedOn = now;
                            trackable.UpdatedBy = UserName;
                            break;

                        case EntityState.Added:
                            trackable.CreatedOn = now;
                            trackable.CreatedBy = UserName;
                            trackable.UpdatedOn = now;
                            trackable.UpdatedBy = UserName;
                            break;
                    }
                }
            }
        }


        public DbSet<NBD.Models.ProductionPlan> ProductionPlan { get; set; }
    }
}
