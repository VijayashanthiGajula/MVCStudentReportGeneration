using Microsoft.EntityFrameworkCore;
using MVCStudentReportGenaration.Models;

namespace MVCStudentReportGenaration.Context
{
    public class StudentDBContext : DbContext
    {
        public StudentDBContext(DbContextOptions<StudentDBContext> options)
    : base(options)
        {
        }
        //Code for bringing the entity model that we created for students and courses
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            DataToSeed.Seed(modelBuilder);
        }
    }
}
