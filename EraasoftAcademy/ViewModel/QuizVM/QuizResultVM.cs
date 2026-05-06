namespace EraasoftAcademy.ViewModel.QuizVM
{
    public class QuizResultVM
    {
        public string QuizTitle { get; set; }
        public int TotalMarks { get; set; } 
        public int Score { get; set; }
        public List<QuestionResultsVM> QuestionResults { get; set; }
    }
}
