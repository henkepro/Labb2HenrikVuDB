using System;
using System.Collections.Generic;

namespace Labb2HenrikVuDB.Domain;

public partial class OrderDetaljer
{
    public int OrderDetaljId { get; set; }

    public int OrderId { get; set; }

    public string Isbn { get; set; } = null!;

    public int Antal { get; set; }

    public decimal Pris { get; set; }

    public virtual Böcker IsbnNavigation { get; set; } = null!;

    public virtual Beställningar Order { get; set; } = null!;
}
