using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EraasoftAcademy.Models
{
    public class User
    {
        public int User_Id { get; set; }
        public string F_Name { get; set; }
        public string L_Name { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Role { get; set; }
        public Student Student { get; set; }
        public Teacher Teacher { get; set; }
    }
}
