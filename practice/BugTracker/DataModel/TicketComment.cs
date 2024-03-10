using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.DataModel;

public partial class TicketComment
{
    public int Id { get; set; }

    public int? LoginId { get; set; }

    public int? TicketId { get; set; }

    [Column(TypeName = "text")]
    public DateTime CreatedDate { get; set; }

    public string Text { get; set; } = null!;

    public virtual UserLogin? Login { get; set; }

    public virtual Ticket? Ticket { get; set; }
}
