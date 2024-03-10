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
        public static List<TicketDeadline> GetTicketDeadlines()
        {
            List<TicketDeadline> deadlines = new List<TicketDeadline>();
            using (BugTrackerContext db = new BugTrackerContext())
            {
                if (db.TicketDeadlines is not null)
                {
                    deadlines = db.TicketDeadlines.ToList();
                }
            }
            return deadlines;
        }
    }
}
