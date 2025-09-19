using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Houseiana.Models;

public class Listing
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string HostId { get; set; }

    public ListingStatus Status { get; set; } = ListingStatus.Draft;

    [Required]
    [MaxLength(255)]
    public string Title { get; set; }

    [MaxLength(255)]
    public string? Slug { get; set; }

    [Required]
    public string Description { get; set; }

    public PropertyType PropertyType { get; set; }
    public bool EntirePlace { get; set; } = true;

    [MaxLength(100)]
    public string Country { get; set; } = "Qatar";

    [Required]
    [MaxLength(100)]
    public string City { get; set; }

    [MaxLength(100)]
    public string? Area { get; set; }

    [MaxLength(100)]
    public string? District { get; set; }

    public object Coordinates { get; set; }
    public object? Landmarks { get; set; }

    public int Bedrooms { get; set; } = 1;

    [Column(TypeName = "decimal(3,1)")]
    public decimal Bathrooms { get; set; } = 1;

    public int Beds { get; set; } = 1;
    public int? SquareMeters { get; set; }

    [MaxLength(50)]
    public string? FloorNumber { get; set; }

    public int? TotalFloors { get; set; }
    public int MaxGuests { get; set; } = 4;
    public int MaxAdults { get; set; } = 2;
    public int MaxChildren { get; set; } = 0;
    public int MaxInfants { get; set; } = 0;

    public object? InUnitFeatures { get; set; }
    public object? BuildingFacilities { get; set; }
    public object? CompoundAmenities { get; set; }
    public object? NearbyServices { get; set; }
    public object? SafetyFeatures { get; set; }
    public object? FamilyFeatures { get; set; }
    public object? AccessibilityFeatures { get; set; }
    public object? WorkFeatures { get; set; }

    public string? HouseRules { get; set; }
    public string? CheckInInstructions { get; set; }
    public object? WifiDetails { get; set; }
    public string? NeighborhoodInfo { get; set; }
    public string? TransitInfo { get; set; }
    public string? HostTips { get; set; }

    [MaxLength(10)]
    public string CheckInTime { get; set; } = "15:00";

    [MaxLength(10)]
    public string CheckOutTime { get; set; } = "11:00";

    public bool FlexibleCheckIn { get; set; } = false;
    public bool SelfCheckIn { get; set; } = false;

    [Column(TypeName = "decimal(10,2)")]
    public decimal? NightlyPrice { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal? WeeklyPrice { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal MonthlyPrice { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal? CleaningFee { get; set; } = 200;

    [Column(TypeName = "decimal(10,2)")]
    public decimal? SecurityDeposit { get; set; } = 1000;

    [Column(TypeName = "decimal(10,2)")]
    public decimal? ExtraGuestFee { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal? LateFeePerHour { get; set; }

    public int? WeeklyDiscount { get; set; }
    public int? MonthlyDiscount { get; set; }
    public int? EarlyBirdDiscount { get; set; }
    public int? LastMinuteDiscount { get; set; }

    public int MinNights { get; set; } = 28;
    public int? MaxNights { get; set; } = 365;
    public int AdvanceNotice { get; set; } = 3;
    public int BookingWindow { get; set; } = 365;

    public bool InstantBook { get; set; } = false;
    public bool RequireProfilePicture { get; set; } = false;
    public bool RequireVerifiedPhone { get; set; } = false;

    [MaxLength(50)]
    public string CancellationPolicy { get; set; } = "moderate";

    public bool UtilitiesIncluded { get; set; } = false;

    [Column(TypeName = "decimal(10,2)")]
    public decimal? UtilitiesCap { get; set; }

    public bool InternetIncluded { get; set; } = true;

    [MaxLength(100)]
    public string? InternetSpeed { get; set; }

    public bool ParkingIncluded { get; set; } = true;

    [MaxLength(100)]
    public string? ParkingType { get; set; }

    public List<string>? Photos { get; set; }

    [MaxLength(255)]
    public string? VirtualTourUrl { get; set; }

    [MaxLength(255)]
    public string? VideoUrl { get; set; }

    [MaxLength(255)]
    public string? FloorPlanUrl { get; set; }

    public Tier Tier { get; set; } = Tier.Standard;
    public bool IsVerified { get; set; } = false;
    public DateTime? VerifiedAt { get; set; }
    public bool IsFeatured { get; set; } = false;
    public DateTime? FeaturedUntil { get; set; }

    public int ViewCount { get; set; } = 0;
    public int BookingCount { get; set; } = 0;
    public int SaveCount { get; set; } = 0;
    public float? AverageRating { get; set; } = 0;
    public int ReviewCount { get; set; } = 0;

    [MaxLength(255)]
    public string? MetaTitle { get; set; }

    public string? MetaDescription { get; set; }
    public List<string> Keywords { get; set; } = new List<string>();

    public bool IsActive { get; set; } = false;
    public DateTime? PublishedAt { get; set; }
    public DateTime? LastBooked { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [MaxLength(255)]
    public string? BuildingName { get; set; }

    [MaxLength(50)]
    public string? BuildingNumber { get; set; }

    public CurrencyCode Currency { get; set; } = CurrencyCode.QAR;

    public string? Directions { get; set; }
    public FurnishingStatus FurnishingStatus { get; set; } = FurnishingStatus.FullyFurnished;

    [MaxLength(255)]
    public string? GoogleMapsUrl { get; set; }

    public int? LastRenovated { get; set; }

    [MaxLength(20)]
    public string? PostalCode { get; set; }

    public object? PricesInOtherCurrencies { get; set; }
    public List<RentalType> RentalType { get; set; } = new List<RentalType>();

    public int? SquareFeet { get; set; }

    [MaxLength(255)]
    public string? StreetName { get; set; }

    [MaxLength(50)]
    public string? StreetNumber { get; set; }

    [MaxLength(50)]
    public string? UnitNumber { get; set; }

    public int? YearBuilt { get; set; }

    [MaxLength(50)]
    public string? ZoneNumber { get; set; }

    // Navigation properties
    [ForeignKey("HostId")]
    public virtual User Host { get; set; }

    public virtual ICollection<Availability> Availability { get; set; } = new List<Availability>();
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public virtual ICollection<CalendarBlock> CalendarBlocks { get; set; } = new List<CalendarBlock>();
    public virtual ICollection<FavoriteListing> Favorites { get; set; } = new List<FavoriteListing>();
    public virtual ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();
    public virtual ICollection<PriceHistory> PriceHistory { get; set; } = new List<PriceHistory>();
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    public virtual ICollection<CoHost> CoHosts { get; set; } = new List<CoHost>();
    public virtual ICollection<CoHostInvitation> CoHostInvitations { get; set; } = new List<CoHostInvitation>();
}