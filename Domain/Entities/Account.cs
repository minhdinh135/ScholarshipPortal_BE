using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Account : BaseEntity
{
    [MaxLength(100)]
    public string Username { get; set; }

    [MaxLength(100)]
    public string Email { get; set; }

    [MaxLength(100)]
    public string PhoneNumber { get; set; }

    [MaxLength(100)]
    public string HashedPassword { get; set; }

    [MaxLength(100)] 
    public string? Address { get; set; }

    [MaxLength(1024)]
    public string? AvatarUrl { get; set; }

    public bool LoginWithGoogle { get; set; }
    
    public DateTime? SubscriptionEndDate { get; set; }

    [MaxLength(100)]
    public string Status { get; set; }
    
    public int? SubscriptionId { get; set; }
    
    public Subscription? Subscription { get; set; }

    public int RoleId { get; set; }

    public Role Role { get; set; }
    
    public int? FunderId { get; set; }
    
    public Account? Funder { get; set; }
    
    public ICollection<Account>? Experts { get; set; }
    
    public ExpertProfile? ExpertProfile { get; set; }
    
    public Wallet? Wallet { get; set; }

    public ApplicantProfile ApplicantProfile { get; set; }

    public FunderProfile? FunderProfile { get; set; }

    public ProviderProfile ProviderProfile { get; set; }

    public ICollection<Chat> SenderChats { get; set; }

    public ICollection<Chat> ReceiverChats { get; set; }

    public ICollection<Notification> Notifications { get; set; }

    public ICollection<Service> Services { get; set; }

    public ICollection<Request> Requests { get; set; }
    
    public ICollection<ScholarshipProgram>? FunderScholarshipPrograms { get; set; }
    
    public ICollection<ExpertForProgram>? AssignedPrograms { get; set; }

    public ICollection<Application>? Applications { get; set; }

    public ICollection<Review>? ApplicationReviews { get; set; }

    public ICollection<Feedback>? Feedbacks { get; set; }
}
