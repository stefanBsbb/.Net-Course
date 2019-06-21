using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using webapp.Models;
using webapp.Helpers;
namespace webapp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            List<Hotel> allHotels = unitOfWork.HotelsRepository.GetAll();

            HomeViewModel model = new HomeViewModel(allHotels);
            return View(model);
        }

        public ActionResult Search(string searchVal)
        {
            UnitOfWork unitOfWork = new UnitOfWork();

            // just in case prevent NullPointerException
            if (searchVal == null)
            {
                searchVal = string.Empty;
            }

            // compare all words to lower case
            searchVal = searchVal.ToLower();

            // use lambda expression to filter the matched objects
            List<Hotel> foundHotels = unitOfWork.HotelsRepository.GetAll()
                .Where(c => c.HotelName.ToLower().Contains(searchVal)
                    || c.WebAddress.ToLower().Contains(searchVal))
                .ToList();

            // convert the DB objects to ViewModel objects
            List<SearchViewModel> model = new List<SearchViewModel>();
            foreach (Hotel dbhotel in foundHotels)
            {
                SearchViewModel modelItem = new SearchViewModel(dbhotel);
                model.Add(modelItem);
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Login")]
        public ActionResult Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //here we have to check if the username exists in the DB
                UserRepository userRepository = new UserRepository();
                User dbUser = userRepository.GetUserByNameAndPassword(viewModel.Username, viewModel.Password);

                bool isUserExists = dbUser != null;
                if (isUserExists)
                {
                    LoginUserSession.Current.SetCurrentUser(dbUser.ID, dbUser.UserName, dbUser.IsAdministrator);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username and/or password!");
                }
            }

            return View();
        }

            // if we are here, this means there is some validation error and we have to show the login screen again
 

        public ActionResult Logout()
        {
            LoginUserSession.Current.Logout();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

            [HttpPost]
        public ActionResult Register(RegisterViewModelcs viewModel)
        {
            if (ModelState.IsValid)
            {
                UserRepository userRepository = new UserRepository();
                //check if the user already exists in the DB
                User existingDbUser = userRepository.GetUserByName(viewModel.Username);
                if (existingDbUser != null)
                {
                    ModelState.AddModelError("", "This user is already registered in the system!");
                    return View();
                }

                User dbUser = new DataAccess.User();
                viewModel.UpdateDbModel(dbUser);
                // dbUser.PasswordHash = viewModel.pass
                //save the user to the D
                userRepository.RegisterUser(dbUser, viewModel.Password);
                userRepository.Save(dbUser);
                userRepository.Save2();

                TempData["Message"] = "User was registered successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}
