using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPlanner.Models.EventViewModel
{
    public class EventViewJson
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public List<FieldView> description { get; set; }
        public int UsersSubscribedCount { get; set; }
        public string[] Usernames { get; set; }
        public int MaxUsers { get; set; }
    }
    public class FieldView
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}

