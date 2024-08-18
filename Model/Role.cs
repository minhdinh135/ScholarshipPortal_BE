using Microsoft.AspNetCore.Identity;

namespace Model;

public partial class Role : IdentityRole<int>
{
    public Role() : base()
    { 
    }

    public Role(string roleName)
    {
        Name = roleName;
    }
}
