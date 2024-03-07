using System;
using System.Collections.Generic;

namespace BugTracker.DataModel;

public partial class TicketHistory
{
    public int Id { get; set; }

    public string ChangedDate { get; set; } = null!;

    public int? TicketId { get; set; }

    public int? TypeId { get; set; }

    public int? StatusId { get; set; }

    public int? PriorityId { get; set; }

    public int? ServiceComponentId { get; set; }

    public string? Deadline { get; set; }

    public virtual TicketPriority? Priority { get; set; }

    public virtual ServiceComponent? ServiceComponent { get; set; }

    public virtual TicketStatus? Status { get; set; }

    public virtual Ticket? Ticket { get; set; }

    public virtual TicketType? Type { get; set; }
}
