using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input.Manipulations;
using BugTracker.DataModel;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Present
{
    public static partial class Presenter
    {
        public static List<TicketStatus> GetTicketStatuses(bool includeRelatedTickets = false) 
        {
            List<TicketStatus> result = new List<TicketStatus>();
            using (BugTrackerContext db = new BugTrackerContext()) 
            {
                if (db.TicketStatuses is not null) 
                {
                    IQueryable<TicketStatus>? ticketStatuses = 
                        includeRelatedTickets ? db.TicketStatuses.Include(s => s.Tickets) : db.TicketStatuses;

                    if (ticketStatuses is not null && ticketStatuses.Any())
                    {
                        result = ticketStatuses.ToList();
                    }
                }
            }
            return result; 
        }

        public static TicketStatus? GetTicketStatus(int statusId) 
        {
            TicketStatus? ticketStatus = null;
            using (BugTrackerContext db = new BugTrackerContext()) 
            {
                ticketStatus = db.TicketStatuses.First(s => s.Id == statusId);
            }
            return ticketStatus;
        }

        public static (bool result, string message) AddToDB(TicketStatus status) 
        {
            string msg = string.Empty;
            using (BugTrackerContext db = new BugTrackerContext()) 
            {
                if (db.TicketStatuses != null) 
                {
                    try
                    {
                        db.TicketStatuses.Add(status);
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

        public static (bool result, string message) UpdateDB(TicketStatus status) 
        {
            string msg = string.Empty;
            using (BugTrackerContext db = new BugTrackerContext()) 
            {
                if (db.TicketStatuses is null) 
                {
                    msg = "Table does not exist in database";
                    return (false, msg);
                }

                TicketStatus statusToUpdate = db.TicketStatuses.First(s => s.Id == status.Id);
                if (statusToUpdate != null) 
                {
                    try
                    {
                        statusToUpdate.Name = status.Name;
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

        public static int Delete(TicketStatus[] statuses)
        {
            int counter = 0;
            if (statuses.Length > 0) 
            {
                foreach (TicketStatus status in statuses)
                {
                    counter += Delete(status);
                }
            }
            return counter;
        }

        public static int Delete(TicketStatus status)
        {
            int counter = 0;
            if (status != null)
            {
                using (BugTrackerContext db = new BugTrackerContext())
                {
                    TicketStatus? statusToDelete = db.TicketStatuses.First(s => s.Id == status.Id);
                    if (statusToDelete != null)
                    {
                        db.TicketStatuses.Remove(statusToDelete);
                        counter++;
                    }
                    db.SaveChanges();
                }
            }
            return counter;
        }
    }
}
