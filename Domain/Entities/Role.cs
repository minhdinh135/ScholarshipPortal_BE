using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

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
