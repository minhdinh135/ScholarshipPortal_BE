using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public partial class User : IdentityUser<int> 
{
    public string Address { get; set; }

    public string Avatar { get; set; }

    public string Gender { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Status { get; set; }

    public string Role { get; set; }
}
