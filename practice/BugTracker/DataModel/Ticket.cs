using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.DataModel;

public partial class Ticket
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? Resolution { get; set; }

    public int? TypeId { get; set; }

    public int? StatusId { get; set; }

    public int? PriorityId { get; set; }

    public int? ServiceComponentId { get; set; }

    public int? OpenedByLoginId { get; set; }

    [Column(TypeName = "text")]
    public DateTime CreatedDate { get; set; }

    public int? DeadlineId { get; set; }

    [Column(TypeName = "text")]
    public DateTime? DeadlineDate { get; set; }

    public virtual TicketDeadline? Deadline { get; set; }

    public virtual UserLogin? OpenedByLogin { get; set; }

    public virtual TicketPriority? Priority { get; set; }

    public virtual ServiceComponent? ServiceComponent { get; set; }

    public virtual TicketStatus? Status { get; set; }

    public virtual ICollection<TicketAction> TicketActions { get; set; } = new List<TicketAction>();

    public virtual ICollection<TicketComment> TicketComments { get; set; } = new List<TicketComment>();

    public virtual ICollection<TicketHistory> TicketHistories { get; set; } = new List<TicketHistory>();

    public virtual ICollection<TicketsAssignment> TicketsAssignments { get; set; } = new List<TicketsAssignment>();

    public virtual TicketType? Type { get; set; }
}
