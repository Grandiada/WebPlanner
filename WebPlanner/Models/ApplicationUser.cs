using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebPlanner.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string About { get; set; }
        public ICollection<EventUsersSigned> EventUsersSigned { get; set; }
  
        public ApplicationUser()
        {
            EventUsersSigned = new List<EventUsersSigned>();
        }
    }
  
}
