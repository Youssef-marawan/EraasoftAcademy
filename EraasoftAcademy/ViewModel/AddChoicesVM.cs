using EraasoftAcademy.Models;

namespace EraasoftAcademy.ViewModel
{
    public class AddChoicesVM
    {
        
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }

        public List<QuestionChoiceVM> Choices { get; set; } = new();
    }
}
