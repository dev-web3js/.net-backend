using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Houseiana.Data;
using Houseiana.Models;
using Houseiana.DTOs;

namespace Houseiana.Services;

public class BookingService : IBookingService
{
    private readonly HouseianaDbContext _context;

    public BookingService(HouseianaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Booking>> GetBookingsAsync(string userId, int page = 1, int pageSize = 20)
    {
        return await _context.Bookings
            .Include(b => b.Guest)
            .Include(b => b.Host)
            .Include(b => b.Listing)
            .Where(b => b.GuestId == userId || b.HostId == userId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync();
    }

    public async Task<Booking?> GetBookingByIdAsync(string id)
    {
        return await _context.Bookings
            .Include(b => b.Guest)
            .Include(b => b.Host)
            .Include(b => b.Listing)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Booking> CreateBookingAsync(object bookingData)
    {
        var booking = new Booking
        {
            Id = Guid.NewGuid().ToString(),
            BookingCode = Guid.NewGuid().ToString(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
            // Map other properties from bookingData
        };

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();
        return booking;
    }

    public async Task<Booking> UpdateBookingAsync(string id, object updateData)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null)
            throw new ArgumentException("Booking not found");

        booking.UpdatedAt = DateTime.UtcNow;
        
        _context.Bookings.Update(booking);
        await _context.SaveChangesAsync();
        return booking;
    }

    public async Task<bool> CancelBookingAsync(string id, string reason)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null)
            return false;

        booking.Status = BookingStatus.Cancelled;
        booking.CancelledAt = DateTime.UtcNow;
        booking.CancelReason = reason;
        booking.UpdatedAt = DateTime.UtcNow;
        
        _context.Bookings.Update(booking);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Booking>> GetHostBookingsAsync(string hostId)
    {
        return await _context.Bookings
            .Include(b => b.Guest)
            .Include(b => b.Listing)
            .Where(b => b.HostId == hostId)
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Booking>> GetGuestBookingsAsync(string guestId)
    {
        return await _context.Bookings
            .Include(b => b.Host)
            .Include(b => b.Listing)
            .Where(b => b.GuestId == guestId)
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync();
    }
}