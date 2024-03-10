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
        public static List<TicketType> GetTicketTypes()
        {
            List<TicketType> types = new List<TicketType>();
            using (BugTrackerContext db = new BugTrackerContext())
            {
                if (db.TicketTypes is not null)
                {
                    types = db.TicketTypes.ToList();
                }
            }
            return types;
        }
    }
}
