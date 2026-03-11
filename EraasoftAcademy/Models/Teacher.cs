using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EraasoftAcademy.Models
{
    public class Teacher
    {
        public int Teacher_Id { get; set; }

        public int User_Id { get; set; }

        public string Specialization { get; set; }

        public DateTime HireDate { get; set; }

        public User User { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}
