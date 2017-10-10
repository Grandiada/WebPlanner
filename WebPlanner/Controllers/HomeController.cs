using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebPlanner.Models;
using WebPlanner.Filters;
using WebPlanner.Models.ManageViewModels;
using WebPlanner.Data;
using Microsoft.AspNetCore.Identity;
using WebPlanner.Models.AccountViewModels;

namespace WebPlanner.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
       
        public HomeController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
           
            _signInManager = signInManager;
            _userManager = userManager;
            db = context;
        }

        [ServiceFilter(typeof(InfoFilter))]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
         public async Task<IActionResult> PersonalData()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> PersonalData(PersonalDataViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var phoneNumber = user.PhoneNumber;
            if (model.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, model.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
                }
            }

            var name = user.Name;
            if (model.Name != name || model.Name != "")
            {
                user.Name = model.Name;
                db.Users.Update(user);
            }
            var surname = user.Surname;
            if (model.Surname != surname || model.Surname != "")
            {
                user.Surname = model.Surname;
                db.Users.Update(user);
            }
            var about = user.About;
            if (model.About != about || model.About != "")
            {
                user.About = model.About;
                db.Users.Update(user);
            }
            db.SaveChanges();
            return Redirect("/Home/Index");
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
