using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Houseiana.Data;
using Houseiana.Models;
using Houseiana.DTOs;

namespace Houseiana.Services;

public class ListingService : IListingService
{
    private readonly HouseianaDbContext _context;

    public ListingService(HouseianaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Listing>> GetListingsAsync(int page = 1, int pageSize = 20)
    {
        return await _context.Listings
            .Include(l => l.Host)
            .Where(l => l.Status == ListingStatus.Active)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Listing?> GetListingByIdAsync(string id)
    {
        return await _context.Listings
            .Include(l => l.Host)
            .Include(l => l.Reviews)
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<Listing> CreateListingAsync(string hostId, object listingData)
    {
        // Implementation would depend on the specific DTO structure
        // This is a simplified version
        var listing = new Listing
        {
            Id = Guid.NewGuid().ToString(),
            HostId = hostId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
            // Map other properties from listingData
        };

        _context.Listings.Add(listing);
        await _context.SaveChangesAsync();
        return listing;
    }

    public async Task<Listing> UpdateListingAsync(string id, object updateData)
    {
        var listing = await _context.Listings.FindAsync(id);
        if (listing == null)
            throw new ArgumentException("Listing not found");

        // Update properties based on updateData
        listing.UpdatedAt = DateTime.UtcNow;
        
        _context.Listings.Update(listing);
        await _context.SaveChangesAsync();
        return listing;
    }

    public async Task<bool> DeleteListingAsync(string id)
    {
        var listing = await _context.Listings.FindAsync(id);
        if (listing == null)
            return false;

        _context.Listings.Remove(listing);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Listing>> GetHostListingsAsync(string hostId)
    {
        return await _context.Listings
            .Where(l => l.HostId == hostId)
            .OrderByDescending(l => l.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Listing>> SearchListingsAsync(object searchCriteria)
    {
        // Implementation would depend on search criteria structure
        // This is a basic implementation
        return await _context.Listings
            .Where(l => l.Status == ListingStatus.Active)
            .ToListAsync();
    }
}