namespace EraasoftAcademy.Models
{
    public class StudentAttendance
    {
        public int AttendanceId { get; set; } // primary key for the StudentAttendance entity
        //public Session Session { get; set; } = default!; // Session relationship
        public int SessionId { get; set; } // foreign key to the Session entity
        public StudentEnrollment Enrollment { get; set; } = default!; // Enrollment relationship
        public int EnrollmentId { get; set; } // foreign key to the StudentEnrollment entity
        //public Student Student { get; set; } = default!; // Student relationship
        public int StudentId { get; set; } // foreign key to the Student entity
        public string Status { get; set; } = null!; // attendance status (Present, Absent, Late)

        public string? Note { get; set; } // optional note for the attendance record
    }
}
