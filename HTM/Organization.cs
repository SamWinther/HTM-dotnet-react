using System;
using System.Collections.Generic;

namespace HTMbackend.HTM;

public partial class Organization
{
    public int Id { get; set; }

    public string Name{ get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
