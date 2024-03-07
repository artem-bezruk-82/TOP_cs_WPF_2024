using System;
using System.Collections.Generic;

namespace BugTracker.DataModel;

public partial class ServiceComponent
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? ServiceId { get; set; }

    public virtual Service? Service { get; set; }

    public virtual ICollection<TicketDeadline> TicketDeadlines { get; set; } = new List<TicketDeadline>();

    public virtual ICollection<TicketHistory> TicketHistories { get; set; } = new List<TicketHistory>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
