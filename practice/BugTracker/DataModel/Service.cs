using System;
using System.Collections.Generic;

namespace BugTracker.DataModel;

public partial class Service
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ServiceComponent> ServiceComponents { get; set; } = new List<ServiceComponent>();
}
