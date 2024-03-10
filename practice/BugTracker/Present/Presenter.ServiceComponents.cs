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
        public static List<ServiceComponent> GetServiceComponents()
        {
            List<ServiceComponent> serviceComponents = new List<ServiceComponent>();
            using (BugTrackerContext db = new BugTrackerContext())
            {
                if (db.ServiceComponents is not null)
                {
                    serviceComponents = db.ServiceComponents.ToList();
                }
            }
            return serviceComponents;
        }
    }
}
