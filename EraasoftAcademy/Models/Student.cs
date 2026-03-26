using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EraasoftAcademy.Models
{
    public class Student
    {
        public int Student_Id { get; set; }
        public int User_Id { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public User User { get; set; }
       // public ICollection<Course> Courses { get; set; }
    }
}
