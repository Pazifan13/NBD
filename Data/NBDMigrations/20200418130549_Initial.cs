using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NBD.Data.NBDMigrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "MO");

            migrationBuilder.CreateTable(
                name: "Cities",
                schema: "MO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                schema: "MO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<float>(nullable: true),
                    Cost = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                schema: "MO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Stages",
                schema: "MO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                schema: "MO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    StdTimeAmnt = table.Column<int>(nullable: false),
                    stdTimeUnit = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tools",
                schema: "MO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tools", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                schema: "MO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    CompanyName = table.Column<string>(maxLength: 50, nullable: false),
                    Position = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<long>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    CityID = table.Column<int>(nullable: false),
                    Province = table.Column<string>(nullable: false),
                    PostalCode = table.Column<string>(maxLength: 6, nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Clients_Cities_CityID",
                        column: x => x.CityID,
                        principalSchema: "MO",
                        principalTable: "Cities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "MO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<long>(nullable: false),
                    DepartmentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalSchema: "MO",
                        principalTable: "Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                schema: "MO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AvgNet = table.Column<float>(nullable: true),
                    List = table.Column<float>(nullable: true),
                    SizeAmount = table.Column<int>(nullable: false),
                    SizeUnit = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    MaterialID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Inventories_Materials_MaterialID",
                        column: x => x.MaterialID,
                        principalSchema: "MO",
                        principalTable: "Materials",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaterialRequirements",
                schema: "MO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeliveryDate = table.Column<DateTime>(nullable: true),
                    DeliveryTime = table.Column<DateTime>(nullable: true),
                    InstallDate = table.Column<DateTime>(nullable: true),
                    InstallTime = table.Column<DateTime>(nullable: true),
                    EstQuantity = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    InventoryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialRequirements", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MaterialRequirements_Inventories_InventoryID",
                        column: x => x.InventoryID,
                        principalSchema: "MO",
                        principalTable: "Inventories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                schema: "MO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    ProjSite = table.Column<string>(nullable: true),
                    ProjBidDate = table.Column<DateTime>(nullable: false),
                    EstStartDate = table.Column<DateTime>(nullable: false),
                    EstEndDate = table.Column<DateTime>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    ActAmount = table.Column<float>(nullable: true),
                    EstAmount = table.Column<float>(nullable: false),
                    ClientApproval = table.Column<bool>(nullable: false),
                    AdminApproval = table.Column<bool>(nullable: false),
                    ProjCurrentPhase = table.Column<string>(nullable: true),
                    ClientID = table.Column<int>(nullable: false),
                    ProjIsFlagged = table.Column<bool>(nullable: false),
                    TeamID = table.Column<int>(nullable: true),
                    ToolID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Projects_Clients_ClientID",
                        column: x => x.ClientID,
                        principalSchema: "MO",
                        principalTable: "Clients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_Tools_ToolID",
                        column: x => x.ToolID,
                        principalSchema: "MO",
                        principalTable: "Tools",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BidStageReports",
                schema: "MO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EstimatedBid = table.Column<string>(nullable: true),
                    ActualDesingHours = table.Column<string>(nullable: true),
                    EstimatedDesingHours = table.Column<string>(nullable: true),
                    ActualDesingCost = table.Column<string>(nullable: true),
                    EstimatedDesingCost = table.Column<string>(nullable: true),
                    Hours = table.Column<string>(nullable: true),
                    Remaining = table.Column<string>(nullable: true),
                    ProjectID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BidStageReports", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BidStageReports_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "MO",
                        principalTable: "Projects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DesignReports",
                schema: "MO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Hour = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: true),
                    EmployeeID = table.Column<int>(nullable: false),
                    ProjectID = table.Column<int>(nullable: false),
                    TaskID = table.Column<int>(nullable: false),
                    StageID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignReports", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DesignReports_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalSchema: "MO",
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DesignReports_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "MO",
                        principalTable: "Projects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DesignReports_Stages_StageID",
                        column: x => x.StageID,
                        principalSchema: "MO",
                        principalTable: "Stages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DesignReports_Tasks_TaskID",
                        column: x => x.TaskID,
                        principalSchema: "MO",
                        principalTable: "Tasks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LabourSummaries",
                schema: "MO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Hours = table.Column<int>(nullable: false),
                    ProjectID = table.Column<int>(nullable: false),
                    DepartmentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabourSummaries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LabourSummaries_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalSchema: "MO",
                        principalTable: "Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LabourSummaries_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "MO",
                        principalTable: "Projects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialReports",
                schema: "MO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Quantity = table.Column<int>(nullable: false),
                    Costs = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: true),
                    EmployeeID = table.Column<int>(nullable: false),
                    ProjectID = table.Column<int>(nullable: false),
                    MaterialID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialReports", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MaterialReports_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalSchema: "MO",
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialReports_Materials_MaterialID",
                        column: x => x.MaterialID,
                        principalSchema: "MO",
                        principalTable: "Materials",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialReports_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "MO",
                        principalTable: "Projects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductionTools",
                schema: "MO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Quantity = table.Column<int>(nullable: false),
                    EarliestDelivery = table.Column<DateTime>(nullable: true),
                    LatestDelivery = table.Column<DateTime>(nullable: true),
                    ProjectID = table.Column<int>(nullable: false),
                    ToolID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionTools", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductionTools_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "MO",
                        principalTable: "Projects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductionTools_Tools_ToolID",
                        column: x => x.ToolID,
                        principalSchema: "MO",
                        principalTable: "Tools",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectMaterials",
                schema: "MO",
                columns: table => new
                {
                    MaterialReqID = table.Column<int>(nullable: false),
                    ProjectID = table.Column<int>(nullable: false),
                    MaterialRequirementID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMaterials", x => new { x.ProjectID, x.MaterialReqID });
                    table.ForeignKey(
                        name: "FK_ProjectMaterials_MaterialRequirements_MaterialRequirementID",
                        column: x => x.MaterialRequirementID,
                        principalSchema: "MO",
                        principalTable: "MaterialRequirements",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectMaterials_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "MO",
                        principalTable: "Projects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                schema: "MO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Phase = table.Column<string>(nullable: true),
                    TeamName = table.Column<string>(nullable: false),
                    ProjectID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Teams_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "MO",
                        principalTable: "Projects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkerReports",
                schema: "MO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Hours = table.Column<int>(nullable: false),
                    Costs = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: true),
                    EmployeeID = table.Column<int>(nullable: false),
                    ProjectID = table.Column<int>(nullable: false),
                    TaskID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerReports", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WorkerReports_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalSchema: "MO",
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkerReports_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "MO",
                        principalTable: "Projects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkerReports_Tasks_TaskID",
                        column: x => x.TaskID,
                        principalSchema: "MO",
                        principalTable: "Tasks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LabourRequirements",
                schema: "MO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EstDate = table.Column<DateTime>(nullable: true),
                    EstHours = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: true),
                    Hours = table.Column<int>(nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    TeamID = table.Column<int>(nullable: false),
                    TaskID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabourRequirements", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LabourRequirements_Tasks_TaskID",
                        column: x => x.TaskID,
                        principalSchema: "MO",
                        principalTable: "Tasks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LabourRequirements_Teams_TeamID",
                        column: x => x.TeamID,
                        principalSchema: "MO",
                        principalTable: "Teams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductionPlan",
                schema: "MO",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProjectID = table.Column<int>(nullable: false),
                    TeamID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionPlan", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductionPlan_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "MO",
                        principalTable: "Projects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductionPlan_Teams_TeamID",
                        column: x => x.TeamID,
                        principalSchema: "MO",
                        principalTable: "Teams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeamEmployees",
                schema: "MO",
                columns: table => new
                {
                    TeamID = table.Column<int>(nullable: false),
                    EmployeeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamEmployees", x => new { x.TeamID, x.EmployeeID });
                    table.ForeignKey(
                        name: "FK_TeamEmployees_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalSchema: "MO",
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamEmployees_Teams_TeamID",
                        column: x => x.TeamID,
                        principalSchema: "MO",
                        principalTable: "Teams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectLabours",
                schema: "MO",
                columns: table => new
                {
                    ProjectID = table.Column<int>(nullable: false),
                    LabourReqID = table.Column<int>(nullable: false),
                    LabourRequirementID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectLabours", x => new { x.ProjectID, x.LabourReqID });
                    table.ForeignKey(
                        name: "FK_ProjectLabours_LabourRequirements_LabourRequirementID",
                        column: x => x.LabourRequirementID,
                        principalSchema: "MO",
                        principalTable: "LabourRequirements",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectLabours_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "MO",
                        principalTable: "Projects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdPlanLabours",
                schema: "MO",
                columns: table => new
                {
                    ProdPlanID = table.Column<int>(nullable: false),
                    LabourReqID = table.Column<int>(nullable: false),
                    ProductionPlanID = table.Column<int>(nullable: true),
                    LabourRequirementID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdPlanLabours", x => new { x.ProdPlanID, x.LabourReqID });
                    table.ForeignKey(
                        name: "FK_ProdPlanLabours_LabourRequirements_LabourRequirementID",
                        column: x => x.LabourRequirementID,
                        principalSchema: "MO",
                        principalTable: "LabourRequirements",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProdPlanLabours_ProductionPlan_ProductionPlanID",
                        column: x => x.ProductionPlanID,
                        principalSchema: "MO",
                        principalTable: "ProductionPlan",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProdPlanMaterials",
                schema: "MO",
                columns: table => new
                {
                    ProdPlanID = table.Column<int>(nullable: false),
                    MaterialReqID = table.Column<int>(nullable: false),
                    ProductionPlanID = table.Column<int>(nullable: true),
                    MaterialRequirementID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdPlanMaterials", x => new { x.MaterialReqID, x.ProdPlanID });
                    table.ForeignKey(
                        name: "FK_ProdPlanMaterials_MaterialRequirements_MaterialRequirementID",
                        column: x => x.MaterialRequirementID,
                        principalSchema: "MO",
                        principalTable: "MaterialRequirements",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProdPlanMaterials_ProductionPlan_ProductionPlanID",
                        column: x => x.ProductionPlanID,
                        principalSchema: "MO",
                        principalTable: "ProductionPlan",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductionStageReports",
                schema: "MO",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Bid = table.Column<string>(nullable: true),
                    EstProdPlan = table.Column<string>(nullable: true),
                    TotalCosttoDate = table.Column<string>(nullable: true),
                    ActualMtl = table.Column<string>(nullable: true),
                    EstimatedDesingCost = table.Column<string>(nullable: true),
                    ActuLaborPro = table.Column<string>(nullable: true),
                    EstLaborProdCost = table.Column<string>(nullable: true),
                    ActuLaborDesingCost = table.Column<string>(nullable: true),
                    EstLaborDesingCost = table.Column<string>(nullable: true),
                    ProductionPlanID = table.Column<int>(nullable: false),
                    ProjectID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionStageReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductionStageReports_ProductionPlan_ProductionPlanID",
                        column: x => x.ProductionPlanID,
                        principalSchema: "MO",
                        principalTable: "ProductionPlan",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductionStageReports_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "MO",
                        principalTable: "Projects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BidStageReports_ProjectID_ID",
                schema: "MO",
                table: "BidStageReports",
                columns: new[] { "ProjectID", "ID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CityID",
                schema: "MO",
                table: "Clients",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Email",
                schema: "MO",
                table: "Clients",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DesignReports_EmployeeID",
                schema: "MO",
                table: "DesignReports",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_DesignReports_StageID",
                schema: "MO",
                table: "DesignReports",
                column: "StageID");

            migrationBuilder.CreateIndex(
                name: "IX_DesignReports_TaskID",
                schema: "MO",
                table: "DesignReports",
                column: "TaskID");

            migrationBuilder.CreateIndex(
                name: "IX_DesignReports_ProjectID_TaskID_EmployeeID_StageID",
                schema: "MO",
                table: "DesignReports",
                columns: new[] { "ProjectID", "TaskID", "EmployeeID", "StageID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentID",
                schema: "MO",
                table: "Employees",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                schema: "MO",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_MaterialID",
                schema: "MO",
                table: "Inventories",
                column: "MaterialID");

            migrationBuilder.CreateIndex(
                name: "IX_LabourRequirements_TaskID",
                schema: "MO",
                table: "LabourRequirements",
                column: "TaskID");

            migrationBuilder.CreateIndex(
                name: "IX_LabourRequirements_TeamID",
                schema: "MO",
                table: "LabourRequirements",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_LabourSummaries_DepartmentID",
                schema: "MO",
                table: "LabourSummaries",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_LabourSummaries_ProjectID",
                schema: "MO",
                table: "LabourSummaries",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialReports_EmployeeID",
                schema: "MO",
                table: "MaterialReports",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialReports_MaterialID",
                schema: "MO",
                table: "MaterialReports",
                column: "MaterialID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialReports_ProjectID_MaterialID_EmployeeID",
                schema: "MO",
                table: "MaterialReports",
                columns: new[] { "ProjectID", "MaterialID", "EmployeeID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaterialRequirements_InventoryID",
                schema: "MO",
                table: "MaterialRequirements",
                column: "InventoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ProdPlanLabours_LabourRequirementID",
                schema: "MO",
                table: "ProdPlanLabours",
                column: "LabourRequirementID");

            migrationBuilder.CreateIndex(
                name: "IX_ProdPlanLabours_ProductionPlanID",
                schema: "MO",
                table: "ProdPlanLabours",
                column: "ProductionPlanID");

            migrationBuilder.CreateIndex(
                name: "IX_ProdPlanMaterials_MaterialRequirementID",
                schema: "MO",
                table: "ProdPlanMaterials",
                column: "MaterialRequirementID");

            migrationBuilder.CreateIndex(
                name: "IX_ProdPlanMaterials_ProductionPlanID",
                schema: "MO",
                table: "ProdPlanMaterials",
                column: "ProductionPlanID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionPlan_ProjectID",
                schema: "MO",
                table: "ProductionPlan",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionPlan_TeamID",
                schema: "MO",
                table: "ProductionPlan",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionStageReports_ProductionPlanID",
                schema: "MO",
                table: "ProductionStageReports",
                column: "ProductionPlanID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionStageReports_ProjectID_ProductionPlanID_Id",
                schema: "MO",
                table: "ProductionStageReports",
                columns: new[] { "ProjectID", "ProductionPlanID", "Id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductionTools_ProjectID",
                schema: "MO",
                table: "ProductionTools",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionTools_ToolID",
                schema: "MO",
                table: "ProductionTools",
                column: "ToolID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectLabours_LabourRequirementID",
                schema: "MO",
                table: "ProjectLabours",
                column: "LabourRequirementID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMaterials_MaterialRequirementID",
                schema: "MO",
                table: "ProjectMaterials",
                column: "MaterialRequirementID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ClientID",
                schema: "MO",
                table: "Projects",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_TeamID",
                schema: "MO",
                table: "Projects",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ToolID",
                schema: "MO",
                table: "Projects",
                column: "ToolID");

            migrationBuilder.CreateIndex(
                name: "IX_TeamEmployees_EmployeeID",
                schema: "MO",
                table: "TeamEmployees",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ProjectID",
                schema: "MO",
                table: "Teams",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerReports_EmployeeID",
                schema: "MO",
                table: "WorkerReports",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerReports_TaskID",
                schema: "MO",
                table: "WorkerReports",
                column: "TaskID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerReports_ProjectID_TaskID_EmployeeID",
                schema: "MO",
                table: "WorkerReports",
                columns: new[] { "ProjectID", "TaskID", "EmployeeID" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Teams_TeamID",
                schema: "MO",
                table: "Projects",
                column: "TeamID",
                principalSchema: "MO",
                principalTable: "Teams",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Projects_ProjectID",
                schema: "MO",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "BidStageReports",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "DesignReports",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "LabourSummaries",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "MaterialReports",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "ProdPlanLabours",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "ProdPlanMaterials",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "ProductionStageReports",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "ProductionTools",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "ProjectLabours",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "ProjectMaterials",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "TeamEmployees",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "WorkerReports",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "Stages",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "ProductionPlan",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "LabourRequirements",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "MaterialRequirements",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "Tasks",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "Inventories",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "Departments",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "Materials",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "Projects",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "Clients",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "Teams",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "Tools",
                schema: "MO");

            migrationBuilder.DropTable(
                name: "Cities",
                schema: "MO");
        }
    }
}
