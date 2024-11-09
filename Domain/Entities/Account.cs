namespace Domain.Entities;

public class Account : BaseEntity
{
    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? HashedPassword { get; set; }

    public string? Address { get; set; }

    public string? AvatarUrl { get; set; }

    public bool? LoginWithGoogle { get; set; }

    public string? Status { get; set; }

    public int? RoleId { get; set; }

    public Role? Role { get; set; }
    
    public Wallet? Wallet { get; set; }

    public ApplicantProfile? ApplicantProfile { get; set; }

    public FunderProfile? FunderProfile { get; set; }

    public ProviderProfile ProviderProfile { get; set; }

    public ICollection<Chat> SenderChats { get; set; }

    public ICollection<Chat> ReceiverChats { get; set; }

    public ICollection<Notification> Notifications { get; set; }

    public ICollection<Service> Services { get; set; }

    public ICollection<Request> Requests { get; set; }
    
    public ICollection<ScholarshipProgram>? FunderScholarshipPrograms { get; set; }

    public ICollection<Application>? Applications { get; set; }

    public ICollection<ApplicationReview>? ApplicationReviews { get; set; }

    public ICollection<Feedback>? Feedbacks { get; set; }
}
