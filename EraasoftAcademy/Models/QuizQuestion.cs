using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EraasoftAcademy.Models
{
    public class QuizQuestion 
	{
        public int Id { get; set; } // primary key
        public string QuestionText { get; set; } = null!; // the text of the question or heading (Header)
		public QuestionTypes QuestionType { get; set; } // the type of the question (e.g., multiple choice, true/false) 
        public double Marks { get; set; } // marks or points allocated for the question
		public bool IsActive { get; set; } = true; // indicates if the question is currently active or not
        public int QuizId { get; set; } // foreign key to the Quiz entity
        public Quiz Quiz { get; set; } // Quiz RelationShip
        public ICollection<QuestionChoices> QuestionChoices { get; set; } = []; // question Choices relation


    }
}
