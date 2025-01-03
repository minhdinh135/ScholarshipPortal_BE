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

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            var baseEntity = (BaseEntity)entityEntry.Entity;

            var updatedAtProperty =
                entityEntry.Properties.FirstOrDefault(p => p.Metadata.Name == nameof(BaseEntity.UpdatedAt));
            if (entityEntry.State == EntityState.Modified && updatedAtProperty != null)
            {
                var databaseValues = await entityEntry.GetDatabaseValuesAsync(cancellationToken);
                var originalValue = databaseValues?.GetValue<DateTime?>(nameof(BaseEntity.UpdatedAt));

                if (originalValue.HasValue && originalValue.Value != (DateTime?)updatedAtProperty.CurrentValue)
                {
                    baseEntity.UpdatedAt = (DateTime?)updatedAtProperty.CurrentValue;
                }
                else
                {
                    baseEntity.UpdatedAt = DateTime.Now;
                }
            }
            else
            {
                baseEntity.UpdatedAt = DateTime.Now;
            }

            if (entityEntry.State == EntityState.Added)
            {
                baseEntity.CreatedAt = DateTime.Now;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }


    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Wallet> Wallets { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<ApplicantProfile> ApplicantProfiles { get; set; }

    public virtual DbSet<ApplicationDocument> ApplicationDocuments { get; set; }

    public virtual DbSet<ApplicantSkill> ApplicantSkills { get; set; }

    public virtual DbSet<ApplicantCertificate> ApplicantCertificates { get; set; }

    public virtual DbSet<Experience> Experiences { get; set; }

    public virtual DbSet<ProviderProfile> ProviderProfiles { get; set; }

    public virtual DbSet<ProviderDocument> ProviderDocuments { get; set; }

    public virtual DbSet<FunderProfile> FunderProfiles { get; set; }

    public virtual DbSet<FunderDocument> FunderDocuments { get; set; }

    public virtual DbSet<ExpertProfile> ExpertProfiles { get; set; }

    public virtual DbSet<Education> Educations { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<RequestDetail> RequestDetails { get; set; }

    public virtual DbSet<RequestDetailFile> RequestDetailFiles { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Domain.Entities.Application> Applications { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<ScholarshipProgram> ScholarshipPrograms { get; set; }

    public virtual DbSet<AwardMilestone> AwardMilestones { get; set; }

    public virtual DbSet<AwardMilestoneDocument> AwardMilestoneDocuments { get; set; }

    public virtual DbSet<ReviewMilestone> ReviewMilestones { get; set; }

    public virtual DbSet<Criteria> Criteria { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<Certificate> Certificates { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<University> Universities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Major> Majors { get; set; }

    public virtual DbSet<ExpertForProgram> ExpertForPrograms { get; set; }

    public virtual DbSet<ScholarshipProgramCertificate> ScholarshipProgramCertificates { get; set; }

    public virtual DbSet<MajorSkill> MajorSkills { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "";
 
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{environment}.json", true, true)
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

        modelBuilder.Entity<FunderProfile>()
            .ToTable("funder_profiles");

        modelBuilder.Entity<ExpertProfile>()
            .ToTable("expert_profiles");

        modelBuilder.Entity<ProviderProfile>()
            .ToTable("provider_profiles");

        modelBuilder.Entity<ApplicationDocument>()
            .ToTable("application_documents");

        modelBuilder.Entity<ApplicantSkill>()
            .ToTable("applicant_skills");

        modelBuilder.Entity<ApplicantCertificate>()
            .ToTable("applicant_certificates");

        modelBuilder.Entity<FunderDocument>()
            .ToTable("funder_documents");

        modelBuilder.Entity<ProviderDocument>()
            .ToTable("provider_documents");

        modelBuilder.Entity<RequestDetail>()
            .ToTable("request_details");

        modelBuilder.Entity<RequestDetailFile>()
            .ToTable("request_detail_files");

        modelBuilder.Entity<AwardMilestone>()
            .ToTable("award_milestones");

        modelBuilder.Entity<AwardMilestoneDocument>()
            .ToTable("award_milestone_documents");

        modelBuilder.Entity<ReviewMilestone>()
            .ToTable("review_milestones");

        modelBuilder.Entity<ScholarshipProgram>()
            .ToTable("scholarship_programs");

        modelBuilder.Entity<ScholarshipProgramCertificate>()
            .ToTable("scholarship_program_certificates");

        modelBuilder.Entity<ExpertForProgram>()
            .ToTable("expert_for_programs");

        modelBuilder.Entity<MajorSkill>()
            .ToTable("major_skills");

        // Configure relationships
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasOne(account => account.Role)
                .WithMany(role => role.Accounts)
                .HasForeignKey(account => account.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(account => account.Subscription)
                .WithMany(subscription => subscription.Accounts)
                .HasForeignKey(account => account.SubscriptionId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(account => account.ApplicantProfile)
                .WithOne(applicantProfile => applicantProfile.Applicant)
                .HasForeignKey<ApplicantProfile>(applicantProfile => applicantProfile.ApplicantId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(account => account.FunderProfile)
                .WithOne(funderProfile => funderProfile.Funder)
                .HasForeignKey<FunderProfile>(funderProfile => funderProfile.FunderId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(account => account.ExpertProfile)
                .WithOne(expertProfile => expertProfile.Expert)
                .HasForeignKey<ExpertProfile>(expertProfile => expertProfile.ExpertId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(account => account.ProviderProfile)
                .WithOne(providerProfile => providerProfile.Provider)
                .HasForeignKey<ProviderProfile>(providerProfile => providerProfile.ProviderId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(account => account.Wallet)
                .WithOne(wallet => wallet.Account)
                .HasForeignKey<Wallet>(wallet => wallet.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(account => account.Funder)
                .WithMany(account => account.Experts)
                .HasForeignKey(account => account.FunderId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasOne(chat => chat.Sender)
                .WithMany(account => account.SenderChats)
                .HasForeignKey(chat => chat.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(chat => chat.Receiver)
                .WithMany(account => account.ReceiverChats)
                .HasForeignKey(chat => chat.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Notification>()
            .HasOne(notification => notification.Receiver)
            .WithMany(account => account.Notifications)
            .HasForeignKey(notification => notification.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasOne(transaction => transaction.WalletSender)
                .WithMany(wallet => wallet.SenderTransactions)
                .HasForeignKey(transaction => transaction.WalletSenderId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(transaction => transaction.WalletReceiver)
                .WithMany(wallet => wallet.ReceiverTransactions)
                .HasForeignKey(transaction => transaction.WalletReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Education>()
            .HasOne(achievement => achievement.ApplicantProfile)
            .WithMany(applicantProfile => applicantProfile.Educations)
            .HasForeignKey(achievement => achievement.ApplicantProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ApplicantCertificate>()
            .HasOne(certificate => certificate.ApplicantProfile)
            .WithMany(applicantProfile => applicantProfile.ApplicantCertificates)
            .HasForeignKey(achievement => achievement.ApplicantProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ApplicantSkill>()
            .HasOne(skill => skill.ApplicantProfile)
            .WithMany(applicantProfile => applicantProfile.ApplicantSkills)
            .HasForeignKey(skill => skill.ApplicantProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<FunderDocument>()
            .HasOne(funderDocument => funderDocument.FunderProfile)
            .WithMany(funderProfile => funderProfile.FunderDocuments)
            .HasForeignKey(funderDocument => funderDocument.FunderProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProviderDocument>()
            .HasOne(providerDocument => providerDocument.ProviderProfile)
            .WithMany(providerProfile => providerProfile.ProviderDocuments)
            .HasForeignKey(providerDocument => providerDocument.ProviderProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Domain.Entities.Application>(entity =>
        {
            entity.HasOne(application => application.Applicant)
                .WithMany(applicant => applicant.Applications)
                .HasForeignKey(application => application.ApplicantId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(application => application.ScholarshipProgram)
                .WithMany(scholarshipProgram => scholarshipProgram.Applications)
                .HasForeignKey(application => application.ScholarshipProgramId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ApplicationDocument>()
            .HasOne(document => document.Application)
            .WithMany(application => application.ApplicationDocuments)
            .HasForeignKey(document => document.ApplicationId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasOne(review => review.Expert)
                .WithMany(expert => expert.ApplicationReviews)
                .HasForeignKey(review => review.ExpertId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(review => review.Application)
                .WithMany(application => application.ApplicationReviews)
                .HasForeignKey(review => review.ApplicationId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasOne(feedback => feedback.Applicant)
                .WithMany(applicant => applicant.Feedbacks)
                .HasForeignKey(feedback => feedback.ApplicantId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(feedback => feedback.Service)
                .WithMany(service => service.Feedbacks)
                .HasForeignKey(feedback => feedback.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<RequestDetail>(entity =>
        {
            entity.HasOne(rd => rd.Request)
                .WithMany(r => r.RequestDetails)
                .HasForeignKey(rd => rd.RequestId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(rd => rd.Service)
                .WithMany(s => s.RequestDetails)
                .HasForeignKey(rd => rd.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<RequestDetailFile>(entity =>
        {
            entity.HasOne(rdf => rdf.RequestDetail)
                .WithMany(rd => rd.RequestDetailFiles)
                .HasForeignKey(rdf => rdf.RequestDetailId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Criteria>()
            .HasOne(criterion => criterion.ScholarshipProgram)
            .WithMany(scholarshipProgram => scholarshipProgram.Criteria)
            .HasForeignKey(criterion => criterion.ScholarshipProgramId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Document>()
            .HasOne(document => document.ScholarshipProgram)
            .WithMany(scholarshipProgram => scholarshipProgram.Documents)
            .HasForeignKey(document => document.ScholarshipProgramId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<University>()
            .HasOne(university => university.Country)
            .WithMany(country => country.Universities)
            .HasForeignKey(university => university.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ScholarshipProgram>(entity =>
        {
            entity.HasOne(scholarshipProgram => scholarshipProgram.Funder)
                .WithMany(funder => funder.FunderScholarshipPrograms)
                .HasForeignKey(scholarshipProgram => scholarshipProgram.FunderId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(scholarshipProgram => scholarshipProgram.Category)
                .WithMany(category => category.ScholarshipPrograms)
                .HasForeignKey(scholarshipProgram => scholarshipProgram.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(scholarshipProgram => scholarshipProgram.University)
                .WithMany(university => university.ScholarshipPrograms)
                .HasForeignKey(scholarshipProgram => scholarshipProgram.UniversityId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(scholarshipProgram => scholarshipProgram.Major)
                .WithMany(major => major.ScholarshipPrograms)
                .HasForeignKey(scholarshipProgram => scholarshipProgram.MajorId)
                .OnDelete(DeleteBehavior.Restrict);
        });


        modelBuilder.Entity<AwardMilestone>()
            .HasOne(awardMilestone => awardMilestone.ScholarshipProgram)
            .WithMany(scholarshipProgram => scholarshipProgram.AwardMilestones)
            .HasForeignKey(awardMilestone => awardMilestone.ScholarshipProgramId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<AwardMilestoneDocument>()
            .HasOne(awd => awd.AwardMilestone)
            .WithMany(aw => aw.AwardMilestoneDocuments)
            .HasForeignKey(awd => awd.AwardMilestoneId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ReviewMilestone>()
            .HasOne(reviewMilestone => reviewMilestone.ScholarshipProgram)
            .WithMany(scholarshipProgram => scholarshipProgram.ReviewMilestones)
            .HasForeignKey(reviewMilestone => reviewMilestone.ScholarshipProgramId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Major>()
            .HasOne(m => m.ParentMajor)
            .WithMany(m => m.SubMajors)
            .HasForeignKey(e => e.ParentMajorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MajorSkill>(entity =>
        {
            entity.HasKey(ms => new { ms.MajorId, ms.SkillId });

            entity.HasOne(ms => ms.Major)
                .WithMany(m => m.MajorSkills)
                .HasForeignKey(ms => ms.MajorId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(ms => ms.Skill)
                .WithMany(s => s.MajorSkills)
                .HasForeignKey(ms => ms.SkillId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ScholarshipProgramCertificate>(entity =>
        {
            entity.HasKey(spc => new { spc.ScholarshipProgramId, spc.CertificateId });

            entity.HasOne(spc => spc.ScholarshipProgram)
                .WithMany(sp => sp.ScholarshipProgramCertificates)
                .HasForeignKey(spc => spc.ScholarshipProgramId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(spc => spc.Certificate)
                .WithMany(c => c.ScholarshipProgramCertificates)
                .HasForeignKey(spc => spc.CertificateId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        modelBuilder.Entity<ExpertForProgram>(entity =>
        {
            entity.HasKey(x => new { x.ScholarshipProgramId, x.ExpertId });
            
            entity.HasOne(expertProgram => expertProgram.Expert)
                .WithMany(account => account.AssignedPrograms)
                .HasForeignKey(expertProgram => expertProgram.ExpertId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(expertProgram => expertProgram.ScholarshipProgram)
                .WithMany(program => program.AssignedExperts)
                .HasForeignKey(expertProgram => expertProgram.ScholarshipProgramId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}