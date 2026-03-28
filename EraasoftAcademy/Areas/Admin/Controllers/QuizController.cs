using EraasoftAcademy.DataAccess;
using EraasoftAcademy.Models;
using EraasoftAcademy.Repositories;
using EraasoftAcademy.Repositories.IRepositories;
using EraasoftAcademy.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EraasoftAcademy.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuizController : Controller
    {

        private readonly IGenericRepository<Quiz> _QuizRepo;
        private readonly IGenericRepository<Course> _courseRepo;
        private readonly IGenericRepository<QuizQuestion> _QuestionRepo;

        public QuizController(IGenericRepository<Quiz> productRepo, IGenericRepository<Course> courseRepo, IGenericRepository<QuizQuestion> questionRepo)
        {
            _QuizRepo = productRepo;
            _courseRepo = courseRepo;
            _QuestionRepo = questionRepo;
        }


        public async Task<IActionResult> Index()
        {
            var quizzes = await _QuizRepo.GetAllAsync(includes: new Expression<Func<Quiz, object>>[]
                                                    {
                                                        q => q.Course
                                                    });

            var quizList = new ViewModel.QuizVM()
            {
                QuizList = (IEnumerable<Quiz>)quizzes
            };

            return View(quizList);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var quiz = await _QuizRepo.GetByIdAsync(Id);
            if (quiz == null)
            {
                return NotFound();
            }
            _QuizRepo.Delete(quiz);
            await _QuizRepo.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var courses = await _courseRepo.GetAllAsync();
            ViewBag.Courses = courses;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                quiz.QuizCode = Guid.NewGuid()
                                            .ToString("N")
                                            .Substring(0, 8)
                                            .ToUpper();
                quiz.ScheduledDate = DateTime.UtcNow;
                await _QuizRepo.AddAsync(quiz);
                await _QuizRepo.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            var courses = await _courseRepo.GetAllAsync();
            ViewBag.Courses = courses;
            return View(quiz);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var quiz = await _QuizRepo.GetByIdAsync(Id);
            if (quiz == null)
            {
                return NotFound();
            }
            var courses = await _courseRepo.GetAllAsync();
            ViewBag.Courses = courses;
            return View(quiz);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                quiz.QuizCode = Guid.NewGuid()
                                            .ToString("N")
                                            .Substring(0, 8)
                                            .ToUpper();
                _QuizRepo.Update(quiz);
                await _QuizRepo.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            var courses = await _courseRepo.GetAllAsync();
            ViewBag.Courses = courses;
            return View(quiz);

        }

        [HttpGet]
        public async Task<IActionResult> QuestionList(int Id)
        {
            var questions = await _QuestionRepo.GetAllAsync(
                            expression: q => q.QuizId == Id,
                            includes: new Expression<Func<QuizQuestion, object>>[]
                            {
                                q => q.QuestionChoices
                            });
            if (questions == null)
            {
                return NotFound();
            }
            ViewBag.QuizId = Id;
            return View(questions);
        }

        [HttpGet]
        public IActionResult CreateQuestion(int quizId)
        {
            ViewBag.QuizId = quizId;
            return View();
        }
       
    }
}
