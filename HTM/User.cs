﻿using System;
using System.Collections.Generic;

namespace HTMbackend.HTM;

public partial class User
{
    public int Id { get; set; }
    public string FirstName{ get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int OrganizationId { get; set; }
    public virtual Organization? Organization { get; set; }
    public virtual ICollection<UserRole> UserRoles{ get; set; } = new List<UserRole>();

}
