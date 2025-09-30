using Microsoft.EntityFrameworkCore;
using QuizApp.Models;

namespace QuizApp.Models
{
    public class QuizSession
    {
        public int QuizSessionId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int QuizId { get; set; }
        public virtual Quiz Quiz { get; set; } = default!;
        public int Score { get; set; }
    }
}