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
        public static List<AccessRightsLevel> GetAccessLevels()
        {
            List<AccessRightsLevel> accessLevels = new List<AccessRightsLevel>();
            using (BugTrackerContext db = new BugTrackerContext())
            {
                if (db.AccessRightsLevels is not null)
                {
                    accessLevels = db.AccessRightsLevels.ToList();
                }
            }
            return accessLevels;
        }
    }
}
