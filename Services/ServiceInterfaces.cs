using System.Collections.Generic;
using System.Threading.Tasks;
using Houseiana.Models;
using Houseiana.DTOs;

namespace Houseiana.Services;

public interface IListingService
{
    Task<IEnumerable<Listing>> GetListingsAsync(int page = 1, int pageSize = 20);
    Task<Listing?> GetListingByIdAsync(string id);
    Task<Listing> CreateListingAsync(string hostId, object listingData);
    Task<Listing> UpdateListingAsync(string id, object updateData);
    Task<bool> DeleteListingAsync(string id);
    Task<IEnumerable<Listing>> GetHostListingsAsync(string hostId);
    Task<IEnumerable<Listing>> SearchListingsAsync(object searchCriteria);
}

public interface IBookingService
{
    Task<IEnumerable<Booking>> GetBookingsAsync(string userId, int page = 1, int pageSize = 20);
    Task<Booking?> GetBookingByIdAsync(string id);
    Task<Booking> CreateBookingAsync(object bookingData);
    Task<Booking> UpdateBookingAsync(string id, object updateData);
    Task<bool> CancelBookingAsync(string id, string reason);
    Task<IEnumerable<Booking>> GetHostBookingsAsync(string hostId);
    Task<IEnumerable<Booking>> GetGuestBookingsAsync(string guestId);
}

public interface IUserService
{
    Task<User?> GetUserByIdAsync(string id);
    Task<User> UpdateUserAsync(string id, object updateData);
    Task<bool> DeleteUserAsync(string id);
    Task<IEnumerable<User>> GetUsersAsync(int page = 1, int pageSize = 20);
    Task<bool> VerifyEmailAsync(string userId, string token);
    Task<bool> VerifyPhoneAsync(string userId, string code);
}

public interface INotificationService
{
    Task<IEnumerable<Notification>> GetNotificationsAsync(string userId);
    Task<Notification> CreateNotificationAsync(object notificationData);
    Task<bool> MarkAsReadAsync(string notificationId);
    Task<bool> DeleteNotificationAsync(string notificationId);
    Task SendEmailNotificationAsync(string email, string subject, string message);
    Task SendPushNotificationAsync(string userId, string title, string message);
}