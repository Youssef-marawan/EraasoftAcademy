using EraasoftAcademy.Models;
using EraasoftAcademy.Repositories.IRepositories;
using EraasoftAcademy.ViewModel.QuizVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace EraasoftAcademy.Areas.Student.Controllers
{
    [Area("Student")]
    public class QuizController : Controller
    {
        private readonly IGenericRepository<Quiz> _quizRepo;
        private readonly IGenericRepository<QuestionChoices> _choicesRepo;
        private readonly IGenericRepository<QuizAttempt> _quizAttemptRepo;
        private readonly IGenericRepository<StudentAnswer> _studentAnswerRepo;


        public QuizController(IGenericRepository<Quiz> quizRepo, IGenericRepository<QuestionChoices> choicesRepo , IGenericRepository<QuizAttempt> quizAttemptRepo, IGenericRepository<StudentAnswer> studentAnswerRepo)
        {
            _quizRepo = quizRepo;
            _choicesRepo = choicesRepo;
            _quizAttemptRepo = quizAttemptRepo;
            _studentAnswerRepo = studentAnswerRepo;
        }

        public async Task<IActionResult> Index()
        {
            var quizzes = await _quizRepo.GetAllAsync(includes: new Expression<Func<Quiz, object>>[]
                                                    {
                                                        q => q.Course
                                                    });

            var activeQuizzes = quizzes
                .Where(q => q.IsActive)
                .ToList();
            var quizList = new ViewModel.QuizVM.QuizVM()
            {
                QuizList = (IEnumerable<Quiz>)quizzes
            };



            return View(quizList);
        }

        public async Task<IActionResult> Start(int id)
        {
            var quiz = await _quizRepo.GetByIdAsync(id, includes: new Expression<Func<Quiz, object>>[]
                                                    {
                                                        q => q.QuizQuestions
                                                    });

            if (quiz == null)
                return NotFound();


            if (!quiz.IsActive)
                return BadRequest("Quiz not available");



            return View();
        }

        public async Task<IActionResult> QuizTake(int id)
        {
            var quiz = await _quizRepo.GetByIdAsync_2(id, query =>
                            query.Include(q => q.QuizQuestions)
                                  .ThenInclude(qq => qq.QuestionChoices)
                                          
                            );

            var quizDetails = new QuizTakeVM()
            {
                Title = quiz.Title,
                Duration = quiz.Duration,
                Id = id,
                QuizId = quiz.Id,
                Questions = quiz.QuizQuestions.ToList()
            };

            return View(quizDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Submit(SubmitVM model)
        {
            if (model.StudentAnswers == null || !model.StudentAnswers.Any())
            {
                // تعامل مع حالة عدم وجود إجابات
                return RedirectToAction("Index");
            }

            int score = 0;
            var questionIds = model.StudentAnswers.Select(x => x.QuestionId).ToList();

            // جلب الإجابات الصحيحة للأسئلة المرسلة فقط
            var correctAnswers = await _choicesRepo.GetAllAsync(
                expression: a => a.IsCorrect && questionIds.Contains(a.QuizQuestionId)
            );

            var correctDict = correctAnswers.ToDictionary(a => a.QuizQuestionId, a => a.Id);


            var attempt = new QuizAttempt
            {
                QuizId = model.QuizId,
                Score = score,
                SubmittedAt = DateTime.Now,
                IsSubmitted = true,
            };

            await _quizAttemptRepo.AddAsync(attempt);
            await _quizAttemptRepo.SaveChangesAsync();

            // 1. عرف قائمة لتخزين إجابات الأسئلة قبل حفظها
            var studentAnswerEntries = new List<StudentAnswer>();

            foreach (var answer in model.StudentAnswers)
            {
                // بنجيب الإجابة الصحيحة من الـ Dictionary اللي حضرناه فوق
                if (correctDict.TryGetValue(answer.QuestionId, out var correctId))
                {
                    bool isCorrect = answer.SelectedAnswerId == correctId;

                    // 2. إنشاء كائن الإجابة
                    var studentAnswer = new StudentAnswer
                    {
                        QuizAttemptId = attempt.Id, // نربط الإجابة بمحاولة الدخول دي
                        QuizQuestionId = answer.QuestionId,
                        QuestionChoiceId = (int)answer.SelectedAnswerId, // الإجابة اللي الطالب اختارها
                        IsCorrect = isCorrect
                    };

                    // 3. أضفها للقائمة
                    studentAnswerEntries.Add(studentAnswer);

                    // 4. حساب السكور بناءً على وزن السؤال (Score)
                    if (isCorrect)
                    {
                        score += answer.Score;
                    }
                }
            }

            foreach (var data in studentAnswerEntries)
            {
                await _studentAnswerRepo.AddAsync(data);
            }


            // 6. تحديث سكور المحاولة النهائي وحفظ التغييرات
            attempt.Score = score;
            await _quizAttemptRepo.SaveChangesAsync();

            // جلب بيانات الكويز للعرض في صفحة النتيجة
            var quiz = await _quizRepo.GetByIdAsync(model.QuizId);

            // يفضل عمل ViewModel خاص للنتيجة بدل إعادة استخدام QuizTakeVM
            var resultVM = new QuizResultVM
            {
                QuizTitle = quiz.Title,
                Score = score,
                TotalMarks = (int)quiz.TotalMarks,
                QuestionResults = quiz.QuizQuestions.Select(q => new QuestionResultsVM
                {
                    QuestionText = q.QuestionText,
                    QuestionType = q.QuestionType,
                    QuestionMarks = (int)q.Marks,
                    // بنشوف الطالب جاوب إيه للسؤال ده
                    StudentChoiceId = correctDict.TryGetValue(q.Id, out var choiceId) ? choiceId : null,
                    // بنعرف لو كانت إجابته صح بمقارنتها بالـ Dictionary اللي فيه الإجابات الصحيحة
                    IsCorrect = correctDict.TryGetValue(q.Id, out var sId) &&
                                correctDict.TryGetValue(q.Id, out var cId) && sId == cId,

                    Choices = q.QuestionChoices.Select(c => new ChoiceResultVM
                    {
                        Id = c.Id,
                        ChoiceText = c.ChoiceText,
                        IsCorrect = c.IsCorrect
                    }).ToList()
                }).ToList()
            };

            return View("QuizResult", resultVM);
        }
        }
}
