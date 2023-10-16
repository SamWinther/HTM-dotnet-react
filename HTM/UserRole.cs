using System;
using System.Collections.Generic;

namespace HTMbackend.HTM;

public partial class UserRole
{
    public int Id { get; set; }
    public string Comment { get; set; }= String.Empty;

    public int UserID { get; set; }
    public int ProjectID { get; set; }
    public int RoleID { get; set; }
    public virtual User? User { get; set; }
    public virtual Project? Project { get; set; }
    public virtual Role? Role { get; set; }
}
