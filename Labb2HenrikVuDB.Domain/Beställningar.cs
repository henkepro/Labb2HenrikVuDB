using System;
using System.Collections.Generic;

namespace Labb2HenrikVuDB.Domain;

public partial class Beställningar
{
    public int OrderId { get; set; }

    public int BeställareId { get; set; }

    public int ButikId { get; set; }

    public DateOnly Datum { get; set; }

    public virtual Beställare Beställare { get; set; } = null!;

    public virtual Butiker Butik { get; set; } = null!;

    public virtual ICollection<OrderDetaljer> OrderDetaljer { get; set; } = new List<OrderDetaljer>();
}
