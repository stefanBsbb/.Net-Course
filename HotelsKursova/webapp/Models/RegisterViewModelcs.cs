using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DataAccess;
namespace webapp.Models
{
    public class RegisterViewModelcs
    {


        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        public bool IsAdministrator { get; set; }

        public void UpdateDbModel(User dbUser)
        {

            dbUser.FirstName = this.FirstName;
            dbUser.LastName = this.LastName;
            dbUser.UserName = this.Username;
            dbUser.Password = this.Password;
            dbUser.IsAdministrator = this.IsAdministrator;
        }
    }

}