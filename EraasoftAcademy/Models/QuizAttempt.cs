using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EraasoftAcademy.Models
{
    public class QuizAttempt
    {
        public int Id { get; set; } // primary key for the QuizAttempt entity
        public DateTime SubmittedAt { get; set; } // when the quiz attempt was submitted
        public bool IsSubmitted { get; set; } = false; // whether the quiz attempt has been submitted or is still in progress
        public int Score { get; set; } // the score obtained in the quiz attempt
        public string QuizCode { get; set; } // the code of the quiz being attempted
        public int QuizId { get; set; } // foreign key to the Quiz entity
        public Quiz Quiz { get; set; } // navigation property to the Quiz entity
        public TimeSpan CreatedAt { get; set; } // when the quiz attempt was created

        // Student RelationShip

        /*
        public string StudentId { get; set; } // foreign key to the ApplicationUser entity
        public ApplicationUser User { get; set; } // navigation property to the ApplicationUser entity
        */
        public ICollection<StudentAnswer> StudentAnswers { get; set; } // navigation property to the StudentAnswer entity
    }
}
