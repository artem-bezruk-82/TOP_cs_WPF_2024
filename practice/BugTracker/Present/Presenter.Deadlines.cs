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

        public static (bool result, string message) UpdateDB(TicketDeadline deadline)
        {
            string msg = string.Empty;
            using (BugTrackerContext db = new BugTrackerContext())
            {
                if (db.TicketDeadlines is null)
                {
                    msg = "Table does not exist in database";
                    return (false, msg);
                }

                TicketDeadline deadlineToUpdate = db.TicketDeadlines.First(d => d.Id == deadline.Id);
                if (deadlineToUpdate != null)
                {
                    try
                    {
                        deadlineToUpdate.Name = deadline.Name;
                        deadlineToUpdate.DaysToResolve = deadline.DaysToResolve;
                        deadlineToUpdate.PriorityId = deadline.PriorityId;
                        deadline.TypeId = deadlineToUpdate.TypeId;
                        deadlineToUpdate.ServiceComponentId = deadline.ServiceComponentId;
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        msg = ex.InnerException?.Message ?? string.Empty;
                        return (false, msg);
                    }
                }
                else
                {
                    msg = "Entry you are trying to update does not exists in database";
                    return (false, msg);
                }
            }
            return (true, msg);
        }
    }
}
