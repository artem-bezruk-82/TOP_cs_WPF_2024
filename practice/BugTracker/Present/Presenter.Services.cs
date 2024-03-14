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
        public static List<Service> GetServices()
        {
            List<Service> services = new List<Service>();
            using (BugTrackerContext db = new BugTrackerContext())
            {
                if (db.Services is not null)
                {
                    services = db.Services.ToList();
                }
            }
            return services;
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
    }
}
