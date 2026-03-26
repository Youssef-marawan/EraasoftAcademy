using EraasoftAcademy.DataAccess;
using EraasoftAcademy.Models;
using EraasoftAcademy.Repositories;
using EraasoftAcademy.Repositories.IRepositories;
using EraasoftAcademy.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EraasoftAcademy.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuizController : Controller
    {

        private readonly IGenericRepository<Quiz> _QuizRepo;

        public QuizController(IGenericRepository<Quiz> productRepo)
        {
            _QuizRepo = productRepo;
        }


        public async Task<IActionResult> Index()
        {
            var quizzes = await _QuizRepo.GetAllAsync();

            var quizList = new ViewModel.QuizVM()
            {
                 QuizList = (IEnumerable<Quiz>)quizzes
            };
            return View(quizList);
        }
    }
}
