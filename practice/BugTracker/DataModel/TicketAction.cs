using System;
using System.Collections.Generic;

namespace BugTracker.DataModel;

public partial class TicketAction
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? DetailedDescription { get; set; }

    public string CreatedDate { get; set; } = null!;

    public int? TicketId { get; set; }

    public int? LoginId { get; set; }

    public virtual UserLogin? Login { get; set; }

    public virtual Ticket? Ticket { get; set; }
}
