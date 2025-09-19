using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Houseiana.Data;
using Houseiana.Models;

namespace Houseiana.Services;

public class UserService : IUserService
{
    private readonly HouseianaDbContext _context;

    public UserService(HouseianaDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByIdAsync(string id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User> UpdateUserAsync(string id, object updateData)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            throw new ArgumentException("User not found");

        user.UpdatedAt = DateTime.UtcNow;
        
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteUserAsync(string id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return false;

        user.DeletedAt = DateTime.UtcNow;
        
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<User>> GetUsersAsync(int page = 1, int pageSize = 20)
    {
        return await _context.Users
            .Where(u => u.DeletedAt == null)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<bool> VerifyEmailAsync(string userId, string token)
    {
        var verification = await _context.EmailVerifications
            .FirstOrDefaultAsync(v => v.UserId == userId && v.Token == token && !v.IsVerified);
        
        if (verification == null || verification.ExpiresAt < DateTime.UtcNow)
            return false;

        verification.IsVerified = true;
        verification.VerifiedAt = DateTime.UtcNow;
        
        var user = await _context.Users.FindAsync(userId);
        if (user != null)
        {
            user.EmailVerified = true;
            user.EmailVerifiedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> VerifyPhoneAsync(string userId, string code)
    {
        var verification = await _context.PhoneVerifications
            .FirstOrDefaultAsync(v => v.UserId == userId && v.Code == code && !v.IsVerified);
        
        if (verification == null || verification.ExpiresAt < DateTime.UtcNow)
            return false;

        verification.IsVerified = true;
        verification.VerifiedAt = DateTime.UtcNow;
        
        var user = await _context.Users.FindAsync(userId);
        if (user != null)
        {
            user.PhoneVerified = true;
            user.PhoneVerifiedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
        return true;
    }
}