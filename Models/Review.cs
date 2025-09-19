using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Houseiana.Models;

public class Review
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string BookingId { get; set; }

    [Required]
    public string ReviewerId { get; set; }

    [Required]
    public string RevieweeId { get; set; }

    [Required]
    public string ListingId { get; set; }

    [Range(1, 5)]
    public int Overall { get; set; }

    [Range(1, 5)]
    public int? Cleanliness { get; set; }

    [Range(1, 5)]
    public int? Accuracy { get; set; }

    [Range(1, 5)]
    public int? CheckIn { get; set; }

    [Range(1, 5)]
    public int? Communication { get; set; }

    [Range(1, 5)]
    public int? Location { get; set; }

    [Range(1, 5)]
    public int? Value { get; set; }

    public string? PublicReview { get; set; }
    public string? PrivateNote { get; set; }
    public string? Response { get; set; }
    public DateTime? RespondedAt { get; set; }

    public List<string> Photos { get; set; } = new List<string>();
    public bool IsVerifiedStay { get; set; } = true;
    public bool IsHidden { get; set; } = false;
    public bool IsFlagged { get; set; } = false;
    public string? FlagReason { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("BookingId")]
    public virtual Booking Booking { get; set; }

    [ForeignKey("ListingId")]
    public virtual Listing Listing { get; set; }

    [ForeignKey("RevieweeId")]
    public virtual User Reviewee { get; set; }

    [ForeignKey("ReviewerId")]
    public virtual User Reviewer { get; set; }
}