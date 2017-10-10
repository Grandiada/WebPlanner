using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPlanner.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Field> fields { get; set; }
        public DateTime Date { get; set; }
        public int MaxUsers { get; set; }
        public int SignedUsersCount { get; set; }
        public ICollection<EventUsersSigned> EventUsersSigned { get; set; }
        public Event()
        {
            fields = new List<Field>();
            EventUsersSigned = new List<EventUsersSigned>();
        }
    }
}
