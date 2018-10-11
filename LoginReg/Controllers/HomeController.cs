using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LoginReg.Models;
using Microsoft.EntityFrameworkCore;

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
        public ViewResult Index()
        {
            // check if logged in
            //// if logged in go to success.
            return View();
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginSubmission sub)
        {
            if(ModelState.IsValid)
            {
                // check login info
                //// return views from errors
                return RedirectToAction("Success");
            }

            return View("Index");
        }

        [HttpGet("Register")]
        public ViewResult Register()
        {

            return View("Register");
        }

        [HttpPost("CreateUser")]
        public IActionResult CreateUser(User user)
        {
            if(ModelState.IsValid)
            {
                //create user
                //update session info
                return RedirectToAction("Success");
            }

            return View("Register");
        }

        [HttpGet("Success")]
        public ViewResult Success()
        {
            //get session user info
            return View("Success");
        }

        [HttpGet("Logout")]
        public RedirectToActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
