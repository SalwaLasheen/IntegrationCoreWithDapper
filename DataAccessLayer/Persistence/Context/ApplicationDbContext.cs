using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System.Data.Common;

namespace DataAccessLayer.Persistence.Context;
public partial class ApplicationDbContext : DbContext
{
 
    public ApplicationDbContext()
    {

    }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }


    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<DataAccessLayer.Models.Job> Jobs { get; set; }
    //public IDbConnection Connection => null;
    //Database.GetDbConnection();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigurationBuilder(modelBuilder);
        OnModelCreatingPartial(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost; Database=DemoDb; Trusted_Connection=True;Encrypt=false; MultipleActiveResultSets=true;Trust Server Certificate=true");
        }
    }

    private static void ConfigurationBuilder(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Department>(entity =>
        {
            entity.Property(e => e.DepartmentId).ValueGeneratedNever();

        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.EmployeeId).ValueGeneratedNever();

            entity.HasOne(d => d.Department).WithMany(p => p.Employees).HasConstraintName("FK_Employees_Departments");

            entity.HasOne(d => d.Job).WithMany(p => p.Employees).HasConstraintName("FK_Employees_Jobs");
        });

        modelBuilder.Entity<DataAccessLayer.Models.Job>(entity =>
        {
            entity.Property(e => e.JobId).ValueGeneratedNever();
        });
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


}