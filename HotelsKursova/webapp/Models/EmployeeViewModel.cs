using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DataAccess;
namespace webapp.Models
{
    public class EmployeeViewModel
    {
        #region Properties
        public int ID { get; set; }

        [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FirstName { get; set; }

        public string SurName { get; set; }

        public string LastName { get; set; }

        public string Title { get; set; }

        public string EGN { get; set; }

        public DateTime Hiredate { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Email { get; set; }

        public int HotelID { get; set; }
        #endregion

        #region Constructors
        public EmployeeViewModel()
        {
            // create a default constructor, because the MVC needs it when the form is submitted, 
            // in order to create object of this type as parameter in an action
        }
        public EmployeeViewModel(Employee employee)
        {
            this.ID = employee.EmployeeID;
            this.FirstName = employee.FirstName;
            this.SurName = employee.Surname;
            this.LastName = employee.LastName;
            this.Title = employee.Title;
            this.EGN = employee.EGN;
            this.Hiredate = employee.HireDate;
            this.PhoneNumber = employee.PhoneNumber;
            this.Address = employee.Address;
            this.City = employee.City;
            this.Email = employee.EmailAddress;
            this.HotelID = employee.HotelID;
            
        }
        #endregion

        #region public methods
        public void UpdateDbModel(Employee dbEmployee)
        {
            dbEmployee.FirstName = this.FirstName;
            dbEmployee.Surname = this.SurName;
            dbEmployee.LastName = this.LastName;
            dbEmployee.Title = this.Title;
            dbEmployee.EGN = this.EGN;
            dbEmployee.HireDate = this.Hiredate;
            dbEmployee.PhoneNumber = this.PhoneNumber;
            dbEmployee.Address = this.Address;
            dbEmployee.City = this.City;
            dbEmployee.EmailAddress = this.Email;
            dbEmployee.HotelID = this.HotelID;


        }
        #endregion
    }
}