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
        public static List<AccessRightsLevel> GetAccessLevels(bool includeRelatedLogins = false)
        {
            List<AccessRightsLevel> result = new List<AccessRightsLevel>();
            using (BugTrackerContext db = new BugTrackerContext())
            {
                if (db.AccessRightsLevels is not null)
                {
                    IQueryable<AccessRightsLevel>? accessRightsLevels =
                        includeRelatedLogins ? db.AccessRightsLevels.Include(l => l.UserLogins) : db.AccessRightsLevels;

                    if (accessRightsLevels is not null && accessRightsLevels.Any())
                    {
                        result = accessRightsLevels.ToList();
                    }
                }
            }
            return result;
        }

        public static AccessRightsLevel? GetAccessLevel(int levelId)
        {
            AccessRightsLevel? level = null;
            using (BugTrackerContext db = new BugTrackerContext())
            {
                level = db.AccessRightsLevels.First(l => l.Id == levelId);
            }
            return level;
        }

        public static (bool result, string message) AddToDB(AccessRightsLevel level)
        {
            string msg = string.Empty;
            using (BugTrackerContext db = new BugTrackerContext())
            {
                if (db.AccessRightsLevels != null)
                {
                    try
                    {
                        db.AccessRightsLevels.Add(level);
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

        public static (bool result, string message) UpdateDB(AccessRightsLevel level)
        {
            string msg = string.Empty;
            using (BugTrackerContext db = new BugTrackerContext())
            {
                if (db.AccessRightsLevels is null)
                {
                    msg = "Table does not exist in database";
                    return (false, msg);
                }

                AccessRightsLevel levelToUpdate = db.AccessRightsLevels.First(l => l.Id == level.Id);
                if (levelToUpdate != null)
                {
                    try
                    {
                        levelToUpdate.Name = level.Name;
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

        public static int Delete(AccessRightsLevel[] accessRightsLevels)
        {
            int counter = 0;
            if (accessRightsLevels.Length > 0)
            {
                foreach (AccessRightsLevel level in accessRightsLevels)
                {
                    counter += Delete(level);
                }
            }
            return counter;
        }

        public static int Delete(AccessRightsLevel level)
        {
            int counter = 0;
            if (level != null)
            {
                using (BugTrackerContext db = new BugTrackerContext())
                {
                    AccessRightsLevel? levelToDelete = db.AccessRightsLevels.First(l => l.Id == level.Id);
                    if (levelToDelete != null)
                    {
                        db.AccessRightsLevels.Remove(levelToDelete);
                        counter++;
                    }
                    db.SaveChanges();
                }
            }
            return counter;
        }
    }
}
