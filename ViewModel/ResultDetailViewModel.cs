using project.Models;

namespace project.ViewModel
{
    public class ResultDetailViewModel
    {
        public List<QuizQuestion> QuizQuestions { get; set; }
        
        public List<QuizAnswer> QuizAnswers { get; set; }
        public List<TakeAnswer> takeAnswers { get; set; }
        public Quiz Quizzes { get; set; }
    }
}
