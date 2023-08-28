using System;
using System.Collections.Generic;

namespace HTMbackend.HTM;

public partial class Rcm2risk
{
    public int Id { get; set; }

    public int? RiskId { get; set; }

    public int? RcmId { get; set; }

    public virtual Rcm? Rcm { get; set; }

    public virtual Risk? Risk { get; set; }
}
