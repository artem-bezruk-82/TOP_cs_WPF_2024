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
        public static List<Ticket> GetTickets()
        {
            List<Ticket> tickets = new List<Ticket>();
            using (BugTrackerContext db = new BugTrackerContext())
            {
                if (db.Tickets is not null)
                {
                    tickets = db.Tickets.ToList();
                }
            }
            return tickets;
        }
    }
}
