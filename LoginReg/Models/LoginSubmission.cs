using System;
using System.ComponentModel.DataAnnotations;

namespace LoginReg.Models
{
    public class LoginSubmission
    {
        public LoginSubmission()
        {
        }
        
        [Required(ErrorMessage = "Enter an email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter a password")]
        public string Password { get; set; }
    }
}
