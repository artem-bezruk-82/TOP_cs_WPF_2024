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
        public static List<Service> GetServices(bool includeRelatedComponents = false)
        {
            List<Service> result = new List<Service>();
            using (BugTrackerContext db = new BugTrackerContext())
            {
                if (db.Services is not null)
                {
                    IQueryable<Service>? services = 
                        includeRelatedComponents ? db.Services.Include(s => s.ServiceComponents) : db.Services;

                    if (services is not null && services.Any()) 
                    {
                        result = db.Services.ToList();
                    }
                }
            }
            return result;
        }

        public static Service? GetService(int serviceId)
        {
            Service? service = null;
            using (BugTrackerContext db = new BugTrackerContext())
            {
                service = db.Services.First(s => s.Id == serviceId);
            }
            return service;
        }

        public static (bool result, string message) AddToDB(Service service)
        {
            string msg = string.Empty;
            using (BugTrackerContext db = new BugTrackerContext())
            {
                if (db.Services != null)
                {
                    try
                    {
                        db.Services.Add(service);
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

        public static (bool result, string message) UpdateDB(Service service)
        {
            string msg = string.Empty;
            using (BugTrackerContext db = new BugTrackerContext())
            {
                if (db.Services is null)
                {
                    msg = "Table does not exist in database";
                    return (false, msg);
                }

                Service serviceToUpdate = db.Services.First(s => s.Id == service.Id);
                if (serviceToUpdate != null)
                {
                    try
                    {
                        serviceToUpdate.Name = service.Name;
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

        public static int Delete(Service[] services)
        {
            int counter = 0;
            if (services.Length > 0)
            {
                foreach (Service service in services)
                {
                    counter += Delete(service);
                }
            }
            return counter;
        }

        public static int Delete(Service service)
        {
            int counter = 0;
            if (service != null)
            {
                using (BugTrackerContext db = new BugTrackerContext())
                {
                    Service? serviceToDelete = db.Services.First(s => s.Id == service.Id);
                    if (serviceToDelete != null)
                    {
                        db.Services.Remove(serviceToDelete);
                        counter++;
                    }
                    db.SaveChanges();
                }
            }
            return counter;
        }
    }
}
