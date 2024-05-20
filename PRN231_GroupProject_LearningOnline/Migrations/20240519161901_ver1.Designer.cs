﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PRN231_GroupProject_LearningOnline.Models.Entity;

#nullable disable

namespace PRN231_GroupProject_LearningOnline.Migrations
{
    [DbContext(typeof(DonationWebApp_v2Context))]
    [Migration("20240519161901_ver1")]
    partial class ver1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PRN231_GroupProject_LearningOnline.Models.Entity.FundraisingProject", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProjectId"), 1L, 1);

                    b.Property<bool>("Discontinued")
                        .HasColumnType("bit");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Story")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TargetAmount")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<byte>("Type")
                        .HasMaxLength(255)
                        .HasColumnType("tinyint");

                    b.HasKey("ProjectId");

                    b.ToTable("FundraisingProject", (string)null);
                });

            modelBuilder.Entity("PRN231_GroupProject_LearningOnline.Models.Entity.Order", b =>
                {
                    b.Property<string>("OrderId")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Amount")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("BankCode")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("DateOfDonation")
                        .HasColumnType("datetime");

                    b.Property<string>("ErrorCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("LocalMessage")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("OrderInfo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("PRN231_GroupProject_LearningOnline.Models.Entity.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("RoleId");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("PRN231_GroupProject_LearningOnline.Models.Entity.StudentFee", b =>
                {
                    b.Property<string>("StudentFeeId")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Amount")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("BankCode")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("CourseEnrollId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateOfPaid")
                        .HasColumnType("datetime");

                    b.Property<string>("ErrorCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("LocalMessage")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("OrderInfo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("StudentFeeId");

                    b.HasIndex("CourseEnrollId")
                        .IsUnique()
                        .HasFilter("[CourseEnrollId] IS NOT NULL");

                    b.ToTable("StudentFee", (string)null);
                });

            modelBuilder.Entity("PRN231_GroupProject_LearningOnline.Models.Entity.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Phone")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("UserId");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("PRN231_GroupProject_LearningOnline.Models.Entity.UserRole", b =>
                {
                    b.Property<int>("UserRoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserRoleId"), 1L, 1);

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserRoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole", (string)null);
                });

            modelBuilder.Entity("PRN231_GroupProject_LearningOnline.temp.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CategoryID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("PRN231_GroupProject_LearningOnline.temp.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CourseID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("CategoryID");

                    b.Property<string>("CourseInfo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPrivate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("Price")
                        .HasColumnType("bigint");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("CourseId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Course", (string)null);
                });

            modelBuilder.Entity("PRN231_GroupProject_LearningOnline.temp.CourseEnroll", b =>
                {
                    b.Property<int>("CourseEnrollId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CourseEnrollID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseEnrollId"), 1L, 1);

                    b.Property<float?>("AverageGrade")
                        .HasColumnType("real")
                        .HasColumnName("AverageGrade");

                    b.Property<int>("CourseId")
                        .HasColumnType("int")
                        .HasColumnName("CourseID");

                    b.Property<int>("CourseStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("EnrollDate")
                        .HasColumnType("date");

                    b.Property<string>("Grade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LessonCurrent")
                        .HasColumnType("int");

                    b.Property<string>("StudentFeeId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("CourseEnrollId");

                    b.HasIndex("CourseId");

                    b.HasIndex("UserId");

                    b.ToTable("CourseEnroll", (string)null);
                });

            modelBuilder.Entity("PRN231_GroupProject_LearningOnline.temp.Lesson", b =>
                {
                    b.Property<int>("LessonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("LessonID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LessonId"), 1L, 1);

                    b.Property<int>("CourseId")
                        .HasColumnType("int")
                        .HasColumnName("CourseID");

                    b.Property<string>("Description")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<int?>("LessonNum")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Quiz")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("VideoUrl")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.HasKey("LessonId");

                    b.HasIndex("CourseId");

                    b.ToTable("Lesson", (string)null);
                });

            modelBuilder.Entity("PRN231_GroupProject_LearningOnline.temp.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ReviewID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewId"), 1L, 1);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Content");

                    b.Property<int>("CourseId")
                        .HasColumnType("int")
                        .HasColumnName("CourseID");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.Property<int>("Vote")
                        .HasColumnType("int");

                    b.HasKey("ReviewId");

                    b.HasIndex("CourseId");

                    b.HasIndex("UserId");

                    b.ToTable("Review", (string)null);
                });

            modelBuilder.Entity("PRN231_GroupProject_LearningOnline.Models.Entity.Order", b =>
                {
                    b.HasOne("PRN231_GroupProject_LearningOnline.Models.Entity.FundraisingProject", "Project")
                        .WithMany("Orders")
                        .HasForeignKey("ProjectId")
                        .HasConstraintName("FK_Order_FundraisingProject");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("PRN231_GroupProject_LearningOnline.Models.Entity.StudentFee", b =>
                {
                    b.HasOne("PRN231_GroupProject_LearningOnline.temp.CourseEnroll", "CourseEnroll")
                        .WithOne("StudentFee")
                        .HasForeignKey("PRN231_GroupProject_LearningOnline.Models.Entity.StudentFee", "CourseEnrollId")
                        .HasConstraintName("FK_CE_SF");

                    b.Navigation("CourseEnroll");
                });

            modelBuilder.Entity("PRN231_GroupProject_LearningOnline.Models.Entity.UserRole", b =>
                {
                    b.HasOne("PRN231_GroupProject_LearningOnline.Models.Entity.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .IsRequired()
                        .HasConstraintName("FK_UserRole_Role");

                    b.HasOne("PRN231_GroupProject_LearningOnline.Models.Entity.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_UserRole_User");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PRN231_GroupProject_LearningOnline.temp.Course", b =>
                {
                    b.HasOne("PRN231_GroupProject_LearningOnline.temp.Category", "Category")
                        .WithMany("Courses")
                        .HasForeignKey("CategoryId")
                        .IsRequired()
                        .HasConstraintName("FK_Course_CategoryID");

                    b.HasOne("PRN231_GroupProject_LearningOnline.Models.Entity.User", "User")
                        .WithMany("Courses")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Course_UserID");

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PRN231_GroupProject_LearningOnline.temp.CourseEnroll", b =>
                {
                    b.HasOne("PRN231_GroupProject_LearningOnline.temp.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .IsRequired()
                        .HasConstraintName("FK_CourseEnroll_CourseID");

                    b.HasOne("PRN231_GroupProject_LearningOnline.Models.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_CourseEnroll_UserID");

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PRN231_GroupProject_LearningOnline.temp.Lesson", b =>
                {
                    b.HasOne("PRN231_GroupProject_LearningOnline.temp.Course", "Course")
                        .WithMany("Lessons")
                        .HasForeignKey("CourseId")
                        .IsRequired()
                        .HasConstraintName("FK_Lesson_CourseID");

                    b.HasOne("PRN231_GroupProject_LearningOnline.temp.Lesson", "PreviousLession")
                        .WithOne("SubsequenceLession")
                        .HasForeignKey("PRN231_GroupProject_LearningOnline.temp.Lesson", "LessonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("PreviousLession");
                });

            modelBuilder.Entity("PRN231_GroupProject_LearningOnline.temp.Review", b =>
                {
                    b.HasOne("PRN231_GroupProject_LearningOnline.temp.Course", "Course")
                        .WithMany("Reviews")
                        .HasForeignKey("CourseId")
                        .IsRequired()
                        .HasConstraintName("FK_Review_CourseID");

                    b.HasOne("PRN231_GroupProject_LearningOnline.Models.Entity.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Review_UserID");

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PRN231_GroupProject_LearningOnline.Models.Entity.FundraisingProject", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("PRN231_GroupProject_LearningOnline.Models.Entity.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("PRN231_GroupProject_LearningOnline.Models.Entity.User", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("Reviews");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("PRN231_GroupProject_LearningOnline.temp.Category", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("PRN231_GroupProject_LearningOnline.temp.Course", b =>
                {
                    b.Navigation("Lessons");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("PRN231_GroupProject_LearningOnline.temp.CourseEnroll", b =>
                {
                    b.Navigation("StudentFee");
                });

            modelBuilder.Entity("PRN231_GroupProject_LearningOnline.temp.Lesson", b =>
                {
                    b.Navigation("SubsequenceLession");
                });
#pragma warning restore 612, 618
        }
    }
}
