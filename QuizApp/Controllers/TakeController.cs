public class TakeQuizController : Controller{
    private readonly QuizDbContext _context;
 
    public TakeQuizController(QuizDbContext context){
        _context = context;
    }
 
    // Show quiz questions
    public IActionResult Take(int quizId){
        var quiz = _context.Quizzes
                    .Include(q => q.Questions)
                    .ThenInclude(q => q.AnswerOptions)
                    .FirstOrDefault(q => q.QuizId == quizId);
 
        if (quiz == null) return NotFound();
        return View(quiz);
    }
 
    [HttpPost]
    public IActionResult Submit(int quizId, Dictionary<int,int> answers, string userName){
        var quiz = _context.Quizzes
                    .Include(q => q.Questions)
                    .ThenInclude(q => q.AnswerOptions)
                    .FirstOrDefault(q => q.QuizId == quizId);
 
        int score = 0;
        foreach(var question in quiz.Questions){
            if(answers.TryGetValue(question.QuestionId, out int selectedAnswerId))
            {
                var selectedOption = question.AnswerOptions.FirstOrDefault(a => a.AnswerOptionId == selectedAnswerId);
                if(selectedOption != null && selectedOption.IsCorrect) score++;
            }
        }
 
        _context.UserQuizResults.Add(new UserQuizResult { QuizId = quizId, UserName = userName, Score = score });
        _context.SaveChanges();
 
        return RedirectToAction("Result", new { quizId = quizId, userName = userName });
    }
 
    public IActionResult Result(int quizId, string userName)
    {
        var result = _context.UserQuizResults.FirstOrDefault(r => r.QuizId == quizId && r.UserName == userName);
        return View(result);
    }
}