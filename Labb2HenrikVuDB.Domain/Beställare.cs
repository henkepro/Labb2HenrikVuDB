using System;
using System.Collections.Generic;

namespace Labb2HenrikVuDB.Domain;

public partial class Beställare
{
    public int UserId { get; set; }

    public string Namn { get; set; } = null!;

    public DateOnly Födelsedatum { get; set; }

    public string Adress { get; set; } = null!;

    public string Ort { get; set; } = null!;

    public int PostNummer { get; set; }

    public virtual ICollection<Beställningar> Beställningar { get; set; } = new List<Beställningar>();

    public virtual ICollection<BokRecensioner> BokRecensioners { get; set; } = new List<BokRecensioner>();
}
