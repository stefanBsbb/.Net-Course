using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using webapp.Helpers;
using System.IO;
namespace webapp.Models
{
    public class SearchViewModel
    {
        public int HotelID { get; set; }
        public string HotelName { get; set; }
        
        public string HotelImageUrl { get; set; }
        public string City { get; set; }
        
        public string WebAddress { get; set; }
        public bool HasImage { get; set; }

        public SearchViewModel(Hotel dbhotel)
        {
            this.HotelID = dbhotel.HotelID;
            this.HotelName = dbhotel.HotelName;
            this.WebAddress = dbhotel.WebAddress;
            this.City = dbhotel.City;

          

            // we have to check if the image exists, otherwise the UI crashes if there is no valid image 
            if (string.IsNullOrEmpty(dbhotel.ImageName) == false)
            {
                string imageFullPath = Path.Combine(Constants.ImagesDirectory, dbhotel.ImageName);
                string physicalPath = HttpContext.Current.Server.MapPath(imageFullPath);
                bool fileExists = File.Exists(physicalPath);
                if (fileExists == false)
                {
                    imageFullPath = Path.Combine(Constants.ImagesDirectory, "no_image.png");
                }
                // string imageFullPath = Path.Combine(Constants.ImagesDirectory, dbRestaurant.ImageName);

                this.HasImage = true;
                this.HotelImageUrl = imageFullPath;
            }
        }
    }

}