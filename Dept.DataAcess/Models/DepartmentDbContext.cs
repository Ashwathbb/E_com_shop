using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Dept.DataAcess.Models;

public partial class DepartmentDbContext : DbContext
{
    public DepartmentDbContext()
    {
    }

    public DepartmentDbContext(DbContextOptions<DepartmentDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ER-CPU-T2-0018;Database=DepartmentDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Address__091C2A1BD8D72979");

            entity.ToTable("Address");

            entity.Property(e => e.AddressId)
                .ValueGeneratedNever()
                .HasColumnName("AddressID");
            entity.Property(e => e.AddressGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("AddressGUID");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Street)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ZipCode)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Customer).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Address_Customer");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B81F937AF8");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId)
                .ValueGeneratedNever()
                .HasColumnName("CustomerID");
            entity.Property(e => e.CustomerGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("CustomerGUID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BED579A4F07");

            entity.ToTable("Department");

            entity.Property(e => e.DepartmentGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DepartmentName).HasMaxLength(100);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF18F6D9C99");

            entity.ToTable("Employee");

            entity.Property(e => e.EmployeeId)
                .ValueGeneratedNever()
                .HasColumnName("EmployeeID");
            entity.Property(e => e.EmployeeGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("EmployeeGUID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasMany(d => d.Roles).WithMany(p => p.Employees)
                .UsingEntity<Dictionary<string, object>>(
                    "EmployeeRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__EmployeeR__RoleI__4BAC3F29"),
                    l => l.HasOne<Employee>().WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__EmployeeR__Emplo__4AB81AF0"),
                    j =>
                    {
                        j.HasKey("EmployeeId", "RoleId").HasName("PK__Employee__C27FE310CFA0FE69");
                        j.ToTable("EmployeeRole");
                        j.IndexerProperty<int>("EmployeeId").HasColumnName("EmployeeID");
                    });
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAF4B525B52");

            entity.Property(e => e.OrderId)
                .ValueGeneratedNever()
                .HasColumnName("OrderID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.OrderGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("OrderGUID");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Orders_Customer");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1AC1D6385C");

            entity.ToTable("Role");

            entity.Property(e => e.RoleGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.RoleName).HasMaxLength(100);

            entity.HasOne(d => d.Department).WithMany(p => p.Roles)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Role__Department__35BCFE0A");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("PK__Sales__1EE3C41FAA60AC54");

            entity.Property(e => e.SaleId)
                .ValueGeneratedNever()
                .HasColumnName("SaleID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.SaleGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("SaleGUID");

            entity.HasOne(d => d.Order).WithMany(p => p.Sales)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_Sales_Orders");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
