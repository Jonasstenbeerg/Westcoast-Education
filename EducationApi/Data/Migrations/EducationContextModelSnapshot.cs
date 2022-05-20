﻿// <auto-generated />
using EducationApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EducationApi.Data.Migrations
{
    [DbContext(typeof(EducationContext))]
    partial class EducationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.4");

            modelBuilder.Entity("EducationApi.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("StreetNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Zipcode")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("EducationApi.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("EducationApi.Models.Competence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Competencies");
                });

            modelBuilder.Entity("EducationApi.Models.Course", b =>
                {
                    b.Property<int>("CourseNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CourseTitle")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("LenghtInHouers")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TeacherId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CourseNumber");

                    b.HasIndex("CategoryId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("EducationApi.Models.CourseDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CourseId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Module")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("CourseDetails");
                });

            modelBuilder.Entity("EducationApi.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("EducationApi.Models.StudentCourses", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CourseId")
                        .HasColumnType("INTEGER");

                    b.HasKey("StudentId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("StudentCourses");
                });

            modelBuilder.Entity("EducationApi.Models.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("EducationApi.Models.TeacherCompetencies", b =>
                {
                    b.Property<int>("CompetenceId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TeacherId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CompetenceId", "TeacherId");

                    b.HasIndex("TeacherId");

                    b.ToTable("TeacherCompetencies");
                });

            modelBuilder.Entity("EducationApi.Models.UserInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AddressId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("UserInfos");
                });

            modelBuilder.Entity("EducationApi.Models.Course", b =>
                {
                    b.HasOne("EducationApi.Models.Category", "Category")
                        .WithMany("Courses")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EducationApi.Models.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("EducationApi.Models.CourseDetail", b =>
                {
                    b.HasOne("EducationApi.Models.Course", "Course")
                        .WithMany("CourseDetails")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("EducationApi.Models.Student", b =>
                {
                    b.HasOne("EducationApi.Models.UserInfo", "UserInfo")
                        .WithOne("StudentInfo")
                        .HasForeignKey("EducationApi.Models.Student", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("EducationApi.Models.StudentCourses", b =>
                {
                    b.HasOne("EducationApi.Models.Course", "Course")
                        .WithMany("Students")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EducationApi.Models.Student", "Student")
                        .WithMany("Courses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("EducationApi.Models.Teacher", b =>
                {
                    b.HasOne("EducationApi.Models.UserInfo", "UserInfo")
                        .WithOne("TeacherInfo")
                        .HasForeignKey("EducationApi.Models.Teacher", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("EducationApi.Models.TeacherCompetencies", b =>
                {
                    b.HasOne("EducationApi.Models.Competence", "Competence")
                        .WithMany("TeacherCompetencies")
                        .HasForeignKey("CompetenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EducationApi.Models.Teacher", "Teacher")
                        .WithMany("TeacherCompetencies")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Competence");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("EducationApi.Models.UserInfo", b =>
                {
                    b.HasOne("EducationApi.Models.Address", "Address")
                        .WithMany("Users")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("EducationApi.Models.Address", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("EducationApi.Models.Category", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("EducationApi.Models.Competence", b =>
                {
                    b.Navigation("TeacherCompetencies");
                });

            modelBuilder.Entity("EducationApi.Models.Course", b =>
                {
                    b.Navigation("CourseDetails");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("EducationApi.Models.Student", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("EducationApi.Models.Teacher", b =>
                {
                    b.Navigation("TeacherCompetencies");
                });

            modelBuilder.Entity("EducationApi.Models.UserInfo", b =>
                {
                    b.Navigation("StudentInfo")
                        .IsRequired();

                    b.Navigation("TeacherInfo")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
