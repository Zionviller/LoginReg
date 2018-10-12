using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LoginReg.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace LoginReg.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context _context;

        public HomeController(Context con)
        {
            _context = con;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            Console.WriteLine($"*** Index START! Session : UserId - {HttpContext.Session.GetInt32("UserId")}");
            if( HttpContext.Session.GetInt32("UserId") == null )
            {
                Console.WriteLine("\n\n*** Not Logged In! Showing Index!");
                return View();
            }

            Console.WriteLine("\n\n*** Logged in. Redirecting to Success..");
            Console.WriteLine($"User Id in Session: {HttpContext.Session.GetInt32("UserId")}");
            return RedirectToAction("Success");
            // check if logged in
            //// if logged in go to success.
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginSubmission sub)
        {
            Console.WriteLine("\n\n** Loggin In");
            if(ModelState.IsValid)
            {
                Console.WriteLine("-- Model is valid.");
                User dbUser = _context.Users
                                        .FirstOrDefault<User>(u => u.Email == sub.Email);

                if(dbUser == null)
                {
                    Console.WriteLine("XXX No User with that email.");
                    ModelState.AddModelError("Email", "Cannot Login.");
                    return View("Index");
                }

                PasswordHasher<LoginSubmission> hasher = new PasswordHasher<LoginSubmission>();
                var result = hasher.VerifyHashedPassword(sub, dbUser.Password, sub.Password);

                if(result == PasswordVerificationResult.Failed)
                {
                    Console.WriteLine("XXX Password invalid.");
                    ModelState.AddModelError("Password", "Cannot Login");
                    return View("Index");
                }


                // check login info
                //// return views from errors

                HttpContext.Session.SetInt32("UserId", dbUser.UserId);
                Console.WriteLine($"\n\n*** Login Successful! User Id: {HttpContext.Session.GetInt32("UserId")}");
                return RedirectToAction("Success");
            }
            Console.WriteLine("Model is invalid.");
            return View("Index");
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                return RedirectToAction("Success");
            }

            return View();
        }

        [HttpPost("CreateUser")]
        public IActionResult CreateUser(User user)
        {
            if(ModelState.IsValid)
            {
                PasswordHasher<User> hasher = new PasswordHasher<User>();

                User nu = new User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = hasher.HashPassword(user, user.Password),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _context.Add<User>(nu);
                _context.SaveChanges();

                int id = _context.Users.LastOrDefault<User>().UserId;
                HttpContext.Session.SetInt32("UserId", id);
                //update session info
                return RedirectToAction("Success");
            }

            return View("Register");
        }

        [HttpGet("Success")]
        public IActionResult Success()
        {
            if(HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Index");
            }

            User user = _context.Users
                                .FirstOrDefault<User>(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
            return View("Success", user);
        }

        [HttpGet("Logout")]
        public RedirectToActionResult Logout()
        {
            HttpContext.Session.Clear();
            Console.WriteLine("\n\n*** Session CLEARED ***");
            return RedirectToAction("Index");
        }
    }
}
