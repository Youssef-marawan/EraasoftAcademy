namespace EraasoftAcademy.Models
{
    public class StudentEnrollment
    {
        public int EnrollmentId { get; set; } // primary key for the StudentEnrollment entity
        //public Student Student { get; set; } = default!; // Student relationship
        public int StudentId { get; set; } // foreign key to the Student entity
        //public Course Course { get; set; } = default!; // Course relationship
        public int CourseId { get; set; } // foreign key to the Course entity

        public DateTime EnrollmentDate { get; set; } // the date when the student enrolled in the course
        public string Status { get; set; } = null!; // enrollment status (Active, Dropped, Completed)
        public ICollection<StudentAttendance> StudentAttendances { get; set; } = new List<StudentAttendance>(); // one enrollment can have many attendance records


    }
}
