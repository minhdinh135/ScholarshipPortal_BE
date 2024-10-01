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

        modelBuilder.Entity<Account>()
            .HasOne(account => account.Role)
            .WithMany(role => role.Accounts)
            .HasForeignKey(account => account.RoleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}