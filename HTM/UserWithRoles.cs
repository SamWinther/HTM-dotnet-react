namespace HTMbackend.HTM
{
    public class ProjectRole
    {
        public string Project { get; set; } = null!;
        public string Role { get; set; } = null!;
        public int Projectid { get; set; } = 0;

    }
    public class UserWithRoles
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Organization { get; set; } = null!;
        public List<ProjectRole> Roles { get; set; } = new List<ProjectRole>();
    }
}
