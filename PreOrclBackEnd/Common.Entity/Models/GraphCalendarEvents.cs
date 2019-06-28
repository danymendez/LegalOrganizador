using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entity.Models
{
    public class GraphCalendarEvents
    {
        public GraphCalendar GraphCalendar { get; set; }

        public List<GraphEvents> GraphEvents { get; set; }
    }
}
