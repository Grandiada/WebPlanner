using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebPlanner.Data;
using WebPlanner.Models;
using Microsoft.AspNetCore.Hosting;
using WebPlanner.Models.EventViewModel;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;

namespace WebPlanner.Controllers
{
    public class EventController : Controller
    {

        private ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        IHostingEnvironment _appEnvironment;
        public EventController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            _signInManager = signInManager;
            _userManager = userManager;
            db = context;
        }
        [Authorize(Roles = "verified")]
        public void NewEvent(NewEventModel newEvent)
        {
            Event CalendarEvent = new Event
            {
                Date = DateTime.ParseExact(newEvent.date, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Title=newEvent.title,
                MaxUsers=newEvent.MaxUsers,
                fields=new List<Field>()
            };
            foreach (var item in newEvent.Field)
            {
                if (item.Key == null||item.Value==null)
                {
                }
                else
                {
                    Field temp = new Field
                    {
                        Key=item.Key,
                        Value=item.Value
                    };
                    CalendarEvent.fields.Add(temp);
                }
            }
            db.Events.Add(CalendarEvent);
            db.SaveChanges();
        }


        public async Task<JsonResult> getEventData(DateTime start, DateTime end)
        {
            TimeSpan ts = end.Subtract(start);
            DateTime middleTime = start.AddMinutes(ts.TotalMinutes / 2);
            List<Event> events = await db.Events.Where(p => p.Date.Month == middleTime.Month).Include(f => f.fields).Include("EventUsersSigned.User").ToListAsync();
            List<FieldView> tempDic = new List<FieldView>();
            List<EventViewJson> result= new List<EventViewJson>();
            foreach (var item in events)
            {
                tempDic.Clear();
                foreach(var field in item.fields)
                {
                    tempDic.Add(new FieldView{ Key=field.Key,Value=field.Value });
                }
                var users = item.EventUsersSigned.Select(p => p.User.UserName).ToArray();
                EventViewJson temp = new EventViewJson
                {
                    Id = item.Id,
                    title = item.Title,
                    start = (item.Date).ToString("yyyy-MM-dd"),
                    description = tempDic,
                    Usernames = users,
                    UsersSubscribedCount = users.Count(),
                    MaxUsers=item.MaxUsers
                    
                };
                result.Add(temp);
            }

            var json = Json(result);

            return json;
        }

        public async Task<string> signProjectAsync(int id)
        {
            Event eventCalend = db.Events.Find(id);
            ApplicationUser currentUser = await _userManager.GetUserAsync(User);
            var Sign = new EventUsersSigned { Event = eventCalend, User = currentUser };
            if (db.Events.Any(p => p.EventUsersSigned.Any(f=>f== Sign)))
            {

            }
            else 
            if(eventCalend.MaxUsers==eventCalend.SignedUsersCount)
            {

            }
            else
            {
                eventCalend.SignedUsersCount += 1;
                eventCalend.EventUsersSigned.Add(Sign);
                db.Events.Update(eventCalend);
                db.SaveChanges();
            }
            
            return "u a signed";
        }
    }
}