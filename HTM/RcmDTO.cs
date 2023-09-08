using System;
using System.Collections.Generic;

namespace HTMbackend.HTM;

enum YesNo {Yes , No}

public partial class RcmDTO
{
    public string? Rcmtype { get; set; }

    public string Rcmtext { get; set; } = null!;

    public string? NewRiskFromRcm { get; set; }

    public string Implement { get; set; } = null!;

    public string VerOfEff { get; set; } = null!;

}
