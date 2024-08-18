using Microsoft.EntityFrameworkCore;
using MVCStudentReportGenaration.Models;

namespace MVCStudentReportGenaration.Models
{
    public static class DataToSeed
    { 
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Seed Courses
            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Name = "Computer Science" },
                new Course { Id = 2, Name = "Information Technology" },
                new Course { Id = 3, Name = "Software Engineering" }
                );
            // Student data to seed
            modelBuilder.Entity<Student>().HasData(
               new Student
               {
                   Id = 1,
                   FirstName = "John",
                   LastName = "Doe",
                   DateOfBirth = new DateTime(2000, 1, 1),
                   Place = "Auckland",
                   Ethnicity = "European",
                   IsDomestic = true,
                   CourseId = 1
               },
               new Student
               {
                   Id = 2,
                   FirstName = "Jane",
                   LastName = "Smith",
                   DateOfBirth = new DateTime(1999, 5, 15),
                   Place = "Wellington",
                   Ethnicity = "Māori",
                   IsDomestic = true,
                   CourseId = 2
               },
               new Student
               {
                   Id = 3,
                   FirstName = "Michael",
                   LastName = "Brown",
                   DateOfBirth = new DateTime(2001, 9, 23),
                   Place = "Auckland",
                   Ethnicity = "Asian",
                   IsDomestic = false,
                   CourseId = 3
               },
               new Student
               {
                   Id = 4,
                   FirstName = "Emily",
                   LastName = "White",
                   DateOfBirth = new DateTime(2000, 12, 12),
                   Place = "Christchurch",
                   Ethnicity = "Pacific Islander",
                   IsDomestic = true,
                   CourseId = 1
               }
           );
        }

    }
}

