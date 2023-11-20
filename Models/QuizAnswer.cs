using System;
using System.Collections.Generic;

namespace project.Models
{
    public partial class QuizAnswer
    {
        public QuizAnswer()
        {
            TakeAnswers = new HashSet<TakeAnswer>();
        }

        public int Id { get; set; }
        public int? QuizId { get; set; }
        public int? QuestionId { get; set; }
        public int? Correct { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? Description { get; set; }

        public virtual QuizQuestion? Question { get; set; }
        public virtual ICollection<TakeAnswer> TakeAnswers { get; set; }
    }
}
