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
    }
}
