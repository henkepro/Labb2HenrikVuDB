using System;
using System.Collections.Generic;

namespace Labb2HenrikVuDB.Domain;

public partial class BokStatistik
{
    public string Titel { get; set; } = null!;

    public string Isbn { get; set; } = null!;

    public int? AntalBeställningar { get; set; }

    public int AntalRecensioner { get; set; }

    public string Namn { get; set; } = null!;
}
