using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginReg.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Enter your first name.")]
        [MinLength(2, ErrorMessage = "First name must be longer.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Enter your last name.")]
        [MinLength(2, ErrorMessage = "Last name must be longer.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Enter an email.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter a password.")]
        [MinLength(8, ErrorMessage = "Passord must be 8 characters or longer.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Re-enter the password")]
        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm { get; set; }
    }
}
