using System;
using System.Collections.Generic;

namespace HTMbackend.HTM;

public partial class Rcm
{
    public int Id { get; set; }

    public int Rcmtype { get; set; }

    public string Rcmtext { get; set; } = null!;

    public bool NewRiskFromRcm { get; set; }

    public string Implement { get; set; } = null!;

    public string VerOfEff { get; set; } = null!;

    public virtual Rcmtype RcmtypeNavigation { get; set; } = null!;
}
