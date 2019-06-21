using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DataAccess;
namespace webapp.Models
{
    public class VisitorsViewModel
    {
        
        public int ID { get; set; }

        [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string WebAddress { get; set; }
        
        public int HotelID { get; set; }
        public VisitorsViewModel()
        {
            // create a default constructor, because the MVC needs it when the form is submitted, 
            // in order to create object of this type as parameter in an action
        }
        public VisitorsViewModel(Visitor visitor)
        {
            this.ID = visitor.VisitorID;
            this.FirstName = visitor.FirstName;
            this.LastName = visitor.LastName;
            this.Address = visitor.Address;
            this.City = visitor.City;
            this.PostalCode = visitor.PostalCode;
            this.PhoneNumber = visitor.PhoneNumber;
            this.Email = visitor.EmailAddress;
            this.WebAddress = visitor.WebAddress;
            this.HotelID = visitor.HotelID;
        }
       

        
        public void UpdateDbModel(Visitor dbVisitor)
        {

            dbVisitor.FirstName = this.FirstName;
            dbVisitor.LastName = this.LastName;
            dbVisitor.Address = this.Address;
            dbVisitor.City = this.City;
            dbVisitor.PostalCode = this.PostalCode;
            dbVisitor.PhoneNumber = this.PhoneNumber;
            dbVisitor.EmailAddress = this.Email;
            dbVisitor.WebAddress = this.WebAddress;
            dbVisitor.HotelID = this.HotelID;
        }
    }
} 