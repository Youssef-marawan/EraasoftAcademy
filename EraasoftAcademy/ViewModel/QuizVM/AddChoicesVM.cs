using EraasoftAcademy.Models;

namespace EraasoftAcademy.ViewModel.Quiz
{
    public class AddChoicesVM
    {
        
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }

        public List<QuestionChoiceVM> Choices { get; set; } = new();
    }
}
