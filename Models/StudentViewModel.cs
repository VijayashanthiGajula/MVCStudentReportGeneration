namespace MVCStudentReportGenaration.Models
{    
        public class StudentViewModel
    {
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public string FilterFN { get; set; }
        public string FilterLN { get; set; }

        public string FilterPlace { get; set; }
        public string FilterE { get; set; }
        public IEnumerable<string> Places { get; set; } // Optional, for dropdown filter
        public IEnumerable<string> Ethnicities { get; set; } // Optional, for dropdown filter 
    }
}