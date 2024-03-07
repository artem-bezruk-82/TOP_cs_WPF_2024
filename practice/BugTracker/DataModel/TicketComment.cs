using System;
using System.Collections.Generic;

namespace BugTracker.DataModel;

public partial class TicketComment
{
    public int Id { get; set; }

    public int? LoginId { get; set; }

    public int? TicketId { get; set; }

    public string CreatedDate { get; set; } = null!;

    public string Text { get; set; } = null!;

    public virtual UserLogin? Login { get; set; }

    public virtual Ticket? Ticket { get; set; }
}
