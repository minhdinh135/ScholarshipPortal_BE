using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data;

public class ScholarshipContext : DbContext
{
    public ScholarshipContext()
    {
    }

    public ScholarshipContext(DbContextOptions<ScholarshipContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<ApplicantProfile> ApplicantProfiles { get; set; }

    public virtual DbSet<Achievement> Achievements { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Domain.Entities.Application> Applications { get; set; }

    public virtual DbSet<ScholarshipProgram> ScholarshipPrograms { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<Award> Awards { get; set; }

    public virtual DbSet<Criteria> Criteria { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<University> Universities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Major> Majors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", true, true)
            .Build();

        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySQL(configuration.GetConnectionString("Db") ?? string.Empty);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure table names
        modelBuilder.Entity<ApplicantProfile>()
            .ToTable("applicant_profiles");

        modelBuilder.Entity<ScholarshipProgram>()
            .ToTable("scholarship_programs");

        // Configure relationships
        modelBuilder.Entity<Account>()
            .HasOne(account => account.Role)
            .WithMany(role => role.Accounts)
            .HasForeignKey(account => account.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Account>()
            .HasOne(account => account.ApplicantProfile)
            .WithOne(applicantProfile => applicantProfile.Applicant)
            .HasForeignKey<ApplicantProfile>(applicantProfile => applicantProfile.ApplicantId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Achievement>()
            .HasOne(achievement => achievement.ApplicantProfile)
            .WithMany(applicantProfile => applicantProfile.Achievements)
            .HasForeignKey(achievement => achievement.ApplicantProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Domain.Entities.Application>()
            .HasOne(application => application.Applicant)
            .WithMany(applicant => applicant.Applications)
            .HasForeignKey(application => application.ApplicantId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Domain.Entities.Application>()
            .HasOne(application => application.ScholarshipProgram)
            .WithMany(scholarshipProgram => scholarshipProgram.Applications)
            .HasForeignKey(application => application.ScholarshipProgramId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Domain.Entities.Application>()
            .HasOne(application => application.Award)
            .WithOne(award => award.Application)
            .HasForeignKey<Award>(award => award.ApplicationId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Document>()
            .HasOne(document => document.Application)
            .WithMany(application => application.Documents)
            .HasForeignKey(document => document.ApplicationId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Review>()
            .HasOne(review => review.Provider)
            .WithMany(provider => provider.Reviews)
            .HasForeignKey(review => review.ProviderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Review>()
            .HasOne(review => review.Application)
            .WithMany(application => application.Reviews)
            .HasForeignKey(review => review.ApplicationId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Criteria>()
            .HasOne(criterion => criterion.ScholarshipProgram)
            .WithMany(scholarshipProgram => scholarshipProgram.Criteria)
            .HasForeignKey(criterion => criterion.ScholarshipProgramId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<University>()
            .HasOne(university => university.Country)
            .WithMany(country => country.Universities)
            .HasForeignKey(university => university.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ScholarshipProgram>()
            .HasOne(scholarshipProgram => scholarshipProgram.Funder)
            .WithMany(funder => funder.FunderCreatedScholarshipPrograms)
            .HasForeignKey(scholarshipProgram => scholarshipProgram.FunderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ScholarshipProgram>()
            .HasOne(scholarshipProgram => scholarshipProgram.Provider)
            .WithMany(provider => provider.ProviderAssignedScholarshipPrograms)
            .HasForeignKey(scholarshipProgram => scholarshipProgram.ProviderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ScholarshipProgram>()
            .HasMany(scholarshipProgram => scholarshipProgram.Categories)
            .WithMany(category => category.ScholarshipPrograms)
            .UsingEntity("scholarship_program_category");

        modelBuilder.Entity<ScholarshipProgram>()
            .HasMany(scholarshipProgram => scholarshipProgram.Universities)
            .WithMany(university => university.ScholarshipPrograms)
            .UsingEntity("scholarship_program_university");

        modelBuilder.Entity<ScholarshipProgram>()
            .HasMany(scholarshipProgram => scholarshipProgram.Majors)
            .WithMany(major => major.ScholarshipPrograms)
            .UsingEntity("scholarship_program_major");

        modelBuilder.Entity<Feedback>()
            .HasOne(feedback => feedback.Funder)
            .WithMany(funder => funder.FunderFeedbacks)
            .HasForeignKey(feedback => feedback.FunderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Feedback>()
            .HasOne(feedback => feedback.Provider)
            .WithMany(provider => provider.ProviderFeedbacks)
            .HasForeignKey(feedback => feedback.ProviderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}