public class QuizController : Controller{
    private readonly QuizDbContext _context;
 
    public QuizController(QuizDbContext context){
        _context = context;
    }
 
    // List all quizzes
    public IActionResult Index() => View(_context.Quizzes.ToList());
 
    // Create quiz
    [HttpGet]
    public IActionResult Create() => View();
 
    [HttpPost]
    public IActionResult Create(Quiz quiz)
    {
        if (ModelState.IsValid)
        {
            _context.Quizzes.Add(quiz);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(quiz);
    }
 
    // Edit, Delete, Details: samme prinsipp
}