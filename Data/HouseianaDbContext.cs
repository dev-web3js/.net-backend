using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Houseiana.Models;

namespace Houseiana.Data;

public class HouseianaDbContext : IdentityDbContext<User>
{
    public HouseianaDbContext(DbContextOptions<HouseianaDbContext> options) : base(options)
    {
    }

    public DbSet<Listing> Listings { get; set; }
    public DbSet<ListingDraft> ListingDrafts { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Conversation> Conversations { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<HostPayout> HostPayouts { get; set; }
    public DbSet<FavoriteListing> FavoriteListings { get; set; }
    public DbSet<SearchHistory> SearchHistories { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<PriceHistory> PriceHistories { get; set; }
    public DbSet<Availability> Availabilities { get; set; }
    public DbSet<CalendarBlock> CalendarBlocks { get; set; }
    public DbSet<AmenityCategory> AmenityCategories { get; set; }
    public DbSet<Amenity> Amenities { get; set; }
    public DbSet<PropertyRule> PropertyRules { get; set; }
    public DbSet<CurrencyRate> CurrencyRates { get; set; }
    public DbSet<PropertyView> PropertyViews { get; set; }
    public DbSet<PropertyDocument> PropertyDocuments { get; set; }
    public DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<TaxInformation> TaxInformations { get; set; }
    public DbSet<PhoneVerification> PhoneVerifications { get; set; }
    public DbSet<EmailVerification> EmailVerifications { get; set; }
    public DbSet<CoHost> CoHosts { get; set; }
    public DbSet<CoHostInvitation> CoHostInvitations { get; set; }
    public DbSet<CoHostActivity> CoHostActivities { get; set; }
    public DbSet<KycVerification> KycVerifications { get; set; }
    public DbSet<KycDocument> KycDocuments { get; set; }
    public DbSet<KycAuditLog> KycAuditLogs { get; set; }
    public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure JSON columns
        modelBuilder.Entity<User>()
            .Property(e => e.PropertyDocs)
            .HasConversion(
                v => v == null ? null : JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => v == null ? null : JsonSerializer.Deserialize<object>(v, (JsonSerializerOptions?)null));

        modelBuilder.Entity<User>()
            .Property(e => e.NotificationSettings)
            .HasConversion(
                v => v == null ? null : JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => v == null ? null : JsonSerializer.Deserialize<object>(v, (JsonSerializerOptions?)null));

        modelBuilder.Entity<User>()
            .Property(e => e.PrivacySettings)
            .HasConversion(
                v => v == null ? null : JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => v == null ? null : JsonSerializer.Deserialize<object>(v, (JsonSerializerOptions?)null));

        modelBuilder.Entity<Listing>()
            .Property(e => e.Coordinates)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<object>(v, (JsonSerializerOptions?)null));

        modelBuilder.Entity<Listing>()
            .Property(e => e.Photos)
            .HasConversion(
                v => v == null ? null : JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => v == null ? null : JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null));

        // Configure decimal precision
        modelBuilder.Entity<User>()
            .Property(p => p.TotalEarnings)
            .HasPrecision(12, 2);

        modelBuilder.Entity<Listing>()
            .Property(p => p.NightlyPrice)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Listing>()
            .Property(p => p.WeeklyPrice)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Listing>()
            .Property(p => p.MonthlyPrice)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Booking>()
            .Property(p => p.TotalPrice)
            .HasPrecision(10, 2);

        // Configure indexes
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.UserName);

        modelBuilder.Entity<Listing>()
            .HasIndex(l => l.City);

        modelBuilder.Entity<Listing>()
            .HasIndex(l => l.Status);

        modelBuilder.Entity<Booking>()
            .HasIndex(b => b.BookingCode);

        // Configure relationships
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Guest)
            .WithMany(u => u.GuestBookings)
            .HasForeignKey(b => b.GuestId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Host)
            .WithMany(u => u.HostBookings)
            .HasForeignKey(b => b.HostId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.Reviewer)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.ReviewerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.Reviewee)
            .WithMany(u => u.ReceivedReviews)
            .HasForeignKey(r => r.RevieweeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}