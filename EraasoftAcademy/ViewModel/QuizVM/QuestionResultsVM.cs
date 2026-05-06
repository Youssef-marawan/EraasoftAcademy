using DomainLayer.Enums;

namespace EraasoftAcademy.ViewModel.QuizVM
{
    public class QuestionResultsVM
    {
        public string QuestionText { get; set; }
        public QuestionTypes QuestionType { get; set; }
        public int QuestionMarks { get; set; }
        public bool IsCorrect { get; set; }
        public int? StudentChoiceId { get; set; }
        public List<ChoiceResultVM> Choices { get; set; }
    }
}
