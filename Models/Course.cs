using System.Text.Json.Serialization;

namespace MVCStudentReportGenaration.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        // Navigation property for the related Courses
        [JsonIgnore]
        public virtual List<Student>? Students { get; set; }
    }
}
