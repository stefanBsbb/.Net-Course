using DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class UserViewModel
    {
        
        public int UserID { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FirstName { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string LastName { get; set; }

        public bool IsAdministrator { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Username { get; set; }


        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

       

        #region Constructors
        public UserViewModel()
        {
            // create a default constructor, because the MVC needs it when the form is submitted, 
            // in order to create object of this type as parameter in an action
        }
        public UserViewModel(User user)
        {
            this.UserID = user.UserID;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.IsAdministrator = user.IsAdministrator;
            this.Username = user.UserName;
            this.Password = user.Password;
        }
        #endregion

        #region public methods
        public void UpdateDbModel(User dbUser)
        {
            dbUser.UserID = this.UserID;
            dbUser.FirstName = this.FirstName;
            dbUser.LastName = this.LastName;
            dbUser.IsAdministrator = this.IsAdministrator;
            dbUser.UserName = this.Username;
            dbUser.Password = this.Password;
        }
        #endregion
    }
}