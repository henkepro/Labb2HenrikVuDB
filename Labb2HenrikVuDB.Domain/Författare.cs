using System;
using System.Collections.Generic;

namespace Labb2HenrikVuDB.Domain;

public partial class Författare
{
    public int Id { get; set; }

    public string Förnamn { get; set; } = null!;

    public string Efternamn { get; set; } = null!;

    public DateOnly Födelsedatum { get; set; }

    public DateOnly? Dödsdatum { get; set; }

    public virtual ICollection<Böcker> Isbn13 { get; set; } = new List<Böcker>();
}
