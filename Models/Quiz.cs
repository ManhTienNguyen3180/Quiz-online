using System;
using System.Collections.Generic;

namespace project.Models
{
    public partial class Quiz
    {
        public Quiz()
        {
            QuizQuestions = new HashSet<QuizQuestion>();
            Takes = new HashSet<Take>();
        }

        public int QuizId { get; set; }
        public string? Title { get; set; }
        public string? Summary { get; set; }
        public short? Type { get; set; }
        public short? Score { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }

        public virtual CategoryQuiz? Category { get; set; }
        public virtual ICollection<QuizQuestion> QuizQuestions { get; set; }
        public virtual ICollection<Take> Takes { get; set; }
    }
}
