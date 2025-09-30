using Microsoft.EntityFrameworkCore;
using QuizApp.Models;

namespace QuizApp.Models
{
    public class Quiz
    {
        public int QuizId { get; set; }
        public string Title { get; set; } = string.Empty;
        public virtual List<Question> Questions { get; set; } = new();
    }
}