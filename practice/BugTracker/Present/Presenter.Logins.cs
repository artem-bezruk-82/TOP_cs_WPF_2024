using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTracker.DataModel;

namespace BugTracker.Present
{
    public static partial class Presenter
    {
        public static List<UserLogin> GetUserLogins()
        {
            List<UserLogin> userLogins = new List<UserLogin>();
            using (BugTrackerContext db = new BugTrackerContext())
            {
                if (db.UserLogins is not null)
                {
                    userLogins = db.UserLogins.ToList();
                }
            }
            return userLogins;
        }
    }
}
