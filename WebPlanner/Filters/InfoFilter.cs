using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPlanner.Data;
using WebPlanner.Models;

namespace WebPlanner.Filters
{
    public class InfoFilter : Attribute, IActionFilter
    {
        private ApplicationDbContext db;
        public InfoFilter(ApplicationDbContext context)
        {
            db = context;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string username = context.HttpContext.User.Identity.Name;
            if (username == null)
            { }
            else { 
                ApplicationUser _user = db.Users.Where(p=>p.UserName==username).First();
            
           
            if ( _user.Name == null || _user.Surname == null || _user.PhoneNumber == null)
            {
                context.Result = new RedirectResult("/Home/PersonalData");
                return;
            }
            }
        }


        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
