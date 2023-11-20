using System;
using System.Collections.Generic;

namespace project.Models
{
    public partial class Take
    {
        public Take()
        {
            TakeAnswers = new HashSet<TakeAnswer>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? QuizId { get; set; }
        public byte? Status { get; set; }
        public double? Score { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public string? Description { get; set; }

        public virtual Quiz? Quiz { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<TakeAnswer> TakeAnswers { get; set; }

        public Take(int? userId, int? quizId, byte? status, double? score, DateTime? startAt, DateTime? endAt, string? description)
        {
            UserId = userId;
            QuizId = quizId;
            Status = status;
            Score = score;
            StartAt = startAt;
            EndAt = endAt;
            Description = description;
        }

    }
}
