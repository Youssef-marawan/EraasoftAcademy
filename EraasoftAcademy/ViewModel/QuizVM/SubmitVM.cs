using EraasoftAcademy.Models;

namespace EraasoftAcademy.ViewModel.QuizVM
{
    public class SubmitVM
    {

        public int QuizId { get; set; }

        public List<StudentAnswerVM> StudentAnswers { get; set; }
    }
}
