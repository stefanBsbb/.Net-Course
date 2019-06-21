using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using System.IO;
using System.ComponentModel.DataAnnotations;
using webapp.Helpers;

namespace webapp.Models
{
    public class HotelsViewModel
    {
        public int HotelID { get; set; }

        [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string HotelName { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string WebAddress { get; set; }

        public string ImageName { get; set; }   

        public bool HasImage { get; set; }
        public string ImagePath { get; set; }
        public HotelsViewModel() { }
        public HotelsViewModel(Hotel dbHotel)
        {
            this.HotelID = dbHotel.HotelID;
            this.HotelName = dbHotel.HotelName;
            this.Address = dbHotel.Address;
            this.City = dbHotel.City;
            this.PostalCode = dbHotel.PostalCode;
            this.PhoneNumber = dbHotel.PhoneNumber;
            this.EmailAddress = dbHotel.EmailAddress;
            this.WebAddress = dbHotel.WebAddress;
            this.ImageName = dbHotel.ImageName;


            this.HasImage = string.IsNullOrEmpty(dbHotel.ImageName) == false;
            if (this.HasImage)
            {
                this.ImagePath = Path.Combine(Constants.ImagesDirectory, this.ImageName);
                string realPath = System.Web.HttpContext.Current.Server.MapPath(this.ImagePath);
                bool isFileExists = File.Exists(realPath);
                if (isFileExists == false)
                {
                    this.ImagePath = Path.Combine(Constants.ImagesDirectory, "FileNotFound.jpg");
                }
            }

        }

        public void UpdateDBModel(Hotel dbHotel)
        {
            
            dbHotel.HotelName = this.HotelName;
            dbHotel.Address = this.Address;
            dbHotel.City = this.City;
            dbHotel.PostalCode = this.PostalCode;
            dbHotel.PhoneNumber = this.PhoneNumber;
            dbHotel.EmailAddress = this.WebAddress;
            dbHotel.WebAddress = this.WebAddress;
            
        }
    }
}