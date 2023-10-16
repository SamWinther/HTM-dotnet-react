using System;
using System.Collections.Generic;

namespace HTMbackend.HTM;

public partial class Project
{
    public int Id { get; set; }
    public string Name{ get; set; } = null!;
    public int OrganizationId { get; set; }
    public virtual Organization? Organization { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    public virtual ICollection<Rcm> Rcms { get; set; } = new List<Rcm>();
    public virtual ICollection<Risk> Risks { get; set; } = new List<Risk>();

}
