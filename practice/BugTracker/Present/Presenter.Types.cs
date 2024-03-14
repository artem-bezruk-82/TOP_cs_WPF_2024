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
        public static List<TicketType> GetTicketTypes(bool includeRelatedTickets = false)
        {
            List<TicketType> result = new List<TicketType>();
            using (BugTrackerContext db = new BugTrackerContext())
            {
                if (db.TicketTypes is not null)
                {
                    IQueryable<TicketType>? ticketTypes =
                        includeRelatedTickets ? db.TicketTypes.Include(t => t.Tickets) : db.TicketTypes;

                    if (ticketTypes is not null && ticketTypes.Any())
                    {
                        result = ticketTypes.ToList();
                    }
                }
            }
            return result;
        }

        public static TicketType? GetTicketType(int typeId)
        {
            TicketType? ticketType = null;
            using (BugTrackerContext db = new BugTrackerContext())
            {
                ticketType = db.TicketTypes.First(t => t.Id == typeId);
            }
            return ticketType;
        }

        public static (bool result, string message) AddToDB(TicketType type)
        {
            string msg = string.Empty;
            using (BugTrackerContext db = new BugTrackerContext())
            {
                if (db.TicketTypes != null)
                {
                    try
                    {
                        db.TicketTypes.Add(type);
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

        public static (bool result, string message) UpdateDB(TicketType type)
        {
            string msg = string.Empty;
            using (BugTrackerContext db = new BugTrackerContext())
            {
                if (db.TicketTypes is null)
                {
                    msg = "Table does not exist in database";
                    return (false, msg);
                }

                TicketType typeToUpdate = db.TicketTypes.First(t => t.Id == type.Id);
                if (typeToUpdate != null)
                {
                    try
                    {
                        typeToUpdate.Name = type.Name;
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

        public static int Delete(TicketType [] types)
        {
            int counter = 0;
            if (types.Length > 0)
            {
                foreach (TicketType type in types)
                {
                    counter += Delete(type);
                }
            }
            return counter;
        }

        public static int Delete(TicketType type)
        {
            int counter = 0;
            if (type != null)
            {
                using (BugTrackerContext db = new BugTrackerContext())
                {
                    TicketType? typeToDelete = db.TicketTypes.First(t => t.Id == type.Id);
                    if (typeToDelete != null)
                    {
                        db.TicketTypes.Remove(typeToDelete);
                        counter++;
                    }
                    db.SaveChanges();
                }
            }
            return counter;
        }
    }
}
