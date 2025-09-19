using System;

namespace Houseiana.Models;

public enum UserRole
{
    Guest,
    Host,
    Both,
    Admin
}

public enum VerificationStatus
{
    Unverified,
    Pending,
    Verified,
    Rejected
}

public enum PropertyType
{
    Apartment,
    Villa,
    Studio,
    Townhouse,
    Penthouse,
    CompoundVilla,
    Room,
    Duplex,
    Chalet,
    FarmHouse,
    SharedRoom
}

public enum ListingStatus
{
    Draft,
    PendingReview,
    Approved,
    Active,
    Inactive,
    Suspended,
    Deleted
}

public enum Tier
{
    Standard,
    Gold,
    Premium
}

public enum BookingStatus
{
    Pending,
    Confirmed,
    Cancelled,
    Completed,
    NoShow,
    InProgress
}

public enum PaymentStatus
{
    Pending,
    Paid,
    PartiallyPaid,
    Refunded,
    Failed
}

public enum PayoutStatus
{
    Pending,
    Processing,
    Completed,
    Failed,
    Cancelled
}

public enum CurrencyCode
{
    QAR,
    USD,
    EUR,
    GBP,
    AED,
    SAR,
    KWD,
    BHD,
    OMR,
    EGP,
    INR,
    PKR,
    PHP
}

public enum RentalType
{
    ShortTerm,
    MidTerm,
    LongTerm
}

public enum FurnishingStatus
{
    FullyFurnished,
    SemiFurnished,
    Unfurnished
}

public enum CoHostStatus
{
    Pending,
    Accepted,
    Declined,
    Removed,
    Suspended
}

public enum InvitationStatus
{
    Pending,
    Accepted,
    Declined,
    Expired,
    Cancelled
}

public enum KycStatus
{
    Pending,
    UnderReview,
    Approved,
    Rejected,
    Expired,
    Flagged,
    Suspended
}

public enum KycDocumentType
{
    Passport,
    NationalId,
    DriversLicense,
    ResidencePermit,
    UtilityBill,
    BankStatement,
    RentalAgreement,
    GovernmentLetter,
    BusinessLicense,
    TaxCertificate,
    CommercialRegistration,
    TradeLicense,
    Visa,
    WorkPermit,
    BirthCertificate,
    MarriageCertificate,
    Selfie,
    DocumentSelfie,
    BankVerification,
    IbanCertificate
}

public enum KycDocumentCategory
{
    Identity,
    Address,
    Business,
    Financial,
    Legal,
    Photo,
    Supplementary
}

public enum KycDocumentStatus
{
    Pending,
    Processing,
    Verified,
    Rejected,
    Expired,
    Replaced
}

public enum KycRiskLevel
{
    Low,
    Medium,
    High,
    Critical
}

public enum KycAuditAction
{
    Submitted,
    Reviewed,
    Approved,
    Rejected,
    DocumentUploaded,
    DocumentVerified,
    DocumentRejected,
    StatusChanged,
    NotesAdded,
    Flagged,
    Unflagged,
    Escalated
}