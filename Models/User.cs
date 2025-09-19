using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Houseiana.Models;

public class User : IdentityUser
{
    [MaxLength(255)]
    public string? FirstName { get; set; }

    [MaxLength(255)]
    public string? LastName { get; set; }

    [MaxLength(255)]
    public string? Name { get; set; }

    [MaxLength(50)]
    public string? Phone { get; set; }

    [MaxLength(50)]
    public string? PhoneNumber { get; set; }

    public bool PhoneVerified { get; set; } = false;
    public DateTime? PhoneVerifiedAt { get; set; }

    public UserRole Role { get; set; } = UserRole.Guest;
    public bool IsHost { get; set; } = false;
    public bool IsAdmin { get; set; } = false;

    public VerificationStatus HostVerified { get; set; } = VerificationStatus.Unverified;
    public DateTime? HostSince { get; set; }

    public float? ResponseRate { get; set; }
    public int? ResponseTime { get; set; }

    [Column(TypeName = "decimal(12,2)")]
    public decimal? TotalEarnings { get; set; } = 0;

    [MaxLength(255)]
    public string? GovernmentId { get; set; }

    [MaxLength(100)]
    public string? GovernmentIdType { get; set; }

    public DateTime? IdVerifiedAt { get; set; }

    public object? PropertyDocs { get; set; }

    [MaxLength(255)]
    public string? TradeLicense { get; set; }

    [MaxLength(255)]
    public string? BankName { get; set; }

    [MaxLength(100)]
    public string? AccountNumber { get; set; }

    [MaxLength(255)]
    public string? AccountHolderName { get; set; }

    [MaxLength(100)]
    public string? Iban { get; set; }

    [MaxLength(50)]
    public string? SwiftCode { get; set; }

    public DateTime? BankVerifiedAt { get; set; }

    public string? Bio { get; set; }

    [MaxLength(255)]
    public string? ProfileImage { get; set; }

    [MaxLength(255)]
    public string? CoverImage { get; set; }

    [MaxLength(10)]
    public string Language { get; set; } = "en";

    [MaxLength(10)]
    public string Currency { get; set; } = "QAR";

    public bool EmailNotifications { get; set; } = true;
    public bool SmsNotifications { get; set; } = true;
    public bool PushNotifications { get; set; } = true;
    public bool MarketingEmails { get; set; } = false;

    public bool TwoFactorEnabled { get; set; } = false;

    [MaxLength(255)]
    public string? TwoFactorSecret { get; set; }

    public bool EmailVerified { get; set; } = false;
    public DateTime? EmailVerifiedAt { get; set; }

    [MaxLength(100)]
    public string Timezone { get; set; } = "Asia/Qatar";

    public object? NotificationSettings { get; set; }
    public object? PrivacySettings { get; set; }

    public DateTime? LastLoginAt { get; set; }
    public DateTime? LastActiveAt { get; set; }
    public DateTime? SuspendedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public virtual ICollection<Booking> GuestBookings { get; set; } = new List<Booking>();
    public virtual ICollection<Booking> HostBookings { get; set; } = new List<Booking>();
    public virtual ICollection<FavoriteListing> Favorites { get; set; } = new List<FavoriteListing>();
    public virtual ICollection<HostPayout> Payouts { get; set; } = new List<HostPayout>();
    public virtual ICollection<Listing> Listings { get; set; } = new List<Listing>();
    public virtual ICollection<ListingDraft> ListingDrafts { get; set; } = new List<ListingDraft>();
    public virtual ICollection<Message> SentMessages { get; set; } = new List<Message>();
    public virtual ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
    public virtual ICollection<Conversation> Conversations1 { get; set; } = new List<Conversation>();
    public virtual ICollection<Conversation> Conversations2 { get; set; } = new List<Conversation>();
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    public virtual ICollection<Review> ReceivedReviews { get; set; } = new List<Review>();
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    public virtual ICollection<SearchHistory> Searches { get; set; } = new List<SearchHistory>();
    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public virtual ICollection<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();
    public virtual ICollection<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();
    public virtual TaxInformation? TaxInformation { get; set; }
    public virtual ICollection<PhoneVerification> PhoneVerifications { get; set; } = new List<PhoneVerification>();
    public virtual ICollection<EmailVerification> EmailVerifications { get; set; } = new List<EmailVerification>();
    public virtual ICollection<Invoice> UserInvoices { get; set; } = new List<Invoice>();
    public virtual ICollection<Invoice> HostInvoices { get; set; } = new List<Invoice>();
    public virtual ICollection<CoHost> CoHostRoles { get; set; } = new List<CoHost>();
    public virtual ICollection<CoHostInvitation> SentCoHostInvites { get; set; } = new List<CoHostInvitation>();
    public virtual ICollection<CoHost> ReceivedCoHostInvites { get; set; } = new List<CoHost>();
    public virtual ICollection<KycDocument> KycDocuments { get; set; } = new List<KycDocument>();
    public virtual KycVerification? KycVerification { get; set; }
    public virtual ICollection<KycVerification> KycReviews { get; set; } = new List<KycVerification>();
    public virtual ICollection<KycDocument> DocumentReviews { get; set; } = new List<KycDocument>();
    public virtual ICollection<KycAuditLog> KycAuditLogs { get; set; } = new List<KycAuditLog>();
    public virtual ICollection<KycAuditLog> PerformedAudits { get; set; } = new List<KycAuditLog>();
}