using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Houseiana.Data;
using Houseiana.Models;

namespace Houseiana.Services;

public class NotificationService : INotificationService
{
    private readonly HouseianaDbContext _context;

    public NotificationService(HouseianaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Notification>> GetNotificationsAsync(string userId)
    {
        return await _context.Notifications
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();
    }

    public async Task<Notification> CreateNotificationAsync(object notificationData)
    {
        var notification = new Notification
        {
            Id = Guid.NewGuid().ToString(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
            // Map properties from notificationData
        };

        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();
        return notification;
    }

    public async Task<bool> MarkAsReadAsync(string notificationId)
    {
        var notification = await _context.Notifications.FindAsync(notificationId);
        if (notification == null)
            return false;

        notification.IsRead = true;
        notification.ReadAt = DateTime.UtcNow;
        
        _context.Notifications.Update(notification);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteNotificationAsync(string notificationId)
    {
        var notification = await _context.Notifications.FindAsync(notificationId);
        if (notification == null)
            return false;

        _context.Notifications.Remove(notification);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task SendEmailNotificationAsync(string email, string subject, string message)
    {
        // Implementation would depend on email service provider
        // This is a placeholder
        await Task.CompletedTask;
    }

    public async Task SendPushNotificationAsync(string userId, string title, string message)
    {
        // Implementation would depend on push notification service
        // This is a placeholder
        await Task.CompletedTask;
    }
}