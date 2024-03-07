using System;
using System.Collections.Generic;

namespace BugTracker.DataModel;

public partial class UserLogin
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public int? UserId { get; set; }

    public int? AccessLevelId { get; set; }

    public virtual AccessRightsLevel? AccessLevel { get; set; }

    public virtual LoginsPassword? LoginsPassword { get; set; }

    public virtual ICollection<TicketAction> TicketActions { get; set; } = new List<TicketAction>();

    public virtual ICollection<TicketComment> TicketComments { get; set; } = new List<TicketComment>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual ICollection<TicketsAssignment> TicketsAssignments { get; set; } = new List<TicketsAssignment>();

    public virtual User? User { get; set; }
}
