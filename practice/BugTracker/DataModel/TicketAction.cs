using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.DataModel;

public partial class TicketAction
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? DetailedDescription { get; set; }

    [Column(TypeName = "text")]
    public DateTime CreatedDate { get; set; }

    public int? TicketId { get; set; }

    public int? LoginId { get; set; }

    public virtual UserLogin? Login { get; set; }

    public virtual Ticket? Ticket { get; set; }
}
