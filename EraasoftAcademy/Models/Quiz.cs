using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EraasoftAcademy.Models
{
    public class Quiz
	{
        public int Id { get; set; } // primary key for the Quiz entity
        public string Title { get; set; } = null!; // the title of the quiz
        public string Description { get; set; } = null!; // a brief description of the quiz 
        public DateTime ScheduledDate { get; set; } // when the quiz is scheduled to take place 
		public TimeSpan Duration { get; set; } // duration of the quiz
		public double TotalMarks { get; set; } // total marks for the quiz
		public bool IsActive { get; set; } = true; // indicates if the quiz is currently active or not
		public string? QuizCode { get; set; }  // a unique code for the quiz that students can use to access it 

        // Course RelationShip

        
        public int CourseId { get; set; }// Course RelationShip
        public Course? Course { get; set; }
		
        public int InstructorId { get; set; } // Instructor RelationShip

        public ICollection<QuizQuestion> QuizQuestions { get; set; }= [];// QuizQuestion RelationShip
		public ICollection<QuizAttempt> QuizAttempts { get; set; } = []; // QuizAttempt RelationShip
    }
}
