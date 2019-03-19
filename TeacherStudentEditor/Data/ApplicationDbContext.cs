using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TeacherStudentEditor.Models;

namespace TeacherStudentEditor.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public const string AdministratorRoleName = "Administrator";
        public const string TeacherRoleName = "Teacher";
        public const string StudentRoleName = "Student";

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Course>().HasMany(x => x.Classes).WithOne().IsRequired();
            builder.Entity<Class>().HasOne(x => x.Teacher).WithMany().IsRequired();
            //builder.Entity<Class>().HasMany(x => x.Files).WithOne().IsRequired();
            builder.Entity<Question>().HasMany(x => x.Answers).WithOne().IsRequired();
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Class> Classes { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<SessionQuestion> SessionQuestions { get; set; }
        public DbSet<EditorSession> Sessions { get; set; }
    }
}
