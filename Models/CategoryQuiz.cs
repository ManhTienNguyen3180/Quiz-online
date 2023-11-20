using System;
using System.Collections.Generic;

namespace project.Models
{
    public partial class CategoryQuiz
    {
        public CategoryQuiz()
        {
            Quizzes = new HashSet<Quiz>();
        }

        public int Id { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryDescription { get; set; }

        public virtual ICollection<Quiz> Quizzes { get; set; }
    }
}
