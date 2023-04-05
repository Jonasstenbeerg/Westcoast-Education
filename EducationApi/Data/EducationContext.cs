using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EducationApi.Data
{
    public class EducationContext : DbContext
    {
        public EducationContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Competence> Competencies => Set<Competence>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<StudentCourses> StudentCourses => Set<StudentCourses>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<TeacherCompetencies> TeacherCompetencies => Set<TeacherCompetencies>();
        public DbSet<UserInfo> UserInfos => Set<UserInfo>();
        public DbSet<Address> Addresses => Set<Address>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeacherCompetencies>()
            .HasKey(c => new {c.CompetenceId,c.TeacherId});

            modelBuilder.Entity<StudentCourses>()
            .HasKey(c => new {c.StudentId,c.CourseId});

            modelBuilder.Entity<Course>()
            .HasOne(c=>c.Teacher)
            .WithMany(t=>t.Courses)
            .OnDelete(DeleteBehavior.SetNull);
            
            //SQL krävde detta för att funka tydligen

            // modelBuilder.Entity<StudentCourses>()
            // .HasOne(sc => sc.Course)
            // .WithMany(s => s.Students)
            // .HasForeignKey(sc => sc.CourseId)
            // .OnDelete(DeleteBehavior.Restrict);
            
            // modelBuilder.Entity<StudentCourses>()
            // .HasOne(sc => sc.Student)
            // .WithMany(s => s.Courses)
            // .HasForeignKey(sc => sc.StudentId)
            // .OnDelete(DeleteBehavior.Restrict);

            
           
        }
    }
}