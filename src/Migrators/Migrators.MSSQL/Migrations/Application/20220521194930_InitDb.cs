using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Edu");

            migrationBuilder.EnsureSchema(
                name: "Auditing");

            migrationBuilder.EnsureSchema(
                name: "Catalog");

            migrationBuilder.EnsureSchema(
                name: "Identity");

            migrationBuilder.CreateTable(
                name: "Audience_types",
                schema: "Edu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audience_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditTrails",
                schema: "Auditing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AffectedColumns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryKey = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cathedras",
                schema: "Edu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Name_abbreviation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cathedras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Education_cycles",
                schema: "Edu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Education_cycle_index = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Education_cycles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Education_forms",
                schema: "Edu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Education_forms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Education_levels",
                schema: "Edu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Education_levels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Education_modules",
                schema: "Edu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Education_module_index = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Education_modules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fixed_discipline_statuses",
                schema: "Edu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fixed_discipline_statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fses_category_partitions",
                schema: "Edu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    First_part_number = table.Column<int>(type: "int", nullable: false),
                    Second_part_number = table.Column<int>(type: "int", nullable: false),
                    Third_path_number = table.Column<short>(type: "smallint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Name_abbreviation = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fses_category_partitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Intermediate_certification_forms",
                schema: "Edu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intermediate_certification_forms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                schema: "Edu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ObjectId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "Catalog",
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Received_education_forms",
                schema: "Edu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Education_form_id = table.Column<int>(type: "int", nullable: false),
                    Additional_info = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Received_education_forms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Received_education_forms_Education_forms",
                        column: x => x.Education_form_id,
                        principalSchema: "Edu",
                        principalTable: "Education_forms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Disciplines",
                schema: "Edu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Discipline_index = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Cathedra_id = table.Column<int>(type: "int", nullable: true),
                    Education_cycle_id = table.Column<int>(type: "int", nullable: false),
                    Education_module_id = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disciplines_Cathedras",
                        column: x => x.Cathedra_id,
                        principalSchema: "Edu",
                        principalTable: "Cathedras",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Disciplines_Education_cycles_and_modules",
                        column: x => x.Education_cycle_id,
                        principalSchema: "Edu",
                        principalTable: "Education_cycles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Disciplines_Education_modules",
                        column: x => x.Education_module_id,
                        principalSchema: "Edu",
                        principalTable: "Education_modules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cathedra_specialties",
                schema: "Edu",
                columns: table => new
                {
                    Cathedra_id = table.Column<int>(type: "int", nullable: false),
                    Fses_category_partition_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cathedra_specialties", x => new { x.Cathedra_id, x.Fses_category_partition_id });
                    table.ForeignKey(
                        name: "FK_Cathedra_specialties_Cathedras",
                        column: x => x.Cathedra_id,
                        principalSchema: "Edu",
                        principalTable: "Cathedras",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cathedra_specialties_Fses_category_partitions",
                        column: x => x.Fses_category_partition_id,
                        principalSchema: "Edu",
                        principalTable: "Fses_category_partitions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Education_plans",
                schema: "Edu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fses_category_partition_id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(65)", maxLength: 65, nullable: false),
                    Begining_year = table.Column<short>(type: "smallint", nullable: false),
                    Ending_year = table.Column<short>(type: "smallint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Education_plans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Education_plans_Fses_category_partitions",
                        column: x => x.Fses_category_partition_id,
                        principalSchema: "Edu",
                        principalTable: "Fses_category_partitions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Received_specialties",
                schema: "Edu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fses_category_partition_id = table.Column<int>(type: "int", nullable: false),
                    Qualification = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Received_specialties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Received_specialties_Fses_category_partitions",
                        column: x => x.Fses_category_partition_id,
                        principalSchema: "Edu",
                        principalTable: "Fses_category_partitions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "Edu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Middlename = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Post_id = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Posts",
                        column: x => x.Post_id,
                        principalSchema: "Edu",
                        principalTable: "Posts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "Identity",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "Identity",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                schema: "Identity",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Discipline_semesters",
                schema: "Edu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Semester_number = table.Column<byte>(type: "tinyint", nullable: false),
                    Weeks_count = table.Column<byte>(type: "tinyint", nullable: true),
                    Discipline_id = table.Column<int>(type: "int", nullable: false),
                    Theory_lesson_hours = table.Column<short>(type: "smallint", nullable: false),
                    Practice_work_hours = table.Column<short>(type: "smallint", nullable: false),
                    Laboratory_work_hours = table.Column<short>(type: "smallint", nullable: false),
                    Control_work_hours = table.Column<short>(type: "smallint", nullable: false),
                    Independent_work_hours = table.Column<short>(type: "smallint", nullable: false),
                    Consultation_hours = table.Column<short>(type: "smallint", nullable: false),
                    Exam_hours = table.Column<short>(type: "smallint", nullable: false),
                    Educational_practice_hours = table.Column<short>(type: "smallint", nullable: false),
                    Production_practice_hours = table.Column<short>(type: "smallint", nullable: false),
                    Certification_form_id = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discipline_semesters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discipline_Semesters_Disciplines1",
                        column: x => x.Discipline_id,
                        principalSchema: "Edu",
                        principalTable: "Disciplines",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Discipline_semesters_Intermediate_certification_forms",
                        column: x => x.Certification_form_id,
                        principalSchema: "Edu",
                        principalTable: "Intermediate_certification_forms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Received_educations",
                schema: "Edu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Received_specialty_id = table.Column<int>(type: "int", nullable: false),
                    Received_education_form_id = table.Column<int>(type: "int", nullable: false),
                    Education_level_id = table.Column<int>(type: "int", nullable: false),
                    Study_period_months = table.Column<short>(type: "smallint", nullable: false),
                    Is_budget = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Received_educations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Received_educations_Education_levels",
                        column: x => x.Education_level_id,
                        principalSchema: "Edu",
                        principalTable: "Education_levels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Received_educations_Received_education_forms",
                        column: x => x.Received_education_form_id,
                        principalSchema: "Edu",
                        principalTable: "Received_education_forms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Received_educations_Received_specialties",
                        column: x => x.Received_specialty_id,
                        principalSchema: "Edu",
                        principalTable: "Received_specialties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Audiences",
                schema: "Edu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Employee_head_id = table.Column<int>(type: "int", nullable: true),
                    Audience_type_id = table.Column<int>(type: "int", nullable: true),
                    Capacity = table.Column<short>(type: "smallint", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Audiences_Audience_types",
                        column: x => x.Audience_type_id,
                        principalSchema: "Edu",
                        principalTable: "Audience_types",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Audiences_Employees",
                        column: x => x.Employee_head_id,
                        principalSchema: "Edu",
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Employee_cathedras",
                schema: "Edu",
                columns: table => new
                {
                    Employee_id = table.Column<int>(type: "int", nullable: false),
                    Cathedra_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee_cathedras", x => new { x.Employee_id, x.Cathedra_id });
                    table.ForeignKey(
                        name: "FK_Employee_cathedras_Cathedras",
                        column: x => x.Cathedra_id,
                        principalSchema: "Edu",
                        principalTable: "Cathedras",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employee_cathedras_Employees",
                        column: x => x.Employee_id,
                        principalSchema: "Edu",
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Education_plan_semester_disciplines",
                schema: "Edu",
                columns: table => new
                {
                    Education_plan_id = table.Column<int>(type: "int", nullable: false),
                    Discipline_semester_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Education_plan_semester_disciplines", x => new { x.Education_plan_id, x.Discipline_semester_id });
                    table.ForeignKey(
                        name: "FK_Education_plan_semester_disciplines_Education_plans",
                        column: x => x.Education_plan_id,
                        principalSchema: "Edu",
                        principalTable: "Education_plans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Education_plan_semester_disciplines_Semester_disciplines",
                        column: x => x.Discipline_semester_id,
                        principalSchema: "Edu",
                        principalTable: "Discipline_semesters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Student_groups",
                schema: "Edu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Course_number = table.Column<byte>(type: "tinyint", nullable: false),
                    Curator_id = table.Column<int>(type: "int", nullable: true),
                    Received_education_id = table.Column<int>(type: "int", nullable: false),
                    Education_plan_id = table.Column<int>(type: "int", nullable: true),
                    Receipt_year = table.Column<short>(type: "smallint", nullable: false),
                    Student_quantity = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student_groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Student_groups_Education_plans",
                        column: x => x.Education_plan_id,
                        principalSchema: "Edu",
                        principalTable: "Education_plans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Student_groups_Employees",
                        column: x => x.Curator_id,
                        principalSchema: "Edu",
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Student_groups_Received_educations",
                        column: x => x.Received_education_id,
                        principalSchema: "Edu",
                        principalTable: "Received_educations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Fixed_disciplines",
                schema: "Edu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fixing_employee_id = table.Column<int>(type: "int", nullable: false),
                    Discipline_semester_id = table.Column<int>(type: "int", nullable: false),
                    Student_group_id = table.Column<int>(type: "int", nullable: false),
                    Fixed_discipline_status_id = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fixed_disciplines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fixed_disciplines_Employees",
                        column: x => x.Fixing_employee_id,
                        principalSchema: "Edu",
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Fixed_disciplines_Fixed_discipline_statuses",
                        column: x => x.Fixed_discipline_status_id,
                        principalSchema: "Edu",
                        principalTable: "Fixed_discipline_statuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Fixed_disciplines_Semester_disciplines",
                        column: x => x.Discipline_semester_id,
                        principalSchema: "Edu",
                        principalTable: "Discipline_semesters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Fixed_disciplines_Stuent_groups",
                        column: x => x.Student_group_id,
                        principalSchema: "Edu",
                        principalTable: "Student_groups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Student_group_name_changes",
                schema: "Edu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Student_group_id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student_group_name_changes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Student_group_name_changes_Student_groups",
                        column: x => x.Student_group_id,
                        principalSchema: "Edu",
                        principalTable: "Student_groups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Discipline_schedules",
                schema: "Edu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fixed_discipline_id = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Pair_number = table.Column<int>(type: "int", nullable: false),
                    Audience_id = table.Column<int>(type: "int", nullable: true),
                    Is_even_pair = table.Column<bool>(type: "bit", nullable: true),
                    Is_first_subgroup = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discipline_schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discipline_schedules_Audiences",
                        column: x => x.Audience_id,
                        principalSchema: "Edu",
                        principalTable: "Audiences",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Discipline_schedules_Fixed_disciplines",
                        column: x => x.Fixed_discipline_id,
                        principalSchema: "Edu",
                        principalTable: "Fixed_disciplines",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Discipline_schedule_replacements",
                schema: "Edu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Discipline_schedule_id = table.Column<int>(type: "int", nullable: true),
                    Fixed_discipline_id = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Pair_number = table.Column<int>(type: "int", nullable: false),
                    Audience_id = table.Column<int>(type: "int", nullable: true),
                    Is_first_subgroup = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discipline_schedule_replacements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discipline_schedule_replacement_Audiences",
                        column: x => x.Audience_id,
                        principalSchema: "Edu",
                        principalTable: "Audiences",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Discipline_schedule_replacement_Discipline_schedules",
                        column: x => x.Discipline_schedule_id,
                        principalSchema: "Edu",
                        principalTable: "Discipline_schedules",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Discipline_schedule_replacement_Fixed_disciplines",
                        column: x => x.Fixed_discipline_id,
                        principalSchema: "Edu",
                        principalTable: "Fixed_disciplines",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Audiences_Audience_type_id",
                schema: "Edu",
                table: "Audiences",
                column: "Audience_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_Audiences_Employee_head_id",
                schema: "Edu",
                table: "Audiences",
                column: "Employee_head_id");

            migrationBuilder.CreateIndex(
                name: "IX_Cathedra_specialties_Specialtie_id",
                schema: "Edu",
                table: "Cathedra_specialties",
                column: "Fses_category_partition_id");

            migrationBuilder.CreateIndex(
                name: "IX_Discipline_schedule_replacement_Audience_id",
                schema: "Edu",
                table: "Discipline_schedule_replacements",
                column: "Audience_id");

            migrationBuilder.CreateIndex(
                name: "IX_Discipline_schedule_replacement_Discipline_schedule_id",
                schema: "Edu",
                table: "Discipline_schedule_replacements",
                column: "Discipline_schedule_id");

            migrationBuilder.CreateIndex(
                name: "IX_Discipline_schedule_replacement_Fixed_discipline_id",
                schema: "Edu",
                table: "Discipline_schedule_replacements",
                column: "Fixed_discipline_id");

            migrationBuilder.CreateIndex(
                name: "IX_Discipline_schedules_Audience_id",
                schema: "Edu",
                table: "Discipline_schedules",
                column: "Audience_id");

            migrationBuilder.CreateIndex(
                name: "IX_Discipline_schedules_Fixed_discipline_id",
                schema: "Edu",
                table: "Discipline_schedules",
                column: "Fixed_discipline_id");

            migrationBuilder.CreateIndex(
                name: "IX_Discipline_semesters_Certification_form_id",
                schema: "Edu",
                table: "Discipline_semesters",
                column: "Certification_form_id");

            migrationBuilder.CreateIndex(
                name: "IX_Discipline_semesters_Discipline_id",
                schema: "Edu",
                table: "Discipline_semesters",
                column: "Discipline_id");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_Cathedra_id",
                schema: "Edu",
                table: "Disciplines",
                column: "Cathedra_id");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_Education_cycle_id",
                schema: "Edu",
                table: "Disciplines",
                column: "Education_cycle_id");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_Education_module_id",
                schema: "Edu",
                table: "Disciplines",
                column: "Education_module_id");

            migrationBuilder.CreateIndex(
                name: "IX_Education_plan_semester_disciplines_Discipline_semester_id",
                schema: "Edu",
                table: "Education_plan_semester_disciplines",
                column: "Discipline_semester_id");

            migrationBuilder.CreateIndex(
                name: "IX_Education_plans_Specialtie_id",
                schema: "Edu",
                table: "Education_plans",
                column: "Fses_category_partition_id");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_cathedras_Cathedra_id",
                schema: "Edu",
                table: "Employee_cathedras",
                column: "Cathedra_id");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Post_id",
                schema: "Edu",
                table: "Employees",
                column: "Post_id");

            migrationBuilder.CreateIndex(
                name: "IX_Fixed_disciplines_Discipline_semester_id",
                schema: "Edu",
                table: "Fixed_disciplines",
                column: "Discipline_semester_id");

            migrationBuilder.CreateIndex(
                name: "IX_Fixed_disciplines_Employee_id",
                schema: "Edu",
                table: "Fixed_disciplines",
                column: "Fixing_employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_Fixed_disciplines_Fixed_discipline_status_id",
                schema: "Edu",
                table: "Fixed_disciplines",
                column: "Fixed_discipline_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_Fixed_disciplines_Student_group_id",
                schema: "Edu",
                table: "Fixed_disciplines",
                column: "Student_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                schema: "Catalog",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Received_education_forms_Education_form_id",
                schema: "Edu",
                table: "Received_education_forms",
                column: "Education_form_id");

            migrationBuilder.CreateIndex(
                name: "IX_Received_educations_Education_level_id",
                schema: "Edu",
                table: "Received_educations",
                column: "Education_level_id");

            migrationBuilder.CreateIndex(
                name: "IX_Received_educations_Received_education_form_id",
                schema: "Edu",
                table: "Received_educations",
                column: "Received_education_form_id");

            migrationBuilder.CreateIndex(
                name: "IX_Received_educations_Received_specialty_id",
                schema: "Edu",
                table: "Received_educations",
                column: "Received_specialty_id");

            migrationBuilder.CreateIndex(
                name: "IX_Received_specialties_Specialtie_id",
                schema: "Edu",
                table: "Received_specialties",
                column: "Fses_category_partition_id");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "Identity",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Identity",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Student_group_name_changes_Student_group_id",
                schema: "Edu",
                table: "Student_group_name_changes",
                column: "Student_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Curator_id",
                schema: "Edu",
                table: "Student_groups",
                column: "Curator_id");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Education_plan_id",
                schema: "Edu",
                table: "Student_groups",
                column: "Education_plan_id");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Received_education_id",
                schema: "Edu",
                table: "Student_groups",
                column: "Received_education_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "Identity",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                schema: "Identity",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "Identity",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Identity",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Identity",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditTrails",
                schema: "Auditing");

            migrationBuilder.DropTable(
                name: "Cathedra_specialties",
                schema: "Edu");

            migrationBuilder.DropTable(
                name: "Discipline_schedule_replacements",
                schema: "Edu");

            migrationBuilder.DropTable(
                name: "Education_plan_semester_disciplines",
                schema: "Edu");

            migrationBuilder.DropTable(
                name: "Employee_cathedras",
                schema: "Edu");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Student_group_name_changes",
                schema: "Edu");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserTokens",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Discipline_schedules",
                schema: "Edu");

            migrationBuilder.DropTable(
                name: "Brands",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Audiences",
                schema: "Edu");

            migrationBuilder.DropTable(
                name: "Fixed_disciplines",
                schema: "Edu");

            migrationBuilder.DropTable(
                name: "Audience_types",
                schema: "Edu");

            migrationBuilder.DropTable(
                name: "Fixed_discipline_statuses",
                schema: "Edu");

            migrationBuilder.DropTable(
                name: "Discipline_semesters",
                schema: "Edu");

            migrationBuilder.DropTable(
                name: "Student_groups",
                schema: "Edu");

            migrationBuilder.DropTable(
                name: "Disciplines",
                schema: "Edu");

            migrationBuilder.DropTable(
                name: "Intermediate_certification_forms",
                schema: "Edu");

            migrationBuilder.DropTable(
                name: "Education_plans",
                schema: "Edu");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "Edu");

            migrationBuilder.DropTable(
                name: "Received_educations",
                schema: "Edu");

            migrationBuilder.DropTable(
                name: "Cathedras",
                schema: "Edu");

            migrationBuilder.DropTable(
                name: "Education_cycles",
                schema: "Edu");

            migrationBuilder.DropTable(
                name: "Education_modules",
                schema: "Edu");

            migrationBuilder.DropTable(
                name: "Posts",
                schema: "Edu");

            migrationBuilder.DropTable(
                name: "Education_levels",
                schema: "Edu");

            migrationBuilder.DropTable(
                name: "Received_education_forms",
                schema: "Edu");

            migrationBuilder.DropTable(
                name: "Received_specialties",
                schema: "Edu");

            migrationBuilder.DropTable(
                name: "Education_forms",
                schema: "Edu");

            migrationBuilder.DropTable(
                name: "Fses_category_partitions",
                schema: "Edu");
        }
    }
}
