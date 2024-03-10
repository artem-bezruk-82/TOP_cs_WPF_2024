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
        public static List<User> GetUsers()
        {
            List<User> users = new List<User>();
            using (BugTrackerContext db = new BugTrackerContext())
            {
                if (db.Users is not null)
                {
                    users = db.Users.ToList();
                }
            }
            return users;
        }
    }
}
