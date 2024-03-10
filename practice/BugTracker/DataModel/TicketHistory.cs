using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.DataModel;

public partial class TicketHistory
{
    public int Id { get; set; }

    [Column(TypeName = "text")]
    public DateTime ChangedDate { get; set; }

    public int? TicketId { get; set; }

    public int? TypeId { get; set; }

    public int? StatusId { get; set; }

    public int? PriorityId { get; set; }

    public int? ServiceComponentId { get; set; }

    [Column(TypeName = "text")]
    public DateTime? Deadline { get; set; }

    public virtual TicketPriority? Priority { get; set; }

    public virtual ServiceComponent? ServiceComponent { get; set; }

    public virtual TicketStatus? Status { get; set; }

    public virtual Ticket? Ticket { get; set; }

    public virtual TicketType? Type { get; set; }
}
