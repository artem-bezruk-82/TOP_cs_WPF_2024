using System;
using System.Collections.Generic;

namespace BugTracker.DataModel;

public partial class TicketDeadline
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? TypeId { get; set; }

    public int? PriorityId { get; set; }

    public int? ServiceComponentId { get; set; }

    public int DaysToResolve { get; set; }

    public virtual TicketPriority? Priority { get; set; }

    public virtual ServiceComponent? ServiceComponent { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual TicketType? Type { get; set; }
}
