using Edu.WebApi.Application.Common.Events;
using Edu.WebApi.Application.Common.Interfaces;
using Edu.WebApi.Domain.Catalog;
using Edu.WebApi.Domain.EducationProcess;
using Edu.WebApi.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Edu.WebApi.Infrastructure.Persistence.Context;

public class ApplicationDbContext : BaseDbContext
{
    public ApplicationDbContext(/*,*/ DbContextOptions options, ICurrentUser currentUser, ISerializerService serializer, IOptions<DatabaseSettings> dbSettings, IEventPublisher events)
        : base(/*currentTenant,*/ options, currentUser, serializer, dbSettings, events)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Brand> Brands => Set<Brand>();

    public virtual DbSet<Audience> Audiences { get; set; } = null!;
    public virtual DbSet<AudienceType> AudienceTypes { get; set; } = null!;
    public virtual DbSet<Cathedra> Cathedras { get; set; } = null!;
    public virtual DbSet<Discipline> Disciplines { get; set; } = null!;
    public virtual DbSet<DisciplineSchedule> DisciplineSchedules { get; set; } = null!;
    public virtual DbSet<DisciplineScheduleReplacement> DisciplineScheduleReplacements { get; set; } = null!;
    public virtual DbSet<DisciplineSemester> DisciplineSemesters { get; set; } = null!;
    public virtual DbSet<EducationCycle> EducationCycles { get; set; } = null!;
    public virtual DbSet<EducationForm> EducationForms { get; set; } = null!;
    public virtual DbSet<EducationLevel> EducationLevels { get; set; } = null!;
    public virtual DbSet<EducationModule> EducationModules { get; set; } = null!;
    public virtual DbSet<EducationPlan> EducationPlans { get; set; } = null!;
    public virtual DbSet<Employee> Employees { get; set; } = null!;
    public virtual DbSet<FixedDiscipline> FixedDisciplines { get; set; } = null!;
    public virtual DbSet<FixedDisciplineStatus> FixedDisciplineStatuses { get; set; } = null!;
    public virtual DbSet<FsesCategoryPartition> FsesCategoryPartitions { get; set; } = null!;
    public virtual DbSet<IntermediateCertificationForm> IntermediateCertificationForms { get; set; } = null!;
    public virtual DbSet<Post> Posts { get; set; } = null!;
    public virtual DbSet<ReceivedEducation> ReceivedEducations { get; set; } = null!;
    public virtual DbSet<ReceivedEducationForm> ReceivedEducationForms { get; set; } = null!;
    public virtual DbSet<ReceivedSpecialty> ReceivedSpecialties { get; set; } = null!;
    public virtual DbSet<StudentGroup> StudentGroups { get; set; } = null!;
    public virtual DbSet<StudentGroupNameChange> StudentGroupNameChanges { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Audience>(entity =>
        {
            entity.HasIndex(e => e.AudienceTypeId, "IX_Audiences_Audience_type_id");

            entity.HasIndex(e => e.EmployeeHeadId, "IX_Audiences_Employee_head_id");

            entity.Property(e => e.AudienceTypeId).HasColumnName("Audience_type_id");

            entity.Property(e => e.EmployeeHeadId).HasColumnName("Employee_head_id");

            entity.Property(e => e.Number).HasMaxLength(5);

            entity.HasOne(d => d.AudienceType)
                .WithMany(p => p.Audiences)
                .HasForeignKey(d => d.AudienceTypeId)
                .HasConstraintName("FK_Audiences_Audience_types");

            entity.HasOne(d => d.EmployeeHead)
                .WithMany(p => p.Audiences)
                .HasForeignKey(d => d.EmployeeHeadId)
                .HasConstraintName("FK_Audiences_Employees");
        });

        modelBuilder.Entity<AudienceType>(entity =>
        {
            entity.ToTable("Audience_types");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Cathedra>(entity =>
        {
            entity.Property(e => e.Description).HasMaxLength(300);

            entity.Property(e => e.Name).HasMaxLength(75);

            entity.Property(e => e.NameAbbreviation)
                .HasMaxLength(10)
                .HasColumnName("Name_abbreviation");

            entity.HasMany(d => d.FsesCategoryPartitions)
                .WithMany(p => p.Cathedras)
                .UsingEntity<Dictionary<string, object>>(
                    "CathedraSpecialty",
                    l => l.HasOne<FsesCategoryPartition>().WithMany().HasForeignKey("FsesCategoryPartitionId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Cathedra_specialties_Fses_category_partitions"),
                    r => r.HasOne<Cathedra>().WithMany().HasForeignKey("CathedraId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Cathedra_specialties_Cathedras"),
                    j =>
                    {
                        j.HasKey("CathedraId", "FsesCategoryPartitionId");

                        j.ToTable("Cathedra_specialties");

                        j.HasIndex(new[] { "FsesCategoryPartitionId" }, "IX_Cathedra_specialties_Specialtie_id");

                        j.IndexerProperty<int>("CathedraId").HasColumnName("Cathedra_id");

                        j.IndexerProperty<int>("FsesCategoryPartitionId").HasColumnName("Fses_category_partition_id");
                    });
        });

        modelBuilder.Entity<Discipline>(entity =>
        {
            entity.HasIndex(e => e.CathedraId, "IX_Disciplines_Cathedra_id");

            entity.HasIndex(e => e.EducationCycleId, "IX_Disciplines_Education_cycle_id");

            entity.Property(e => e.CathedraId).HasColumnName("Cathedra_id");

            entity.Property(e => e.Description).HasMaxLength(500);

            entity.Property(e => e.DisciplineIndex)
                .HasMaxLength(10)
                .HasColumnName("Discipline_index");

            entity.Property(e => e.EducationCycleId).HasColumnName("Education_cycle_id");

            entity.Property(e => e.EducationModuleId).HasColumnName("Education_module_id");

            entity.Property(e => e.Name).HasMaxLength(125);

            entity.HasOne(d => d.Cathedra)
                .WithMany(p => p.Disciplines)
                .HasForeignKey(d => d.CathedraId)
                .HasConstraintName("FK_Disciplines_Cathedras");

            entity.HasOne(d => d.EducationCycle)
                .WithMany(p => p.Disciplines)
                .HasForeignKey(d => d.EducationCycleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Disciplines_Education_cycles_and_modules");

            entity.HasOne(d => d.EducationModule)
                .WithMany(p => p.Disciplines)
                .HasForeignKey(d => d.EducationModuleId)
                .HasConstraintName("FK_Disciplines_Education_modules");
        });

        modelBuilder.Entity<DisciplineSchedule>(entity =>
        {
            entity.ToTable("Discipline_schedules");

            entity.HasIndex(e => e.AudienceId, "IX_Discipline_schedules_Audience_id");

            entity.HasIndex(e => e.FixedDisciplineId, "IX_Discipline_schedules_Fixed_discipline_id");

            entity.Property(e => e.AudienceId).HasColumnName("Audience_id");

            entity.Property(e => e.Date).HasColumnType("date");

            entity.Property(e => e.FixedDisciplineId).HasColumnName("Fixed_discipline_id");

            entity.Property(e => e.IsEvenPair).HasColumnName("Is_even_pair");

            entity.Property(e => e.IsFirstSubgroup).HasColumnName("Is_first_subgroup");

            entity.Property(e => e.PairNumber).HasColumnName("Pair_number");

            entity.HasOne(d => d.Audience)
                .WithMany(p => p.DisciplineSchedules)
                .HasForeignKey(d => d.AudienceId)
                .HasConstraintName("FK_Discipline_schedules_Audiences");

            entity.HasOne(d => d.FixedDiscipline)
                .WithMany(p => p.DisciplineSchedules)
                .HasForeignKey(d => d.FixedDisciplineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Discipline_schedules_Fixed_disciplines");
        });

        modelBuilder.Entity<DisciplineScheduleReplacement>(entity =>
        {
            entity.ToTable("Discipline_schedule_replacements");

            entity.HasIndex(e => e.AudienceId, "IX_Discipline_schedule_replacement_Audience_id");

            entity.HasIndex(e => e.DisciplineScheduleId, "IX_Discipline_schedule_replacement_Discipline_schedule_id");

            entity.HasIndex(e => e.FixedDisciplineId, "IX_Discipline_schedule_replacement_Fixed_discipline_id");

            entity.Property(e => e.AudienceId).HasColumnName("Audience_id");

            entity.Property(e => e.Date).HasColumnType("date");

            entity.Property(e => e.DisciplineScheduleId).HasColumnName("Discipline_schedule_id");

            entity.Property(e => e.FixedDisciplineId).HasColumnName("Fixed_discipline_id");

            entity.Property(e => e.IsFirstSubgroup).HasColumnName("Is_first_subgroup");

            entity.Property(e => e.PairNumber).HasColumnName("Pair_number");

            entity.HasOne(d => d.Audience)
                .WithMany(p => p.DisciplineScheduleReplacements)
                .HasForeignKey(d => d.AudienceId)
                .HasConstraintName("FK_Discipline_schedule_replacement_Audiences");

            entity.HasOne(d => d.DisciplineSchedule)
                .WithMany(p => p.DisciplineScheduleReplacements)
                .HasForeignKey(d => d.DisciplineScheduleId)
                .HasConstraintName("FK_Discipline_schedule_replacement_Discipline_schedules");

            entity.HasOne(d => d.FixedDiscipline)
                .WithMany(p => p.DisciplineScheduleReplacements)
                .HasForeignKey(d => d.FixedDisciplineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Discipline_schedule_replacement_Fixed_disciplines");
        });

        modelBuilder.Entity<DisciplineSemester>(entity =>
        {
            entity.ToTable("Discipline_semesters");

            entity.HasIndex(e => e.CertificationFormId, "IX_Discipline_semesters_Certification_form_id");

            entity.HasIndex(e => e.DisciplineId, "IX_Discipline_semesters_Discipline_id");

            entity.Property(e => e.CertificationFormId).HasColumnName("Certification_form_id");

            entity.Property(e => e.ConsultationHours).HasColumnName("Consultation_hours");

            entity.Property(e => e.ControlWorkHours).HasColumnName("Control_work_hours");

            entity.Property(e => e.Description).HasMaxLength(300);

            entity.Property(e => e.DisciplineId).HasColumnName("Discipline_id");

            entity.Property(e => e.EducationalPracticeHours).HasColumnName("Educational_practice_hours");

            entity.Property(e => e.ExamHours).HasColumnName("Exam_hours");

            entity.Property(e => e.IndependentWorkHours).HasColumnName("Independent_work_hours");

            entity.Property(e => e.LaboratoryWorkHours).HasColumnName("Laboratory_work_hours");

            entity.Property(e => e.PracticeWorkHours).HasColumnName("Practice_work_hours");

            entity.Property(e => e.ProductionPracticeHours).HasColumnName("Production_practice_hours");

            entity.Property(e => e.SemesterNumber).HasColumnName("Semester_number");

            entity.Property(e => e.TheoryLessonHours).HasColumnName("Theory_lesson_hours");

            entity.Property(e => e.WeeksCount).HasColumnName("Weeks_count");

            entity.HasOne(d => d.CertificationForm)
                .WithMany(p => p.DisciplineSemesters)
                .HasForeignKey(d => d.CertificationFormId)
                .HasConstraintName("FK_Discipline_semesters_Intermediate_certification_forms");

            entity.HasOne(d => d.Discipline)
                .WithMany(p => p.DisciplineSemesters)
                .HasForeignKey(d => d.DisciplineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Discipline_Semesters_Disciplines1");
        });

        modelBuilder.Entity<EducationCycle>(entity =>
        {
            entity.ToTable("Education_cycles");

            entity.Property(e => e.Description).HasMaxLength(300);

            entity.Property(e => e.EducationCycleIndex)
                .HasMaxLength(10)
                .HasColumnName("Education_cycle_index");

            entity.Property(e => e.Name).HasMaxLength(120);
        });

        modelBuilder.Entity<EducationForm>(entity =>
        {
            entity.ToTable("Education_forms");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<EducationLevel>(entity =>
        {
            entity.ToTable("Education_levels");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<EducationModule>(entity =>
        {
            entity.ToTable("Education_modules");

            entity.Property(e => e.Description).HasMaxLength(300);

            entity.Property(e => e.EducationModuleIndex)
                .HasMaxLength(10)
                .HasColumnName("Education_module_index");

            entity.Property(e => e.Name).HasMaxLength(65);
        });

        modelBuilder.Entity<EducationPlan>(entity =>
        {
            entity.ToTable("Education_plans");

            entity.HasIndex(e => e.FsesCategoryPartitionId, "IX_Education_plans_Specialtie_id");

            entity.Property(e => e.BeginingYear).HasColumnName("Begining_year");

            entity.Property(e => e.Description).HasMaxLength(300);

            entity.Property(e => e.EndingYear).HasColumnName("Ending_year");

            entity.Property(e => e.FsesCategoryPartitionId).HasColumnName("Fses_category_partition_id");

            entity.Property(e => e.Name).HasMaxLength(65);

            entity.HasOne(d => d.FsesCategoryPartition)
                .WithMany(p => p.EducationPlans)
                .HasForeignKey(d => d.FsesCategoryPartitionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Education_plans_Fses_category_partitions");

            entity.HasMany(d => d.SemesterDisciplines)
                .WithMany(p => p.EducationPlans)
                .UsingEntity<Dictionary<string, object>>(
                    "EducationPlanSemesterDiscipline",
                    l => l.HasOne<DisciplineSemester>().WithMany().HasForeignKey("DisciplineSemesterId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Education_plan_semester_disciplines_Semester_disciplines"),
                    r => r.HasOne<EducationPlan>().WithMany().HasForeignKey("EducationPlanId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Education_plan_semester_disciplines_Education_plans"),
                    j =>
                    {
                        j.HasKey("EducationPlanId", "DisciplineSemesterId");

                        j.ToTable("Education_plan_semester_disciplines");

                        j.HasIndex(new[] { "DisciplineSemesterId" }, "IX_Education_plan_semester_disciplines_Discipline_semester_id");

                        j.IndexerProperty<int>("EducationPlanId").HasColumnName("Education_plan_id");

                        j.IndexerProperty<int>("DisciplineSemesterId").HasColumnName("Discipline_semester_id");
                    });
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasIndex(e => e.PostId, "IX_Employees_Post_id");

            entity.Property(e => e.Firstname).HasMaxLength(100);

            entity.Property(e => e.Lastname).HasMaxLength(100);

            entity.Property(e => e.Middlename).HasMaxLength(100);

            entity.Property(e => e.PostId).HasColumnName("Post_id");

            entity.HasOne(d => d.Post)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employees_Posts");

            entity.HasMany(d => d.Cathedras)
                .WithMany(p => p.Employees)
                .UsingEntity<Dictionary<string, object>>(
                    "EmployeeCathedra",
                    l => l.HasOne<Cathedra>().WithMany().HasForeignKey("CathedraId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Employee_cathedras_Cathedras"),
                    r => r.HasOne<Employee>().WithMany().HasForeignKey("EmployeeId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Employee_cathedras_Employees"),
                    j =>
                    {
                        j.HasKey("EmployeeId", "CathedraId");

                        j.ToTable("Employee_cathedras");

                        j.HasIndex(new[] { "CathedraId" }, "IX_Employee_cathedras_Cathedra_id");

                        j.IndexerProperty<int>("EmployeeId").HasColumnName("Employee_id");

                        j.IndexerProperty<int>("CathedraId").HasColumnName("Cathedra_id");
                    });
        });

        modelBuilder.Entity<FixedDiscipline>(entity =>
        {
            entity.ToTable("Fixed_disciplines");

            entity.HasIndex(e => e.FixingEmployeeId, "IX_Fixed_disciplines_Employee_id");

            entity.HasIndex(e => e.StudentGroupId, "IX_Fixed_disciplines_Student_group_id");

            entity.HasIndex(e => e.DisciplineSemesterId, "IX_Fixed_disciplines_Discipline_semester_id");

            entity.Property(e => e.FixedDisciplineStatusId).HasColumnName("Fixed_discipline_status_id");

            entity.Property(e => e.FixingEmployeeId).HasColumnName("Fixing_employee_id");

            entity.Property(e => e.StudentGroupId).HasColumnName("Student_group_id");

            entity.Property(e => e.DisciplineSemesterId).HasColumnName("Discipline_semester_id");

            entity.HasOne(d => d.FixedDisciplineStatus)
                .WithMany(p => p.FixedDisciplines)
                .HasForeignKey(d => d.FixedDisciplineStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fixed_disciplines_Fixed_discipline_statuses");

            entity.HasOne(d => d.FixingEmployee)
                .WithMany(p => p.FixedDisciplines)
                .HasForeignKey(d => d.FixingEmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fixed_disciplines_Employees");

            entity.HasOne(d => d.Group)
                .WithMany(p => p.FixedDisciplines)
                .HasForeignKey(d => d.StudentGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fixed_disciplines_Stuent_groups");

            entity.HasOne(d => d.SemesterDiscipline)
                .WithMany(p => p.FixedDisciplines)
                .HasForeignKey(d => d.DisciplineSemesterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fixed_disciplines_Semester_disciplines");
        });

        modelBuilder.Entity<FixedDisciplineStatus>(entity =>
        {
            entity.ToTable("Fixed_discipline_statuses");

            entity.Property(e => e.Description).HasMaxLength(300);

            entity.Property(e => e.Name).HasMaxLength(65);
        });

        modelBuilder.Entity<FsesCategoryPartition>(entity =>
        {
            entity.ToTable("Fses_category_partitions");

            entity.Property(e => e.FirstPartNumber).HasColumnName("First_part_number");

            entity.Property(e => e.Name).HasMaxLength(150);

            entity.Property(e => e.NameAbbreviation)
                .HasMaxLength(10)
                .HasColumnName("Name_abbreviation")
                .IsFixedLength();

            entity.Property(e => e.SecondPartNumber).HasColumnName("Second_part_number");

            entity.Property(e => e.ThirdPathNumber).HasColumnName("Third_path_number");
        });

        modelBuilder.Entity<IntermediateCertificationForm>(entity =>
        {
            entity.ToTable("Intermediate_certification_forms");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(75);
        });

        modelBuilder.Entity<ReceivedEducation>(entity =>
        {
            entity.ToTable("Received_educations");

            entity.HasIndex(e => e.EducationLevelId, "IX_Received_educations_Education_level_id");

            entity.HasIndex(e => e.ReceivedEducationFormId, "IX_Received_educations_Received_education_form_id");

            entity.HasIndex(e => e.ReceivedSpecialtyId, "IX_Received_educations_Received_specialty_id");

            entity.Property(e => e.EducationLevelId).HasColumnName("Education_level_id");

            entity.Property(e => e.IsBudget).HasColumnName("Is_budget");

            entity.Property(e => e.ReceivedEducationFormId).HasColumnName("Received_education_form_id");

            entity.Property(e => e.ReceivedSpecialtyId).HasColumnName("Received_specialty_id");

            entity.Property(e => e.StudyPeriodMonths).HasColumnName("Study_period_months");

            entity.HasOne(d => d.EducationLevel)
                .WithMany(p => p.ReceivedEducations)
                .HasForeignKey(d => d.EducationLevelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Received_educations_Education_levels");

            entity.HasOne(d => d.ReceivedEducationForm)
                .WithMany(p => p.ReceivedEducations)
                .HasForeignKey(d => d.ReceivedEducationFormId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Received_educations_Received_education_forms");

            entity.HasOne(d => d.ReceivedSpecialty)
                .WithMany(p => p.ReceivedEducations)
                .HasForeignKey(d => d.ReceivedSpecialtyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Received_educations_Received_specialties");
        });

        modelBuilder.Entity<ReceivedEducationForm>(entity =>
        {
            entity.ToTable("Received_education_forms");

            entity.HasIndex(e => e.EducationFormId, "IX_Received_education_forms_Education_form_id");

            entity.Property(e => e.AdditionalInfo)
                .HasMaxLength(65)
                .HasColumnName("Additional_info");

            entity.Property(e => e.EducationFormId).HasColumnName("Education_form_id");

            entity.HasOne(d => d.EducationForm)
                .WithMany(p => p.ReceivedEducationForms)
                .HasForeignKey(d => d.EducationFormId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Received_education_forms_Education_forms");
        });

        modelBuilder.Entity<ReceivedSpecialty>(entity =>
        {
            entity.ToTable("Received_specialties");

            entity.HasIndex(e => e.FsesCategoryPartitionId, "IX_Received_specialties_Specialtie_id");

            entity.Property(e => e.FsesCategoryPartitionId).HasColumnName("Fses_category_partition_id");

            entity.Property(e => e.Qualification).HasMaxLength(100);

            entity.HasOne(d => d.FsesCategoryPartition)
                .WithMany(p => p.ReceivedSpecialties)
                .HasForeignKey(d => d.FsesCategoryPartitionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Received_specialties_Fses_category_partitions");
        });

        modelBuilder.Entity<StudentGroup>(entity =>
        {
            entity.ToTable("Student_groups");

            entity.HasIndex(e => e.CuratorId, "IX_Groups_Curator_id");

            entity.HasIndex(e => e.EducationPlanId, "IX_Groups_Education_plan_id");

            entity.HasIndex(e => e.ReceivedEducationId, "IX_Groups_Received_education_id");

            entity.Property(e => e.CourseNumber).HasColumnName("Course_number");

            entity.Property(e => e.CuratorId).HasColumnName("Curator_id");

            entity.Property(e => e.EducationPlanId).HasColumnName("Education_plan_id");

            entity.Property(e => e.Name).HasMaxLength(10);

            entity.Property(e => e.ReceiptYear).HasColumnName("Receipt_year");

            entity.Property(e => e.ReceivedEducationId).HasColumnName("Received_education_id");

            entity.Property(e => e.StudentQuantity).HasColumnName("Student_quantity");

            entity.HasOne(d => d.Curator)
                .WithMany(p => p.StudentGroups)
                .HasForeignKey(d => d.CuratorId)
                .HasConstraintName("FK_Student_groups_Employees");

            entity.HasOne(d => d.EducationPlan)
                .WithMany(p => p.StudentGroups)
                .HasForeignKey(d => d.EducationPlanId)
                .HasConstraintName("FK_Student_groups_Education_plans");

            entity.HasOne(d => d.ReceivedEducation)
                .WithMany(p => p.StudentGroups)
                .HasForeignKey(d => d.ReceivedEducationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_groups_Received_educations");
        });

        modelBuilder.Entity<StudentGroupNameChange>(entity =>
        {
            entity.ToTable("Student_group_name_changes");

            entity.Property(e => e.Date).HasColumnType("date");

            entity.Property(e => e.Name).HasMaxLength(10);

            entity.Property(e => e.StudentGroupId).HasColumnName("Student_group_id");

            entity.HasOne(d => d.StudentGroup)
                .WithMany(p => p.StudentGroupNameChanges)
                .HasForeignKey(d => d.StudentGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_group_name_changes_Student_groups");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.ToTable(nameof(Brands), SchemaNames.Catalog);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable(nameof(Products), SchemaNames.Catalog);
        });

        foreach (var property in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(t => t.GetProperties())
                     .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetColumnType("decimal(18,2)");
        }

        foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.Name is "LastModifiedBy" or "CreatedBy"))
        {
            property.SetColumnType("nvarchar(128)");
        }

        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(SchemaNames.Edu);
    }
}