using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PRN231_GroupProject_LearningOnline.Models.Entity
{
    //dotnet tool install --global dotnet-ef 
    //        Cài đặt công cụ dotnet ef
    //dotnet tool update --global dotnet-ef 
    //        Cập nhật công cụ dotnet ef
    //dotnet ef migrations add NameMigration  
    //        Tạo một Migration có tên NameMigration
    //dotnet ef migrations list   
    //        Danh sách các Migration
    //dotnet ef database update   
    //        Cập nhật Database đến Migration cuối
    //dotnet ef database update NameMigration 
    //        Cập nhật Database đến Migration có tên NameMigration
    //dotnet ef migrations remove 
    //        Xóa migration cuối
    //dotnet ef migrations script --output migrations.sql 
    //        Xuất lệnh SQL khi thực hiện Migration
    //dotnet ef database drop -f 
    //    Xóa database
    public partial class DonationWebApp_v2Context : DbContext
    {
        public DonationWebApp_v2Context()
        {
        }

        public DonationWebApp_v2Context(DbContextOptions<DonationWebApp_v2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<FundraisingProject> FundraisingProjects { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json").Build().GetConnectionString("MyDB");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FundraisingProject>(entity =>
            {
                entity.HasKey(e => e.ProjectId);

                entity.ToTable("FundraisingProject");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.Type).HasMaxLength(255);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId).HasMaxLength(255);

                entity.Property(e => e.Amount).HasMaxLength(255);

                entity.Property(e => e.BankCode).HasMaxLength(255);

                entity.Property(e => e.ErrorCode).HasMaxLength(10);

                entity.Property(e => e.LocalMessage).HasMaxLength(255);

                entity.Property(e => e.OrderInfo).HasMaxLength(255);

                entity.Property(e => e.PaymentMethod).HasMaxLength(255);

                entity.Property(e => e.DateOfDonation).HasColumnType("datetime");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_Order_FundraisingProject");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.RoleName).HasMaxLength(20);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.FirstName).HasMaxLength(255);

                entity.Property(e => e.LastName).HasMaxLength(255);

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.Phone).HasMaxLength(255);

                entity.Property(e => e.UserName).HasMaxLength(255);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRole_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRole_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
