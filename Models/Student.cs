using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MVCStudentReportGenaration.Models
{
    public class Student
    {

        public int Id { get; set; } // Primary Key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Place { get; set; } // E.g., Auckland

        public string Ethnicity { get; set; }
        public bool IsDomestic { get; set; } // True for Domestic, False for International

        [Required]
        public int CourseId { get; set; } // Foreign Key to the Course

        // Navigation Property
        [JsonIgnore]
        public virtual Course? Course { get; set; }
    }
}
