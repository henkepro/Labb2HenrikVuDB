using System;
using System.Collections.Generic;

namespace Labb2HenrikVuDB.Domain;

public partial class Böcker
{
    public string Isbn13 { get; set; } = null!;

    public string Titel { get; set; } = null!;

    public string Språk { get; set; } = null!;

    public decimal Pris { get; set; }

    public DateOnly Utgivningsdatum { get; set; }

    public virtual ICollection<BokRecensioner> BokRecensioner { get; set; } = new List<BokRecensioner>();

    public virtual ICollection<LagerSaldo> LagerSaldo { get; set; } = new List<LagerSaldo>();

    public virtual ICollection<OrderDetaljer> OrderDetaljer { get; set; } = new List<OrderDetaljer>();

    public virtual ICollection<Författare> Författare { get; set; } = new List<Författare>();

    public virtual ICollection<Genrar> Genrar { get; set; } = new List<Genrar>();
}
