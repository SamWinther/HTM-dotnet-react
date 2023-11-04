using System;
using System.Collections.Generic;

namespace HTMbackend.HTM;
public enum EnumRole { Admin= 1, Author=2, Reviewer=3, Approver=4, ProjectManager=5, SuperUser=6, Reader=7}
public partial class UserRole
{
    public int Id { get; set; }
    public string Comment { get; set; }= String.Empty;

    public int UserID { get; set; }
    public int ProjectID { get; set; }
    public EnumRole EnumRole { get; set; }
    public virtual User? User { get; set; }
    public virtual Project? Project { get; set; }
    //public virtual Role? Role { get; set; }
}
