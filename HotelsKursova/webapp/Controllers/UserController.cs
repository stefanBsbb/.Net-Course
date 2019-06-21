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

    // GET: User
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            // if there are some notification message from other actions, then set them in the viewbag
            // so that we display them in the screen
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            ViewBag.Message = TempData["Message"];

            // 1. get all categories from the DB
            UserRepository userRepository = new UserRepository();
            List<User> allUsers = userRepository.GetAll();

            // initialize the model for the View
            List<UserViewModel> model = new List<UserViewModel>();

            // 2. convert all User objects to ViewModel objects
            foreach (User user in allUsers)
            {
                UserViewModel newItem = new UserViewModel(user);
                model.Add(newItem);
            }

            // 3. pass the viewModel to the view
            return View(model);
        }

        public ActionResult Edit(int id = 0)
        {
            // get the User to edit
            UserRepository userRepository = new UserRepository();
            User user = userRepository.GetByID(id);

            UserViewModel model = new UserViewModel();
            if (user != null)
            {
                // create the viewModel from the User
                model = new UserViewModel(user);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel userEdit)
        {
            // find the User in the DB
            UserRepository userRepository = new UserRepository();
            User dbUser = userRepository.GetByID(userEdit.UserID);

            // if there is no object in the DB, this is a new item -> will create a new one
            if (dbUser == null)
            {
                dbUser = new User();
            }

            // update the DB object from the viewModel 
            userEdit.UpdateDbModel(dbUser);

            userRepository.Save(dbUser);


            TempData["Message"] = "The user was saved successfully";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            bool isDeleted = false;
            UserRepository userRepository = new UserRepository();

            User dbUser = userRepository.GetByID(id);
            if (dbUser != null)
            {

                userRepository.Delete(dbUser);
                isDeleted = userRepository.Save2() > 0;
            }

            if (isDeleted == false)
            {
                TempData["ErrorMessage"] = "Could not find a user with ID = " + id;
            }
            else
            {
                TempData["Message"] = "The user was deleted successfully";
            }

            return RedirectToAction("Index");
        }
    }
}

