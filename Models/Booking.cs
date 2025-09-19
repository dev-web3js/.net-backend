using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Houseiana.Models;

public class Booking
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [MaxLength(100)]
    public string BookingCode { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string ListingId { get; set; }

    [Required]
    public string GuestId { get; set; }

    public string? HostId { get; set; }

    [Column(TypeName = "date")]
    public DateTime CheckIn { get; set; }

    [Column(TypeName = "date")]
    public DateTime CheckOut { get; set; }

    public int Adults { get; set; }
    public int Children { get; set; } = 0;
    public int Infants { get; set; } = 0;
    public int Pets { get; set; } = 0;

    [Column(TypeName = "decimal(10,2)")]
    public decimal NightlyRate { get; set; }

    public int TotalNights { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Subtotal { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal CleaningFee { get; set; } = 0;

    [Column(TypeName = "decimal(10,2)")]
    public decimal ServiceFee { get; set; } = 0;

    [Column(TypeName = "decimal(10,2)")]
    public decimal Taxes { get; set; } = 0;

    [Column(TypeName = "decimal(10,2)")]
    public decimal Discount { get; set; } = 0;

    [Column(TypeName = "decimal(10,2)")]
    public decimal TotalPrice { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal? TotalAmount { get; set; }

    public int? Guests { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal? SecurityDeposit { get; set; }

    public bool DepositPaid { get; set; } = false;
    public bool DepositRefunded { get; set; } = false;
    public DateTime? DepositRefundedAt { get; set; }

    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

    [MaxLength(100)]
    public string? PaymentMethod { get; set; }

    [MaxLength(255)]
    public string? PaymentId { get; set; }

    public DateTime? PaidAt { get; set; }

    [MaxLength(255)]
    public string? PaymentIntentId { get; set; }

    public BookingStatus Status { get; set; } = BookingStatus.Pending;
    public DateTime? ConfirmedAt { get; set; }
    public DateTime? CancelledAt { get; set; }

    [MaxLength(100)]
    public string? CancelledBy { get; set; }

    public string? CancelReason { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime? ActualCheckIn { get; set; }
    public DateTime? ActualCheckOut { get; set; }

    public string? GuestMessage { get; set; }
    public string? SpecialRequests { get; set; }

    [MaxLength(50)]
    public string? ArrivalTime { get; set; }

    [MaxLength(50)]
    public string? GuestPhone { get; set; }

    [MaxLength(255)]
    public string? GuestEmail { get; set; }

    public string? HostMessage { get; set; }
    public string? HostNotes { get; set; }

    [MaxLength(100)]
    public string? Source { get; set; }

    [MaxLength(45)]
    public string? Ip { get; set; }

    public string? UserAgent { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("GuestId")]
    public virtual User Guest { get; set; }

    [ForeignKey("HostId")]
    public virtual User? Host { get; set; }

    [ForeignKey("ListingId")]
    public virtual Listing Listing { get; set; }

    public virtual ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();
    public virtual Review? Review { get; set; }
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    public virtual Payment? Payment { get; set; }
    public virtual Invoice? Invoice { get; set; }
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}