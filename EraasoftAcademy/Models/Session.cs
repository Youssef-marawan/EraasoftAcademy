namespace EraasoftAcademy.Models
{
    public class Session
    {
        public int SessionId { get; set; } // primary key for the Session entity

        public string RoomNUM { get; set; } = null!; // room number where the session takes place

        public TimeSpan StartTime { get; set; } // session start time
        public TimeSpan EndTime { get; set; } // session end time

        public DateTime SessionDate { get; set; } // date of the session
        //public Course Course { get; set; } = default!; // Course relationship
        public int CourseId { get; set; } // foreign key to the Course entity
        public ICollection<StudentAttendance> StudentAttendances { get; set; } = new List<StudentAttendance>(); // one session can have many attendance records

    }
}
