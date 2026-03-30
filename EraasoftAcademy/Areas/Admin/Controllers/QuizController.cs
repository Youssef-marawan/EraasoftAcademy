using EraasoftAcademy.DataAccess;
using EraasoftAcademy.Models;
using EraasoftAcademy.Repositories;
using EraasoftAcademy.Repositories.IRepositories;
using EraasoftAcademy.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IGenericRepository<QuestionChoices> _choicesRepo;

        public QuizController(IGenericRepository<Quiz> productRepo, IGenericRepository<Course> courseRepo, IGenericRepository<QuizQuestion> questionRepo, IGenericRepository<QuestionChoices> choicesRepo)
        {
            _QuizRepo = productRepo;
            _courseRepo = courseRepo;
            _QuestionRepo = questionRepo;
            _choicesRepo = choicesRepo;
        }
        [HttpGet]
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
        [HttpPost]
        public async Task<IActionResult> CreateQuestion(int quizId, QuizQuestion question)
        {
            if (ModelState.IsValid)
            {
                question.QuizId = quizId;
                await _QuestionRepo.AddAsync(question);
                await _QuestionRepo.SaveChangesAsync();
                return RedirectToAction("QuestionList", new { Id = question.QuizId });
            }
            ViewBag.QuizId = question.QuizId;
            return View(question);
        }
        public async Task<IActionResult> DeleteQuestion(int Id)
        {
            var question = await _QuestionRepo.GetByIdAsync(Id);
            var quizId = question.QuizId;
            if (question == null)
            {
                return NotFound();
            }
            _QuestionRepo.Delete(question);
            await _QuestionRepo.SaveChangesAsync();
            return RedirectToAction("QuestionList", new { Id = quizId });

        }
        [HttpGet]
        public async Task<IActionResult> EditQuestion(int Id)
        {
            var question = await _QuestionRepo.GetByIdAsync(Id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }
        [HttpPost]
        public async Task<IActionResult> EditQuestion(QuizQuestion question)
        {
            ViewBag.QuizId = question.QuizId;
            if (ModelState.IsValid)
            {
                _QuestionRepo.Update(question);
                await _QuestionRepo.SaveChangesAsync();
                return RedirectToAction("QuestionList", new { Id = question.QuizId });
            }
            
            return View(question);
        }
        [HttpGet]
        public async Task<IActionResult> Choices(int Id)
        {
            var question = await _QuestionRepo.GetByIdAsync(
                            Id,
                            includes: new Expression<Func<QuizQuestion, object>>[]
                            {
                                q => q.QuestionChoices
                            }
                            );

            if (question == null)
                return NotFound();

            var vm = new AddChoicesVM
            {
                QuestionId = question.Id,
                QuestionText = question.QuestionText,

                Choices = question.QuestionChoices.Select(c => new QuestionChoiceVM
                {
                    ChoiceId = c.Id,
                    ChoiceText = c.ChoiceText,
                    IsCorrect = c.IsCorrect
                }).ToList()
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Choices(AddChoicesVM model)
        {
            var question = await _QuestionRepo.GetByIdAsync(
                            model.QuestionId,
                            includes: new Expression<Func<QuizQuestion, object>>[]
                            {
                                q => q.QuestionChoices
                            }
                            );
            var vm = new AddChoicesVM
            {
                QuestionId = question.Id,
                QuestionText = question.QuestionText,

                Choices = question.QuestionChoices.Select(c => new QuestionChoiceVM
                {
                    ChoiceId = c.Id,
                    ChoiceText = c.ChoiceText,
                    IsCorrect = c.IsCorrect
                }).ToList()
            };
            if (!model.Choices.Any(c => c.IsCorrect))
            {
                ModelState.AddModelError("", "You must select at least one correct answer");
                return View(vm);
            }
            foreach (var item in model.Choices)
            {
                var existingChoice = (await _choicesRepo.GetAllAsync(
                    expression: c => c.QuizQuestionId == model.QuestionId
                                  && c.ChoiceText == item.ChoiceText
                )).FirstOrDefault();

                if (existingChoice != null)
                {
                    existingChoice.IsCorrect = item.IsCorrect;

                    _choicesRepo.Update(existingChoice);
                }
                else
                {
                    var choice = new QuestionChoices
                    {
                        ChoiceText = item.ChoiceText,
                        IsCorrect = item.IsCorrect,
                        QuizQuestionId = model.QuestionId
                    };

                    await _choicesRepo.AddAsync(choice);
                }
            }

            await _choicesRepo.SaveChangesAsync();

            return RedirectToAction("QuestionList", new { Id = question.QuizId });
        }
        public async Task<IActionResult> DeleteChoice(int Id)
        {

            var choice = await _choicesRepo.GetByIdAsync(Id);
            var questionId = choice.QuizQuestionId;
            if (choice == null)
            {
                return NotFound();
            }
            _choicesRepo.Delete(choice);
            await _choicesRepo.SaveChangesAsync();
            return RedirectToAction("Choices", new { Id = questionId });
        }

    }
}
