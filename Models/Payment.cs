using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Houseiana.Models;

public class Payment
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string BookingId { get; set; }

    [Required]
    public string UserId { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; set; }

    [MaxLength(10)]
    public string Currency { get; set; } = "QAR";

    [MaxLength(50)]
    public string Method { get; set; } // card, paypal, bank_transfer

    [MaxLength(50)]
    public string Status { get; set; } // pending, processing, completed, failed, refunded

    [Required]
    [MaxLength(255)]
    public string TransactionId { get; set; }

    public object? Metadata { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("BookingId")]
    public virtual Booking Booking { get; set; }

    [ForeignKey("UserId")]
    public virtual User User { get; set; }

    public virtual Invoice? Invoice { get; set; }
}