using BugTracker.DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Present
{
    public static partial class Presenter
    {
        public static List<TicketPriority> GetTicketPriorities(bool includeRelatedTickets = false)
        {
            List<TicketPriority> result = new List<TicketPriority>();
            using (BugTrackerContext db = new BugTrackerContext())
            {
                if (db.TicketPriorities is not null)
                {
                    IQueryable<TicketPriority>? ticketPriorities =
                        includeRelatedTickets ? db.TicketPriorities.Include(p => p.Tickets) : db.TicketPriorities;

                    if (ticketPriorities is not null && ticketPriorities.Any())
                    {
                        result = ticketPriorities.ToList();
                    }
                }
            }
            return result;
        }

        public static TicketPriority? GetTicketPriority(int priorityId)
        {
            TicketPriority? ticketPriority = null;
            using (BugTrackerContext db = new BugTrackerContext())
            {
                ticketPriority = db.TicketPriorities.First(p => p.Id == priorityId);
            }
            return ticketPriority;
        }

        public static (bool result, string message) AddToDB(TicketPriority? priority)
        {
            string msg = string.Empty;
            if (priority is null) 
            {
                msg = "Null entries are not allowed";
                return (false, msg);
            }
            using (BugTrackerContext db = new BugTrackerContext())
            {
                if (db.TicketPriorities != null)
                {
                    try
                    {
                        db.TicketPriorities.Add(priority);
                        db.SaveChanges();
                    }
                    catch (Exception exc)
                    {
                        msg = exc.InnerException?.Message ?? string.Empty;
                        return (false, msg);
                    }
                }
                else
                {
                    msg = "Table does not exist in database";
                    return (false, msg);
                }
            }
            return (true, msg);
        }

        public static (bool result, string message) UpdateDB(TicketPriority priority)
        {
            string msg = string.Empty;
            using (BugTrackerContext db = new BugTrackerContext())
            {
                if (db.TicketPriorities is null)
                {
                    msg = "Table does not exist in database";
                    return (false, msg);
                }

                TicketPriority priorityToUpdate = db.TicketPriorities.First(p => p.Id == priority.Id);
                if (priorityToUpdate != null)
                {
                    try
                    {
                        priorityToUpdate.Name = priority.Name;
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

        public static int Delete(TicketPriority[] priorities)
        {
            int counter = 0;
            if (priorities.Length > 0)
            {
                foreach (TicketPriority priority in priorities)
                {
                    counter += Delete(priority);
                }
            }
            return counter;
        }

        public static int Delete(TicketPriority priority)
        {
            int counter = 0;
            if (priority != null)
            {
                using (BugTrackerContext db = new BugTrackerContext())
                {
                    TicketPriority? priorityToDelete = db.TicketPriorities.First(p => p.Id == priority.Id);
                    if (priorityToDelete != null)
                    {
                        db.TicketPriorities.Remove(priorityToDelete);
                        counter++;
                    }
                    db.SaveChanges();
                }
            }
            return counter;
        }
    }
}
