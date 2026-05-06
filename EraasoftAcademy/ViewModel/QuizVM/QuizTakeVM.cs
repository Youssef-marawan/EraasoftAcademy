using EraasoftAcademy.Models;

namespace EraasoftAcademy.ViewModel.QuizVM
{
    public class QuizTakeVM
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }
        

        public List<QuizQuestion> Questions { get; set; }
        
    }
}
