using System.Collections;

namespace Domain.Entities;

public class Account : BaseEntity
{
    public string? Username { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? HashedPassword { get; set; }

    public string? Address { get; set; }

    public string? Avatar { get; set; }

    public string? Gender { get; set; }

    public int? RoleId { get; set; }

    public Role? Role { get; set; }
    
    public ApplicantProfile? ApplicantProfile { get; set; }
    
    public ICollection<ScholarshipProgram>? FunderCreatedScholarshipPrograms { get; set; }
    
    public ICollection<ScholarshipProgram>? ProviderAssignedScholarshipPrograms { get; set; }
    
    public ICollection<Application>? Applications { get; set; }
    
    public ICollection<Review>? Reviews { get; set; }
    
    public ICollection<Feedback>? FunderFeedbacks { get; set; }
    
    public ICollection<Feedback>? ProviderFeedbacks { get; set; }
}
