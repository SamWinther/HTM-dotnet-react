using System;
using System.Collections.Generic;

namespace HTMbackend.HTM;

public partial class Rcmtype
{
    public int Id { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<Rcm> Rcms { get; set; } = new List<Rcm>();
}
