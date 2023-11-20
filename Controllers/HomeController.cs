using Microsoft.AspNetCore.Mvc;
using project.Models;
using System.Diagnostics.Eventing.Reader;
using System.Text.Json;

namespace project.Controllers
{
    public class HomeController : Controller
    {
        ProjectPrnContext context = new ProjectPrnContext();
        public IActionResult Index()
        {
            string data = HttpContext.Session.GetString("user");
            if(data != null)
            {
                User u = JsonSerializer.Deserialize<User>(data);
                ViewBag.User = u;
            }
            
            return View(context.CategoryQuizzes.ToList());
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(String username, String password)
        {
            List<User> users = context.Users.ToList();
            User data = users.Find(x=>x.Username == username && x.Userpassword== password);
            if(data == null)
            {
                String mess = "*User name or password invalid";
                ViewBag.mess = mess;
                return View();
            }
            else
            {
                HttpContext.Session.SetString("user", JsonSerializer.Serialize(data));
                return RedirectToAction("Index");
            }
            
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string username, string password, string repassword)
        {
            List<User> u = context.Users.ToList();
            User user = u.Find(x => x.Username.Equals(username));
            
            if(user != null)
            {
                string mess = "Username exsit";
                ViewBag.mess = mess;
                return View();
            }else if (!password.Equals(repassword))
            {
                string mess = "Password and confirm password not match";
                ViewBag.mess = mess;
                return View();
            }
            else
            {
                User users = new User(username, password);
                context.Users.Add(users);
                context.SaveChanges();
            }
            return RedirectToAction("Login");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            return RedirectToAction("Index");
        }
    }
}
