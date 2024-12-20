using System;
using System.Collections.Generic;

namespace Labb2HenrikVuDB.Domain;

public partial class Butiker
{
    public int Id { get; set; }

    public string Namn { get; set; } = null!;

    public string Adress { get; set; } = null!;

    public string Ort { get; set; } = null!;

    public int PostNummer { get; set; }

    public virtual ICollection<Beställningar> Beställningar { get; set; } = new List<Beställningar>();

    public virtual ICollection<LagerSaldo> LagerSaldo { get; set; } = new List<LagerSaldo>();
}
