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
        public static List<TicketComment> GetTicketComments()
        {
            List<TicketComment> comments = new List<TicketComment>();
            using (BugTrackerContext db = new BugTrackerContext())
            {
                if (db.TicketComments is not null)
                {
                    comments = db.TicketComments.ToList();
                }
            }
            return comments;
        }
    }
}
