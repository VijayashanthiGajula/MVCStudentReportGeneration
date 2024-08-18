namespace MVCStudentReportGenaration.Models
{    
        public class StudentViewModel
    {
        //public IEnumerable<Student> ? Students { get; set; }
        //public IEnumerable<Course> ? Courses { get; set; }
        //public string? FilterFN{ get; set; }
        //public string? FilterLN { get; set; }

        //public string? FilterPlace { get; set; }
        //public string? FilterE { get; set; }
        //public IEnumerable<string> ? Places { get; set; } // Optional, for dropdown filter
        //public IEnumerable<string> ? Ethnicities { get; set; } // Optional, for dropdown filter 





        public List<Student> Students { get; set; } = new List<Student>();
        public List<Course> Courses { get; set; } = new List<Course>();
        public string FilterFN { get; set; } = string.Empty;
        public string FilterLN { get; set; } = string.Empty;
        public string FilterPlace { get; set; } = string.Empty;
        public string FilterE { get; set; } = string.Empty;
        public List<string> Places { get; set; } = new List<string>();
        public List<string> Ethnicities { get; set; } = new List<string>();
    }
}