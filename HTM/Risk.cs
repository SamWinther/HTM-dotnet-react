using System;
using System.Collections.Generic;

namespace HTMbackend.HTM;

public partial class Risk
{
    public int Id { get; set; }

    public string Hazard { get; set; } = null!;

    public string DesignChar { get; set; } = null!;

    public int LifeCycle { get; set; }

    public string Scenario { get; set; } = null!;

    public string HazardSit { get; set; } = null!;

    public string Harm { get; set; } = null!;

    public int ProbPre { get; set; }

    public int SeverityPre { get; set; }

    public int RiskPre { get; set; }

    public string RcmRational { get; set; } = null!;

    public int ProbPast { get; set; }

    public int SeverityPast { get; set; }

    public int RiskPast { get; set; }

    public byte[]? Complete { get; set; }

    public virtual ICollection<Rcm2risk> Rcm2risks { get; set; } = new List<Rcm2risk>();
}
