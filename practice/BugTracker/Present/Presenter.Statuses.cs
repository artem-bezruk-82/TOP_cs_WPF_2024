using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTracker.DataModel;

namespace BugTracker.Present
{
    public static partial class Presenter
    {
        public static List<TicketStatus> GetTicketStatuses() 
        {
            List<TicketStatus> ticketStatuses = new List<TicketStatus>();
            using (BugTrackerContext db = new BugTrackerContext()) 
            {
                if (db.TicketStatuses is not null) 
                {
                    ticketStatuses = db.TicketStatuses.ToList();
                }
            }
            return ticketStatuses;
        }
    }
}
