using System;
using System.Collections.Generic;

namespace Labb2HenrikVuDB.Domain;

public partial class Genrar
{
    public int GenreId { get; set; }

    public string Namn { get; set; } = null!;

    public string Beskrivning { get; set; } = null!;

    public virtual ICollection<Böcker> Isbn { get; set; } = new List<Böcker>();
}
