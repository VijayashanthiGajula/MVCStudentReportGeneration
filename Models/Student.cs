using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MVCStudentReportGenaration.Models
{
    public class Student
    { 

        public int Id { get; set; } // Primary Key
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;
        public string Place { get; set; } = string.Empty; 

        public string Ethnicity { get; set; } = string.Empty;
        public bool IsDomestic { get; set; } = false;

        [Required]
        public int CourseId { get; set; } // Foreign Key to the Course

        // Navigation Property
        [JsonIgnore]
        public virtual Course? Course { get; set; }

        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - DateOfBirth.Year;
                if (DateOfBirth.Date > today.AddYears(-age)) age--;
                return age;
            }
        }
    }
}
