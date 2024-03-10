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
        public static List<TicketHistory> GetTicketHistories()
        {
            List<TicketHistory> histories = new List<TicketHistory>();
            using (BugTrackerContext db = new BugTrackerContext())
            {
                if (db.Tickets is not null)
                {
                    histories = db.TicketHistories.ToList();
                }
            }
            return histories;
        }
    }
}
