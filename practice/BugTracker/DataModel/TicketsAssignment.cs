using System;
using System.Collections.Generic;

namespace BugTracker.DataModel;

public partial class TicketsAssignment
{
    public int Id { get; set; }

    public int? LoginId { get; set; }

    public int? TicketId { get; set; }

    public virtual UserLogin? Login { get; set; }

    public virtual Ticket? Ticket { get; set; }
}
