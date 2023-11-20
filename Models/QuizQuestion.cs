using System;
using System.Collections.Generic;

namespace project.Models
{
    public partial class QuizQuestion
    {
        public QuizQuestion()
        {
            QuizAnswers = new HashSet<QuizAnswer>();
            TakeAnswers = new HashSet<TakeAnswer>();
        }

        public int Id { get; set; }
        public int? QuizId { get; set; }
        public int? Score { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? Active { get; set; }

        public virtual Quiz? Quiz { get; set; }
        public virtual ICollection<QuizAnswer> QuizAnswers { get; set; }
        public virtual ICollection<TakeAnswer> TakeAnswers { get; set; }
    }
}
