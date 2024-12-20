using System;
using System.Collections.Generic;

namespace Labb2HenrikVuDB.Domain;

public partial class BokRecensioner
{
    public int RecensionsId { get; set; }

    public int UserId { get; set; }

    public string Isbn { get; set; } = null!;

    public string Recension { get; set; } = null!;

    public virtual Böcker IsbnNavigation { get; set; } = null!;

    public virtual Beställare User { get; set; } = null!;
}
