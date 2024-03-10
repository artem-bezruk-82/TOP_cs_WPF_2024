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
        public static List<TicketAction> GetTicketActions()
        {
            List<TicketAction> ticketActions = new List<TicketAction>();
            using (BugTrackerContext db = new BugTrackerContext())
            {
                if (db.TicketActions is not null)
                {
                    ticketActions = db.TicketActions.ToList();
                }
            }
            return ticketActions;
        }
    }
}
