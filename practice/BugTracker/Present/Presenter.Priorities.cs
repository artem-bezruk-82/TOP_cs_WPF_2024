using BugTracker.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Present
{
    public static partial class Presenter
    {
        public static List<TicketPriority> GetTicketPriorities()
        {
            List<TicketPriority> ticketPriorities = new List<TicketPriority>();
            using (BugTrackerContext db = new BugTrackerContext())
            {
                if (db.TicketPriorities is not null)
                {
                    ticketPriorities = db.TicketPriorities.ToList();
                }
            }
            return ticketPriorities;
        }
    }
}
