using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PRN231_GroupProject_LearningOnline.temp;

namespace PRN231_GroupProject_LearningOnline.Models.Entity
{
    //dotnet tool install --global dotnet-ef 
    //        Cài đặt công cụ dotnet ef
    //dotnet tool update --global dotnet-ef 
    //        Cập nhật công cụ dotnet ef
    //dotnet ef migrations add dbcontextver  
    //        Tạo một Migration có tên dbcontextver
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
        public virtual DbSet<StudentFee> StudentFees { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<CourseEnroll> CourseEnrolls { get; set; } = null!;
        public virtual DbSet<Lesson> Lessons { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;

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

            modelBuilder.Entity<StudentFee>(entity =>
            {
                entity.ToTable("StudentFee");
                entity.HasKey(e => e.StudentFeeId);

                entity.Property(e => e.StudentFeeId).HasMaxLength(255);

                entity.Property(e => e.Amount).HasMaxLength(255);

                entity.Property(e => e.BankCode).HasMaxLength(255);

                entity.Property(e => e.ErrorCode).HasMaxLength(10);

                entity.Property(e => e.LocalMessage).HasMaxLength(255);

                entity.Property(e => e.OrderInfo).HasMaxLength(255);

                entity.Property(e => e.PaymentMethod).HasMaxLength(255);

                entity.Property(e => e.DateOfPaid).HasColumnType("datetime");

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
                entity.Property(e => e.Active).HasDefaultValue(true);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.UserRoleId);
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

            //
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");
                entity.Property(e => e.Description).HasColumnType("nvarchar(max)");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                //entity.Property(e => e.UserId).HasColumnName("UserID");
                entity.Property(e => e.IsPrivate).HasDefaultValue(true);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Course_CategoryID");

                //entity.HasOne(d => d.User)
                //    .WithMany(p => p.Courses)
                //    .HasForeignKey(d => d.UserId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Course_UserID");
            });

            modelBuilder.Entity<CourseEnroll>(entity =>
            {
                //entity.HasNoKey();
                //entity.HasKey(e => new { e.CourseId, e.UserId })
                //    .HasName("PK__CourseEnroll__A8049041EBCA9414");
                entity.HasKey(e => e.CourseEnrollId);
                entity.ToTable("CourseEnroll");

                entity.Property(e => e.CourseEnrollId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CourseEnrollID");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.EnrollDate).HasColumnType("date");

                entity.Property(e => e.UserId).HasColumnName("UserID");
                entity.Property(e => e.Grade).HasColumnType("nvarchar(max)");
                entity.Property(e => e.AverageGrade).HasColumnName("AverageGrade");

                entity.HasOne(d => d.Course)
                    .WithMany()
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseEnroll_CourseID");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseEnroll_UserID");

                //entity.HasOne(e => e.StudentFee)
                //    .WithOne(e => e.CourseEnroll)
                //    .HasForeignKey<StudentFee>(e => e.StudentFeeId)
                //    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(d => d.StudentFee)
                    .WithOne(p => p.CourseEnroll)
                    .HasForeignKey<StudentFee>(d => d.CourseEnrollId)
                    .HasConstraintName("FK_CE_SF");
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.ToTable("Lesson");

                entity.Property(e => e.LessonId).HasColumnName("LessonID");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.Description).HasMaxLength(4000);

                entity.Property(e => e.VideoUrl).HasMaxLength(4000);

                entity.Property(e => e.Quiz).HasColumnType("nvarchar(max)");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Lesson_CourseID");

                entity.HasOne(d => d.Course)
                   .WithMany(p => p.Lessons)
                   .HasForeignKey(d => d.CourseId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_Lesson_CourseID");

                //entity.HasOne(e => e.PreviousLession)
                //    .WithOne()
                //    .HasForeignKey<Lesson>(e => e.PreviousLessionId)
                //    .OnDelete(DeleteBehavior.Restrict);

                //.OnDelete(DeleteBehavior.Cascade); // or .OnDelete(DeleteBehavior.Restrict)
            });



            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Review");

                entity.Property(e => e.ReviewId).HasColumnName("ReviewID");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
                entity.Property(e => e.Content).HasColumnName("Content");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Review_CourseID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Review_UserID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
