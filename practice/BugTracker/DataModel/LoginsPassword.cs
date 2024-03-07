using System;
using System.Collections.Generic;

namespace BugTracker.DataModel;

public partial class LoginsPassword
{
    public int Id { get; set; }

    public int? LoginId { get; set; }

    public string Password { get; set; } = null!;

    public virtual UserLogin? Login { get; set; }
}
