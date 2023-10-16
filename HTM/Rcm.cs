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
    public int ProjectId { get; set; }
    public virtual Project? Project{ get; set; }
    public virtual ICollection<Rcm2risk> Rcm2risks { get; set; } = new List<Rcm2risk>();
    public virtual Rcmtype RcmtypeNavigation { get; set; } = null!;
}
