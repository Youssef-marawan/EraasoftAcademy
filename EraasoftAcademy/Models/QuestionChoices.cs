using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EraasoftAcademy.Models
{
    public class QuestionChoices
	{
        public int Id { get; set; } // primary key for the QuestionChoices entity
        public string ChoiceText { get; set; } = null!; // the text of the choice or option for a quiz question
        public bool IsCorrect { get; set; } // indicates whether this choice is the correct answer for the associated quiz question                           
        public int QuizQuestionId { get; set; } // foreign key to the QuizQuestion entity
        public QuizQuestion QuizQuestion { get; set; } // QuizQuestion RelationShip
    }
}
