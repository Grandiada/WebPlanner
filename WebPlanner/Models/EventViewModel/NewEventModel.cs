using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPlanner.Models.EventViewModel
{
    public class NewEventModel
    {
        public string title { get; set; }
        public string date { get; set; }
        public int MaxUsers { get; set; }
        public FieldViewModel[] Field { get; set; }
    }
}
