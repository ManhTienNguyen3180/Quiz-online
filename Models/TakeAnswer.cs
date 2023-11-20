using System;
using System.Collections.Generic;

namespace project.Models
{
    public partial class TakeAnswer
    {
        public int Id { get; set; }
        public int? TakeId { get; set; }
        public int? QuestionId { get; set; }
        public int? AnswerId { get; set; }
        public byte? Active { get; set; }

        public virtual QuizAnswer? Answer { get; set; }
        public virtual QuizQuestion? Question { get; set; }
        public virtual Take? Take { get; set; }

    }

}
