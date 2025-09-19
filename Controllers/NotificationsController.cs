using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Houseiana.Services;
using Houseiana.Models;
using System.Security.Claims;

namespace Houseiana.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NotificationsController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationsController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    /// <summary>
    /// Get user's notifications
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications()
    {
        try
        {
            var userId = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "User ID not found" });

            var notifications = await _notificationService.GetNotificationsAsync(userId);
            return Ok(notifications);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving notifications", error = ex.Message });
        }
    }

    /// <summary>
    /// Create new notification (Admin/System only)
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Notification>> CreateNotification([FromBody] object notificationData)
    {
        try
        {
            var notification = await _notificationService.CreateNotificationAsync(notificationData);
            return CreatedAtAction(nameof(GetNotifications), notification);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error creating notification", error = ex.Message });
        }
    }

    /// <summary>
    /// Mark notification as read
    /// </summary>
    [HttpPut("{id}/read")]
    public async Task<ActionResult> MarkAsRead(string id)
    {
        try
        {
            var success = await _notificationService.MarkAsReadAsync(id);
            if (!success)
                return NotFound(new { message = "Notification not found" });

            return Ok(new { message = "Notification marked as read" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error updating notification", error = ex.Message });
        }
    }

    /// <summary>
    /// Delete notification
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteNotification(string id)
    {
        try
        {
            var success = await _notificationService.DeleteNotificationAsync(id);
            if (!success)
                return NotFound(new { message = "Notification not found" });

            return Ok(new { message = "Notification deleted successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error deleting notification", error = ex.Message });
        }
    }

    /// <summary>
    /// Send email notification (Admin only)
    /// </summary>
    [HttpPost("email")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> SendEmailNotification([FromBody] EmailNotificationRequest request)
    {
        try
        {
            await _notificationService.SendEmailNotificationAsync(request.Email, request.Subject, request.Message);
            return Ok(new { message = "Email notification sent successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error sending email notification", error = ex.Message });
        }
    }

    /// <summary>
    /// Send push notification (Admin only)
    /// </summary>
    [HttpPost("push")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> SendPushNotification([FromBody] PushNotificationRequest request)
    {
        try
        {
            await _notificationService.SendPushNotificationAsync(request.UserId, request.Title, request.Message);
            return Ok(new { message = "Push notification sent successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error sending push notification", error = ex.Message });
        }
    }
}

public class EmailNotificationRequest
{
    public string Email { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class PushNotificationRequest
{
    public string UserId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}