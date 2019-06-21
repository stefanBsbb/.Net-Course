using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using DataAccess;
using webapp.Helpers;
namespace webapp.Models
{
    public class HomeHotelsViewModel
    {
        public int ID { get; set; }

        public string ImagePath { get; set; }
    }
    public class HomeViewModel
    {
        public List<HomeHotelsViewModel> HotelsList { get; set; }

        public HomeViewModel()
        {
            
            HotelsList = new List<HomeHotelsViewModel>();
        }
        public HomeViewModel(List<Hotel> allHotels)
            : this()
        {
            foreach (Hotel hotel in allHotels)
            {
                if (string.IsNullOrEmpty(hotel.ImageName) == false)
                {
                    string imageFullPath = Path.Combine(Constants.ImagesDirectory, hotel.ImageName);
                    string physicalPath = HttpContext.Current.Server.MapPath(imageFullPath);
                    bool fileExists = File.Exists(physicalPath);
                    if (fileExists == false)
                    {
                        imageFullPath = Path.Combine(Constants.ImagesDirectory, "no_image.png");
                    }

                    HomeHotelsViewModel item = new HomeHotelsViewModel();
                    item.ID = hotel.HotelID;
                    item.ImagePath = imageFullPath;
                    HotelsList.Add(item);

                }
            }
        }
    }
}