using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EraasoftAcademy.Models
{
    public class Course
    {
        public int Course_Id { get; set; }

        public string Course_Name { get; set; }

        public int Credits { get; set; }

        public int Teacher_Id { get; set; }

        public Teacher Teacher { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
