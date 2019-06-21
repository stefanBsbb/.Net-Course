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
    public class EmployeesController : Controller
    {
        public ActionResult Index()
        {
            // if there are some notification message from other actions, then set them in the viewbag
            // so that we display them in the screen
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            ViewBag.Message = TempData["Message"];

            // 1. get all categories from the DB
            UnitOfWork unitOfWork = new UnitOfWork();
            List<Employee> allEmployees = unitOfWork.EmployeesRepository.GetAll();
            


            // initialize the model for the View
            List<EmployeeViewModel> model = new List<EmployeeViewModel>();

            // 2. convert all Category objects to ViewModel objects
            foreach (Employee employee in allEmployees)
            {
                EmployeeViewModel newItem = new EmployeeViewModel(employee);
                model.Add(newItem);
            }

            // 3. pass the viewModel to the view
            return View(model);
        }

        public ActionResult Edit(int id = 0)
        {
            // get the Category to edit
            UnitOfWork unitOfWork = new UnitOfWork();
            Employee allemployees = unitOfWork.EmployeesRepository.GetByID(id);


            EmployeeViewModel model = new EmployeeViewModel();
            if (allemployees != null)
            {
                // create the viewModel from the Category
                model = new EmployeeViewModel(allemployees);

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeViewModel employeeEdit)
        {
            // find the Category in the DB
            UnitOfWork unitOfWork = new UnitOfWork();
            Employee dbEmployee = unitOfWork.EmployeesRepository.GetByID(employeeEdit.ID);

            // if there is no object in the DB, this is a new item -> will create a new one
            if (dbEmployee == null)
            {
                dbEmployee = new Employee();
            }

            // update the DB object from the viewModel 
            employeeEdit.UpdateDbModel(dbEmployee);

            unitOfWork.EmployeesRepository.Save(dbEmployee);
            unitOfWork.Save();

            TempData["Message"] = "The category was saved successfully";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.EmployeesRepository.DeleteByID(id);
            bool isDeleted = unitOfWork.Save() > 0;

            if (isDeleted == false)
            {
                TempData["ErrorMessage"] = "Could not find a category with ID = " + id;
            }
            else
            {
                TempData["Message"] = "The category was deleted successfully";
            }

            return RedirectToAction("Index");
        }
    }
}
