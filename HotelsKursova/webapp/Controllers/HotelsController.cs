using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repositories;
using DataAccess;
using webapp.Models;
using webapp.Helpers;
using System.IO;

namespace webapp.Controllers
{
    public class HotelsController : Controller
    {
        // GET: Hotels
        public ActionResult Index()
        {
            // if there are some notification message from other actions, then set them in the viewbag
            // so that we display them in the screen
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            ViewBag.Message = TempData["Message"];

            UnitOfWork unitOfWork = new UnitOfWork();
            List<Hotel> allhotels = unitOfWork.HotelsRepository.GetAll();

            List<HotelsViewModel> model = new List<HotelsViewModel>();
            foreach (Hotel dbhotel in allhotels)
            {
                HotelsViewModel hotelsViewModel = new HotelsViewModel(dbhotel);
                model.Add(hotelsViewModel);
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            // add the Cities to the Viewbag
            UnitOfWork unitOfWork = new UnitOfWork();
            List<Employee> allEmployees = unitOfWork.EmployeesRepository.GetAll();
            ViewBag.AllEmployees = new SelectList(allEmployees, "ID", "Name");


            

            // add the Categories to the Viewbag
            
            List<Visitor> allVisitors = unitOfWork.VisitorsRepository.GetAll();
            ViewBag.AllVisitors = new SelectList(allVisitors, "ID", "Name");

            // create the viewmodel, based on the Restaurant from the database
            HotelsViewModel model = new HotelsViewModel();
            Hotel dbhotel = unitOfWork.HotelsRepository.GetByID(id);
            if (dbhotel != null)
            {
                model = new HotelsViewModel(dbhotel);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(HotelsViewModel viewModel)
        {
            if (viewModel == null)
            {
                // this should not be possible, but just in case, validate
                TempData["ErrorMessage"] = "Ups, a serious error occured: No Viewmodel.";
                return RedirectToAction("Index");
            }

            // get the item from the database by ID
            UnitOfWork unitOfWork = new UnitOfWork();
            Hotel dbHotel = unitOfWork.HotelsRepository.GetByID(viewModel.HotelID);

            // if there is no object in the DB, this is a new item -> will create a new one
            if (dbHotel == null)
            {
                dbHotel = new Hotel();
                
            }

            // update the DB object from the viewModel 
            viewModel.UpdateDBModel(dbHotel);

            // save the image to the local folder if there is an uploaded image
            // we have to generate a unique file name, to avoid duplication when the same image is uploaded for another restaurant
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                // check if the uploaded file is a valid file and only then proceed
                if (file.ContentLength > 0 && string.IsNullOrEmpty(file.FileName) == false)
                {
                    string imagesPath = Server.MapPath(Constants.ImagesDirectory);
                    string uniqueFileName = string.Format("{0}_{1}", DateTime.Now.Ticks, file.FileName);
                    string savedFileName = Path.Combine(imagesPath, Path.GetFileName(uniqueFileName));
                    dbHotel.ImageName = uniqueFileName;
                    file.SaveAs(savedFileName);
 
                }
            }

            unitOfWork.HotelsRepository.Save(dbHotel);
            unitOfWork.Save();

            TempData["Message"] = "The restaurant was saved successfully";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id = 0)
        {
            UnitOfWork unitOfWork = new UnitOfWork();

            bool isDeleted = false;

            Hotel dbHotel = unitOfWork.HotelsRepository.GetByID(id);
            if (dbHotel != null)
            {
                
                unitOfWork.HotelsRepository.Delete(dbHotel);
                isDeleted = unitOfWork.Save() > 0;
            }

            if (isDeleted == false)
            {
                TempData["ErrorMessage"] = "Could not find a restaurant with ID = " + id;
            }
            else
            {
                TempData["Message"] = "The restaurant was deleted successfully";
            }

            return RedirectToAction("Index");
        }

        //public ActionResult Details(int id = 0)
        //{
        //    UnitOfWork unitOfWork = new UnitOfWork();

        //    // get the DB object
        //    Hotel dbHotel = unitOfWork.HotelsRepository.GetByID(id);
        //    if (dbHotel == null)
        //    {
        //        // when we have RedirectToAction, we can not use Viewbag - so we use a TempData!
        //        TempData["ErrorMessage"] = "Could not find a restaurant with ID = " + id;
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        // create the view model
        //        HotelsViewModel model = new HotelsViewModel(dbHotel);
        //        return View(model);
        //    }
        //}
    }
}
