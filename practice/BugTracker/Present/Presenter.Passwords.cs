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
        public static List<LoginsPassword> GetPasswords()
        {
            List<LoginsPassword> passwords = new List<LoginsPassword>();
            using (BugTrackerContext db = new BugTrackerContext())
            {
                if (db.LoginsPasswords is not null)
                {
                    passwords = db.LoginsPasswords.ToList();
                }
            }
            return passwords;
        }
    }
}
