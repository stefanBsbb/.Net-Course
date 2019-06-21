using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repositories;
using DataAccess;
using webapp.Models;
namespace webapp.Controllers
{
    public class VisitorsController : Controller
    {
        public ActionResult Index()
        {
            // if there are some notification message from other actions, then set them in the viewbag
            // so that we display them in the screen
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            ViewBag.Message = TempData["Message"];

            // 1. get all categories from the DB
            UnitOfWork unitOfWork = new UnitOfWork();
            List<Visitor> allVisitors = unitOfWork.VisitorsRepository.GetAll();

            // initialize the model for the View
            List<VisitorsViewModel> model = new List<VisitorsViewModel>();

            // 2. convert all Category objects to ViewModel objects
            foreach (Visitor visitor in allVisitors)
            {
                VisitorsViewModel newItem = new VisitorsViewModel(visitor);
                model.Add(newItem);
            }

            // 3. pass the viewModel to the view
            return View(model);
        }

        public ActionResult Edit(int id = 0)
        {
            // get the Category to edit
            UnitOfWork unitOfWork = new UnitOfWork();
            Visitor visitor = unitOfWork.VisitorsRepository.GetByID(id);

            VisitorsViewModel model = new VisitorsViewModel();
            if (visitor != null)
            {
                // create the viewModel from the Category
                model = new VisitorsViewModel(visitor);

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(VisitorsViewModel visitorEdit)
        {
            // find the Category in the DB
            UnitOfWork unitOfWork = new UnitOfWork();
            Visitor dbVisitor = unitOfWork.VisitorsRepository.GetByID(visitorEdit.ID);

            // if there is no object in the DB, this is a new item -> will create a new one
            if (dbVisitor == null)
            {
                dbVisitor = new Visitor();
            }

            // update the DB object from the viewModel 
            visitorEdit.UpdateDbModel(dbVisitor);

            unitOfWork.VisitorsRepository.Save(dbVisitor);
            unitOfWork.Save();

            TempData["Message"] = "The category was saved successfully";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.VisitorsRepository.DeleteByID(id);
            bool isDeleted = unitOfWork.Save() > 0;

            if (isDeleted == false)
            {
                TempData["ErrorMessage"] = "Could not find a category with ID = " + id;
            }
            else
            {
                TempData["Message"] = "The visitor was deleted successfully";
            }

            return RedirectToAction("Index");
        }
    }
}