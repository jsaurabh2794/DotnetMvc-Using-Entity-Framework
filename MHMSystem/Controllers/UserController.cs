using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MHMSystem.Models;
using Microsoft.AspNetCore.Http;

namespace MHMSystem.Controllers
{
    
    public class UserController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public UserController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        [BindProperty]
        public User user { get; set; }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult doLogin()
        {
            return View();
        }
        public IActionResult doRegister()
        {
            
            return View();
        }
        public IActionResult Dashboard()
        {
            if (checkSessionIsValidOrNot())
            {
                return RedirectToAction("doLogin", "User");
            }
            return View();
        }
        public IActionResult doLogout()
        {
            if (checkSessionIsValidOrNot())
            {
                return RedirectToAction("doLogin", "User");
            }
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult onSubmitLogin(IFormCollection form)
        {
            string userId = form["requestId"];
            Int32 userID = Int32.Parse(userId);
            var user = this.applicationDbContext.Users.Find(userID);
            if(user != null)
            {
                HttpContext.Session.SetInt32("userId", user.userId);
                HttpContext.Session.SetString("firstName", user.firstName);
                HttpContext.Session.SetString("lastName", user.lastName);
                HttpContext.Session.SetString("email", user.email);
                HttpContext.Session.SetString("mobile", user.email.ToString());
                return View("Dashboard");
            }
            ViewBag.errorMessage = "Invalide user Id, Please try again.";
            return View("doLogin");
        }
        [HttpPost]
        public IActionResult onRegistrationSubmit()
        {
            if (ModelState.IsValid)
            {
                if (user.userId == 0)
                {
                    this.applicationDbContext.Users.Add(user);
                }
                else
                {
                    this.applicationDbContext.Users.Update(user);
                }
                this.applicationDbContext.SaveChanges();
                ViewBag.userId = user.userId;
                return View(user);
            }
            ViewData["Message"] = "Error while Registration.";
            return RedirectToAction("doRegister");
        }

        public IActionResult doView()
        {
            if (checkSessionIsValidOrNot())
            {
                return RedirectToAction("doLogin", "User");
            }
            int userId = (int)HttpContext.Session.GetInt32("userId");
            var user = this.applicationDbContext.Users.Find(userId);
            ViewBag.user = user;
            return View();
        }
        public IActionResult doSubmitEdit(IFormCollection form)
        {
            if (checkSessionIsValidOrNot())
            {
                return RedirectToAction("doLogin", "User");
            }
            int userId = (int)HttpContext.Session.GetInt32("userId");
            var user = this.applicationDbContext.Users.Find(userId);
            string firstName = form["firstName"];
            string lastName = form["lastName"];
            string email = form["email"];
            string mobile = form["mobile"];
           
            user.firstName = firstName;
            user.lastName = lastName;
            user.email = email;
            user.mobile = Int64.Parse(mobile);

            this.applicationDbContext.Users.Update(user);
            this.applicationDbContext.SaveChanges();

            return RedirectToAction("doView");
        }
        public IActionResult doEdit()
        {
            if (checkSessionIsValidOrNot())
            {
                return RedirectToAction("doLogin", "User");
            }
            int userId = (int)HttpContext.Session.GetInt32("userId");
            var user = this.applicationDbContext.Users.Find(userId);
            ViewBag.user = user;
            return View();
        }
        public bool checkSessionIsValidOrNot()
        {
            string firstName = HttpContext.Session.GetString("firstName");
            if (firstName == null)
            {
                return true;
            }
            return false;
        }
    }
}