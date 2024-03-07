using System;
using System.Collections.Generic;

namespace BugTracker.DataModel;

public partial class AccessRightsLevel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<UserLogin> UserLogins { get; set; } = new List<UserLogin>();
}
