using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Houseiana.Models;

// Additional models from the Prisma schema

public class ListingDraft
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string HostId { get; set; }
    public int CurrentStep { get; set; } = 1;
    public int TotalSteps { get; set; } = 7;
    public object FormData { get; set; }
    public int LastStep { get; set; } = 1;
    public bool Completed { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("HostId")]
    public virtual User Host { get; set; }
}

public class Availability
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ListingId { get; set; }
    [Column(TypeName = "date")]
    public DateTime Date { get; set; }
    public bool Available { get; set; } = true;
    [Column(TypeName = "decimal(10,2)")]
    public decimal? Price { get; set; }
    public int? MinNights { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("ListingId")]
    public virtual Listing Listing { get; set; }
}

public class CalendarBlock
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ListingId { get; set; }
    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }
    [Column(TypeName = "date")]
    public DateTime EndDate { get; set; }
    public string? Reason { get; set; }
    public string? Note { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("ListingId")]
    public virtual Listing Listing { get; set; }
}

public class Conversation
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Participant1Id { get; set; }
    public string Participant2Id { get; set; }
    public string? ListingId { get; set; }
    public string? BookingId { get; set; }
    public string? LastMessageId { get; set; }
    public DateTime? LastMessageAt { get; set; }
    public int UnreadCount1 { get; set; } = 0;
    public int UnreadCount2 { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("Participant1Id")]
    public virtual User Participant1 { get; set; }
    [ForeignKey("Participant2Id")]
    public virtual User Participant2 { get; set; }
    [ForeignKey("ListingId")]
    public virtual Listing? Listing { get; set; }
    [ForeignKey("BookingId")]
    public virtual Booking? Booking { get; set; }
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}

public class Message
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ConversationId { get; set; }
    public string SenderId { get; set; }
    public string ReceiverId { get; set; }
    public string Content { get; set; }
    public List<string> Attachments { get; set; } = new List<string>();
    public bool IsRead { get; set; } = false;
    public DateTime? ReadAt { get; set; }
    public bool IsEdited { get; set; } = false;
    public DateTime? EditedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("ConversationId")]
    public virtual Conversation Conversation { get; set; }
    [ForeignKey("SenderId")]
    public virtual User Sender { get; set; }
    [ForeignKey("ReceiverId")]
    public virtual User Receiver { get; set; }
}

public class Transaction
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? BookingId { get; set; }
    public string Type { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "QAR";
    public string Status { get; set; }
    public string Method { get; set; }
    public string? Reference { get; set; }
    public object? Metadata { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public DateTime? FailedAt { get; set; }
    public string? FailureReason { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("BookingId")]
    public virtual Booking? Booking { get; set; }
}

public class HostPayout
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string HostId { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "QAR";
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
    public List<string> BookingIds { get; set; } = new List<string>();
    public int BookingsCount { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public decimal Earnings { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public decimal ServiceFee { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public decimal Taxes { get; set; } = 0;
    [Column(TypeName = "decimal(10,2)")]
    public decimal Adjustments { get; set; } = 0;
    public PayoutStatus Status { get; set; } = PayoutStatus.Pending;
    public string Method { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public string? Reference { get; set; }
    public string? FailureReason { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("HostId")]
    public virtual User Host { get; set; }
}

public class FavoriteListing
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; }
    public string ListingId { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("ListingId")]
    public virtual Listing Listing { get; set; }
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
}

public class SearchHistory
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; }
    public object Query { get; set; }
    public int ResultsCount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
}

public class Notification
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; }
    public string? BookingId { get; set; }
    public string Type { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public object? Data { get; set; }
    public object? Metadata { get; set; }
    public bool Read { get; set; } = false;
    public bool IsRead { get; set; } = false;
    public DateTime? ReadAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
    [ForeignKey("BookingId")]
    public virtual Booking? Booking { get; set; }
}

public class Session
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; }
    public string Token { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public DateTime LastActivity { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
}

public class PriceHistory
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ListingId { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public decimal? NightlyPrice { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public decimal? WeeklyPrice { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public decimal MonthlyPrice { get; set; }
    public string? Reason { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime? ValidUntil { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("ListingId")]
    public virtual Listing Listing { get; set; }
}

public class AmenityCategory
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }
    public string? Icon { get; set; }
    public int Order { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public virtual ICollection<Amenity> Amenities { get; set; } = new List<Amenity>();
}

public class Amenity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string CategoryId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Icon { get; set; }
    public bool IsPremium { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("CategoryId")]
    public virtual AmenityCategory Category { get; set; }
}

public class PropertyRule
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Icon { get; set; }
    public string Type { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

public class CurrencyRate
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public CurrencyCode FromCurrency { get; set; }
    public CurrencyCode ToCurrency { get; set; }
    [Column(TypeName = "decimal(10,6)")]
    public decimal Rate { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime? ValidUntil { get; set; }
    public string? Source { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

public class PropertyView
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ListingId { get; set; }
    public string? ViewerId { get; set; }
    public string SessionId { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public string? Referrer { get; set; }
    public int? ViewDuration { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class PropertyDocument
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ListingId { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public int? Size { get; set; }
    public string? MimeType { get; set; }
    public bool IsVerified { get; set; } = false;
    public DateTime? VerifiedAt { get; set; }
    public string? VerifiedBy { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

public class MaintenanceRecord
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ListingId { get; set; }
    public string Type { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public decimal? Cost { get; set; }
    public CurrencyCode Currency { get; set; } = CurrencyCode.QAR;
    public string? PerformedBy { get; set; }
    public DateTime PerformedAt { get; set; }
    public DateTime? NextDue { get; set; }
    public object? Documents { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

public class Invoice
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Number { get; set; }
    public string BookingId { get; set; }
    public string? PaymentId { get; set; }
    public string UserId { get; set; }
    public string HostId { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "QAR";
    public string Status { get; set; } // draft, sent, paid, overdue, cancelled
    public DateTime? DueDate { get; set; }
    public DateTime? SentAt { get; set; }
    public DateTime? PaidAt { get; set; }
    public object? Metadata { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("BookingId")]
    public virtual Booking Booking { get; set; }
    [ForeignKey("PaymentId")]
    public virtual Payment? Payment { get; set; }
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
    [ForeignKey("HostId")]
    public virtual User Host { get; set; }
}

public class PaymentMethod
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; }
    public string CardType { get; set; } // Visa, Mastercard, Amex, etc.
    public string MaskedCardNumber { get; set; } // **** **** **** 1234
    public string Last4 { get; set; } // Last 4 digits
    public int ExpiryMonth { get; set; }
    public int ExpiryYear { get; set; }
    public string CardholderName { get; set; }
    public bool IsDefault { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
}

public class BankAccount
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; }
    public string BankName { get; set; }
    public string AccountNumber { get; set; }
    public string AccountHolderName { get; set; }
    public string? Iban { get; set; }
    public string? SwiftCode { get; set; }
    public string? RoutingNumber { get; set; }
    public string? BranchCode { get; set; }
    public string? BankAddress { get; set; }
    public string AccountType { get; set; } = "savings"; // savings, checking, business
    public string Currency { get; set; } = "QAR";
    public bool IsDefault { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public bool IsVerified { get; set; } = false;
    public DateTime? VerifiedAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
}

public class TaxInformation
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; }
    public string TaxIdType { get; set; } // "ssn", "ein", "qid", "cr", "vat"
    public string TaxId { get; set; }
    public string LegalName { get; set; }
    public string PhoneNumber { get; set; } = "";
    public string? BusinessType { get; set; }
    public string TaxCountry { get; set; } = "QA";
    public string TaxAddress { get; set; }
    public string TaxCity { get; set; }
    public string? TaxState { get; set; }
    public string? TaxPostalCode { get; set; }
    public bool SubjectToBackupWith { get; set; } = false;
    public bool ExemptFromBackupWith { get; set; } = false;
    [Column(TypeName = "decimal(5,2)")]
    public decimal? TaxWithholdingRate { get; set; }
    public object? TaxDocuments { get; set; }
    public string? W9FormUrl { get; set; }
    public string? W8FormUrl { get; set; }
    public string? QatarTaxCertUrl { get; set; }
    public bool IsComplete { get; set; } = false;
    public bool IsVerified { get; set; } = false;
    public DateTime? VerifiedAt { get; set; }
    public string? ReviewedBy { get; set; }
    public string? FatcaStatus { get; set; }
    public string? CrsStatus { get; set; }
    public bool RequiresReporting { get; set; } = true;
    public DateTime? SubmittedAt { get; set; }
    public DateTime? LastReviewedAt { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
}

public class PhoneVerification
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; }
    public string PhoneNumber { get; set; }
    public string Code { get; set; } // 6-digit verification code
    public string Method { get; set; } // "sms" or "whatsapp"
    public bool IsVerified { get; set; } = false;
    public DateTime? VerifiedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public int Attempts { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
}

public class EmailVerification
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; }
    public string Email { get; set; }
    public string Code { get; set; } // 6-digit verification code
    public string Token { get; set; } // verification token for email links
    public string Type { get; set; } = "verification"; // "verification" or "change"
    public bool IsVerified { get; set; } = false;
    public DateTime? VerifiedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public int Attempts { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
}

public class CoHost
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ListingId { get; set; }
    public string CoHostId { get; set; }
    public string InvitedById { get; set; }
    public object Permissions { get; set; }
    public CoHostStatus Status { get; set; } = CoHostStatus.Pending;
    public DateTime? AcceptedAt { get; set; }
    public DateTime? DeclinedAt { get; set; }
    public DateTime? RemovedAt { get; set; }
    public string Role { get; set; } = "co_host";
    public string? Title { get; set; }
    public string? Responsibilities { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime? LastActiveAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("ListingId")]
    public virtual Listing Listing { get; set; }
    [ForeignKey("CoHostId")]
    public virtual User CoHostUser { get; set; }
    [ForeignKey("InvitedById")]
    public virtual User InvitedBy { get; set; }
    public virtual ICollection<CoHostActivity> Activities { get; set; } = new List<CoHostActivity>();
}

public class CoHostInvitation
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Email { get; set; }
    public string ListingId { get; set; }
    public string InviterId { get; set; }
    public string Token { get; set; }
    public object Permissions { get; set; }
    public string Role { get; set; } = "co_host";
    public string? Title { get; set; }
    public string? Message { get; set; }
    public InvitationStatus Status { get; set; } = InvitationStatus.Pending;
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
    public DateTime? AcceptedAt { get; set; }
    public DateTime? DeclinedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public string? DeclineReason { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("ListingId")]
    public virtual Listing Listing { get; set; }
    [ForeignKey("InviterId")]
    public virtual User Inviter { get; set; }
}

public class CoHostActivity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string CoHostId { get; set; }
    public string Action { get; set; }
    public string Description { get; set; }
    public object? Metadata { get; set; }
    public string? RelatedId { get; set; }
    public string? RelatedType { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("CoHostId")]
    public virtual CoHost CoHost { get; set; }
}

public class KycVerification
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Nationality { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string Country { get; set; } = "Qatar";
    public string? PostalCode { get; set; }
    public KycDocumentType DocumentType { get; set; }
    public string DocumentNumber { get; set; }
    public string DocumentCountry { get; set; } = "Qatar";
    public DateTime? DocumentExpiry { get; set; }
    public KycStatus Status { get; set; } = KycStatus.Pending;
    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ReviewedAt { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public DateTime? RejectedAt { get; set; }
    public string? ReviewerId { get; set; }
    public string? RejectionReason { get; set; }
    public string? InternalNotes { get; set; }
    public KycRiskLevel RiskLevel { get; set; } = KycRiskLevel.Low;
    public object? ComplianceFlags { get; set; }
    public string VerificationCode { get; set; } = Guid.NewGuid().ToString();
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
    [ForeignKey("ReviewerId")]
    public virtual User? Reviewer { get; set; }
}

public class KycDocument
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; }
    public KycDocumentType Type { get; set; }
    public KycDocumentCategory Category { get; set; } = KycDocumentCategory.Identity;
    public string FileName { get; set; }
    public string OriginalName { get; set; }
    public int FileSize { get; set; }
    public string MimeType { get; set; }
    public string FileUrl { get; set; }
    public string? ThumbnailUrl { get; set; }
    public string? DocumentNumber { get; set; }
    public DateTime? IssueDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string? IssuingAuthority { get; set; }
    public KycDocumentStatus Status { get; set; } = KycDocumentStatus.Pending;
    public DateTime? VerifiedAt { get; set; }
    public DateTime? RejectedAt { get; set; }
    public string? RejectionReason { get; set; }
    public string? OcrText { get; set; }
    public float? OcrConfidence { get; set; }
    public object? AiVerification { get; set; }
    public string? ReviewerId { get; set; }
    public string? ReviewNotes { get; set; }
    public string? EncryptionKey { get; set; }
    public string Checksum { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
    [ForeignKey("ReviewerId")]
    public virtual User? Reviewer { get; set; }
}

public class KycAuditLog
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; }
    public KycAuditAction Action { get; set; }
    public string Entity { get; set; }
    public string EntityId { get; set; }
    public object? OldValues { get; set; }
    public object? NewValues { get; set; }
    public string? Changes { get; set; }
    public string? PerformedBy { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public string? Reason { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
    [ForeignKey("PerformedBy")]
    public virtual User? Performer { get; set; }
}

public class PasswordResetToken
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Email { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}