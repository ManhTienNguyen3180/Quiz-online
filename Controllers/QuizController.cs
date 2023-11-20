using Microsoft.AspNetCore.Mvc;
using project.Models;
using project.ViewModel;
using System;
using System.Text.Json;

namespace project.Controllers
{

    public class QuizController : Controller
    {
        ProjectPrnContext context = new ProjectPrnContext();
        public IActionResult Quiz(int quizid)
        {
            string data2 = HttpContext.Session.GetString("user");
            if (data2 != null)
            {
                User u = JsonSerializer.Deserialize<User>(data2);
                ViewBag.User = u;
            }
            List<QuizQuestion> questionList = context.QuizQuestions.ToList();
            List<QuizAnswer> quizAnswers = context.QuizAnswers.ToList();
            List<Quiz> quizlist = context.Quizzes.ToList();
            var data = questionList.Where(x => x.QuizId == quizid).ToList();
            Quiz q = quizlist.Find(x => x.QuizId == quizid);
            DateTime dateTime = DateTime.Now;
            ViewBag.time = dateTime;
            ViewBag.quizname = q.Title;
            ViewBag.sub = q.Summary;
            return View(data);
        }

        public IActionResult QuizDetail(int catename)
        {
            string data2 = HttpContext.Session.GetString("user");
            if (data2 != null)
            {
                User u = JsonSerializer.Deserialize<User>(data2);
                ViewBag.User = u;
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
            List<Quiz> list = context.Quizzes.ToList();
            var data = list.Where(x => x.CategoryId == catename).ToList();
            return View(data);
        }

        [HttpPost]
        public IActionResult Result(int quizId, DateTime datetime)
        {
            string data2 = HttpContext.Session.GetString("user");
            User u = null;
            if (data2 != null)
            {
                u = JsonSerializer.Deserialize<User>(data2);
                ViewBag.User = u;
            }
            List<int> listValue = new List<int>();
            List<QuizQuestion> questionList = context.QuizQuestions.ToList();
            List<QuizAnswer> quizAnswers = context.QuizAnswers.ToList();
            List<Quiz> quizlist = context.Quizzes.ToList();
            Quiz q = quizlist.Find(x => x.QuizId == quizId);
            var data = questionList.Where(x => x.QuizId == quizId).ToList();
            DateTime now = DateTime.Now;
            TimeSpan span = now.Subtract(datetime);
            string time = null;
            if (span.TotalSeconds < 60)
            {
                time = "0 phút " + span.Seconds + " giây";
            }
            else
            {
                time = span.Minutes + " phút" + span.Seconds + " giây";
            }

            foreach (string key in Request.Form.Keys)
            {
                if (key.StartsWith("attemptedAnswerId_"))
                {
                    listValue.Add(Convert.ToInt32(Request.Form[key]));
                }
            }


            int count = 0;

            foreach (var item in listValue)
            {
                QuizAnswer quizAnswer = quizAnswers.Find(x => x.Id == item);

                if (quizAnswer != null && quizAnswer.Correct == 1)
                {
                    count++;
                }
            }
            double mark = (double)count * 10 / data.Count;
            ViewBag.count = count;
            ViewBag.mark = mark;
            ViewBag.title = q.Title;
            ViewBag.tick = time;
            Take take = new Take(u.UserId, q.QuizId, 1, mark, datetime, now, "ok");

            context.Takes.AddAsync(take);
            context.SaveChanges();
            List<Take> listtake = context.Takes.ToList();
            var datatake = listtake.Where(x => x.QuizId == quizId && x.UserId == u.UserId).OrderByDescending(x => x.Score).ToList();
            List<TakeAnswer> takeAnswers = context.TakeAnswers.ToList();
            foreach (var item in listValue)
            {
                TakeAnswer takean = new TakeAnswer();
                takean.TakeId = take.Id;
                takean.AnswerId = item;
                takean.QuestionId = quizAnswers.Find(x => x.Id == item).QuestionId;
                context.TakeAnswers.Add(takean);
                context.SaveChanges();
            }
            return RedirectToAction("Inketqua", new { quizId = quizId, userId = u.UserId,sort =0});
        }

        
        public IActionResult Inketqua(int quizId, int userId, int sort)
        {
            ViewBag.quizId = quizId;
            ViewBag.userId = userId;
            string data2 = HttpContext.Session.GetString("user");
            User u = null;
            if (sort == null)
            {
                sort = 0;
            }
            if (data2 != null)
            {
                u = JsonSerializer.Deserialize<User>(data2);
                ViewBag.User = u;
            }
            if (quizId == 0)
            {
                if (sort == 0)
                {
                    List<Quiz> quizlist = context.Quizzes.ToList();
                    Quiz q = quizlist.Find(x => x.QuizId == quizId);
                    List<Take> listtake = context.Takes.ToList();
                    var datatake = listtake.Where(x => x.UserId == userId).OrderBy(x => x.Score).ToList();
                    ViewBag.sort = sort;
                    return View(datatake);
                }
                else
                {
                    List<Quiz> quizlist = context.Quizzes.ToList();
                    Quiz q = quizlist.Find(x => x.QuizId == quizId);
                    List<Take> listtake = context.Takes.ToList();
                    var datatake = listtake.Where(x => x.UserId == userId).OrderByDescending(x => x.Score).GroupBy(x => x.Quiz.Title).ToList();
                    ViewBag.sort = sort;
                    return View(datatake);
                }
            }
            else
            {
                if (sort == 0)
                {
                    List<Quiz> quizlist = context.Quizzes.ToList();
                    Quiz q = quizlist.Find(x => x.QuizId == quizId);
                    List<Take> listtake = context.Takes.ToList();
                    var datatake = listtake.Where(x => x.UserId == userId).OrderBy(x => x.Score).ToList();
                    ViewBag.sort = sort;
                    return View(datatake);
                }
                else
                {
                    List<Quiz> quizlist = context.Quizzes.ToList();
                    Quiz q = quizlist.Find(x => x.QuizId == quizId);
                    List<Take> listtake = context.Takes.ToList();
                    var datatake = listtake.Where(x => x.UserId == userId).OrderByDescending(x => x.Score).ToList();
                    ViewBag.sort = sort;
                    return View(datatake);
                }

            }

        }
        
        public IActionResult ViewResultDetail(int takeId)
        {
            string data2 = HttpContext.Session.GetString("user");
            User u = null;
            if (data2 != null)
            {
                u = JsonSerializer.Deserialize<User>(data2);
                ViewBag.User = u;
            }
            List<TakeAnswer> takeAnswers = context.TakeAnswers.ToList();
            List<Take> takes = context.Takes.ToList();
            List<QuizQuestion> quizQuestions = context.QuizQuestions.ToList();
            List<QuizAnswer> quizAnswers = context.QuizAnswers.ToList();
            List<Quiz> quizzes = context.Quizzes.ToList();
            var data = takeAnswers.Where(x => x.TakeId == takeId).ToList();
            Take take = takes.Find(x => x.Id == takeId);
            var quizQues = quizQuestions.Where(x => x.QuizId == take.QuizId).ToList();
            var quizAns = quizAnswers.Where(x => x.QuizId == take.QuizId).ToList();
            var quiz = quizzes.Find(x => x.QuizId == take.QuizId);

            var viewmodel = new ResultDetailViewModel
            {
                QuizQuestions = quizQues,
                QuizAnswers = quizAns,
                takeAnswers = data,
                Quizzes = quiz

            };
            return View(viewmodel);
        }

        public IActionResult ManageQuiz()
        {
            string data2 = HttpContext.Session.GetString("user");
            User u = null;
            if (data2 != null)
            {
                u = JsonSerializer.Deserialize<User>(data2);
                ViewBag.User = u;
            }
            List<Quiz> quizzes = context.Quizzes.ToList();
            List<CategoryQuiz> categoryQuizzes = context.CategoryQuizzes.ToList();
            ViewBag.category = categoryQuizzes;

            return View(quizzes);
        }

        [HttpPost]
        public IActionResult ManageQuiz(int cate)
        {
            string data2 = HttpContext.Session.GetString("user");
            User u = null;
            if (data2 != null)
            {
                u = JsonSerializer.Deserialize<User>(data2);
                ViewBag.User = u;
            }
            ViewBag.category = context.CategoryQuizzes.ToList();
            ViewBag.categoryselected = cate;
            List<Quiz> quizzes = context.Quizzes.ToList();
            if(cate != 0)
            {
                quizzes = context.Quizzes.Where(x=>x.CategoryId== cate).ToList();
            }else if(cate == 0)
            {
                return View(quizzes);
            }
            return View(quizzes);
        }

        public IActionResult ViewQuizDetail(int quizid)
        {
            string data2 = HttpContext.Session.GetString("user");
            User u = null;
            if (data2 != null)
            {
                u = JsonSerializer.Deserialize<User>(data2);
                ViewBag.User = u;
            }
            
            return RedirectToAction("Quiz", new { quizId = quizid });
        }

        public IActionResult Take(int takeId)
        {
            List<Quiz> quizzes = context.Quizzes.ToList();
            List<QuizQuestion> quizQuestions = context.QuizQuestions.ToList();
            List<Take> takes = context.Takes.ToList();
            Take take = takes.FirstOrDefault(x => x.Id == takeId);
            Quiz quiz = quizzes.FirstOrDefault(x => x.QuizId == take.QuizId);
            return RedirectToAction("Quiz", new { quizid = take.QuizId });
        }

        public IActionResult Retake(int takeId) {
            string data2 = HttpContext.Session.GetString("user");
            User u = null;
            if (data2 != null)
            {
                u = JsonSerializer.Deserialize<User>(data2);
                ViewBag.User = u;
            }
            List<Take> listtake = context.Takes.ToList();
            
            List<Quiz> quizzes = context.Quizzes.ToList();
            List<QuizQuestion> quizQuestions = context.QuizQuestions.ToList();
            List<QuizAnswer> quizAnswers = context.QuizAnswers.ToList();
            Take take = listtake.FirstOrDefault(x=>x.Id == takeId);
            Quiz quiz = quizzes.FirstOrDefault(x => x.QuizId == take.QuizId);
            
            var datatake = listtake.Where(x => x.QuizId == quiz.QuizId && x.UserId == u.UserId).ToList();
            List<TakeAnswer> takeAnswers = context.TakeAnswers.ToList();
            var quizQues = quizQuestions.Where(x => x.QuizId == take.QuizId).ToList();
            var quizAns = quizAnswers.Where(x => x.QuizId == take.QuizId).ToList();
            var data = takeAnswers.Where(x => x.TakeId == takeId).ToList();
            var viewmodel = new ResultDetailViewModel
            {
                QuizQuestions = quizQues,
                QuizAnswers = quizAns,
                takeAnswers = data,
                Quizzes = quiz

            };
            return View(viewmodel);
        }
        [HttpPost]
        public IActionResult Result2(int quizId, DateTime datetime)
        {
            string data2 = HttpContext.Session.GetString("user");
            User u = null;
            if (data2 != null)
            {
                u = JsonSerializer.Deserialize<User>(data2);
                ViewBag.User = u;
            }
            List<int> listValue = new List<int>();
            List<QuizQuestion> questionList = context.QuizQuestions.ToList();
            List<QuizAnswer> quizAnswers = context.QuizAnswers.ToList();
            List<Quiz> quizlist = context.Quizzes.ToList();
            List<Take> listtake = context.Takes.ToList();
            Quiz q = quizlist.Find(x => x.QuizId == quizId);
            var data = questionList.Where(x => x.QuizId == quizId).ToList();
            DateTime now = DateTime.Now;
            TimeSpan span = now.Subtract(datetime);
            string time = null;
            if (span.TotalSeconds < 60)
            {
                time = "0 phút " + span.Seconds + " giây";
            }
            else
            {
                time = span.Minutes + " phút" + span.Seconds + " giây";
            }

            foreach (string key in Request.Form.Keys)
            {
                if (key.StartsWith("attemptedAnswerId_"))
                {
                    listValue.Add(Convert.ToInt32(Request.Form[key]));
                }
            }


            int count = 0;

            foreach (var item in listValue)
            {
                QuizAnswer quizAnswer = quizAnswers.Find(x => x.Id == item);

                if (quizAnswer != null && quizAnswer.Correct == 1)
                {
                    count++;
                }
            }
            double mark = (double)count * 10 / data.Count;
            ViewBag.count = count;
            ViewBag.mark = mark;
            ViewBag.title = q.Title;
            ViewBag.tick = time;
            Take ta = listtake.Where(x => x.QuizId == quizId).OrderByDescending(x => x.StartAt).SingleOrDefault();
            
            context.Takes.AddAsync();
            context.SaveChanges();
            
            var datatake = listtake.Where(x => x.QuizId == quizId && x.UserId == u.UserId).OrderByDescending(x => x.Score).ToList();
            List<TakeAnswer> takeAnswers = context.TakeAnswers.ToList();
            foreach (var item in listValue)
            {
                TakeAnswer takean = new TakeAnswer();
                takean.TakeId = take.Id;
                takean.AnswerId = item;
                takean.QuestionId = quizAnswers.Find(x => x.Id == item).QuestionId;
                context.TakeAnswers.Add(takean);
                context.SaveChanges();
            }
            return RedirectToAction("Inketqua", new { quizId = quizId, userId = u.UserId, sort = 0 });
        }
    }
}
