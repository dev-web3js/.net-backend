using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Houseiana.Services;
using Houseiana.Models;
using System.Security.Claims;

namespace Houseiana.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    /// <summary>
    /// Get user's bookings with pagination
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Booking>>> GetBookings([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        try
        {
            var userId = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "User ID not found" });

            var bookings = await _bookingService.GetBookingsAsync(userId, page, pageSize);
            return Ok(bookings);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving bookings", error = ex.Message });
        }
    }

    /// <summary>
    /// Get booking by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Booking>> GetBooking(string id)
    {
        try
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
                return NotFound(new { message = "Booking not found" });

            var userId = User.FindFirst("userId")?.Value;
            if (booking.GuestId != userId && booking.HostId != userId)
                return Forbid("Access denied");

            return Ok(booking);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving booking", error = ex.Message });
        }
    }

    /// <summary>
    /// Create new booking
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Booking>> CreateBooking([FromBody] object bookingData)
    {
        try
        {
            var userId = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "User ID not found" });

            var booking = await _bookingService.CreateBookingAsync(bookingData);
            return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, booking);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error creating booking", error = ex.Message });
        }
    }

    /// <summary>
    /// Update booking
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<Booking>> UpdateBooking(string id, [FromBody] object updateData)
    {
        try
        {
            var booking = await _bookingService.UpdateBookingAsync(id, updateData);
            return Ok(booking);
        }
        catch (ArgumentException)
        {
            return NotFound(new { message = "Booking not found" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error updating booking", error = ex.Message });
        }
    }

    /// <summary>
    /// Cancel booking
    /// </summary>
    [HttpPost("{id}/cancel")]
    public async Task<ActionResult> CancelBooking(string id, [FromBody] object cancelData)
    {
        try
        {
            var reason = "Cancelled by user"; // Extract from cancelData
            var success = await _bookingService.CancelBookingAsync(id, reason);
            if (!success)
                return NotFound(new { message = "Booking not found" });

            return Ok(new { message = "Booking cancelled successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error cancelling booking", error = ex.Message });
        }
    }

    /// <summary>
    /// Get host's bookings
    /// </summary>
    [HttpGet("host")]
    public async Task<ActionResult<IEnumerable<Booking>>> GetHostBookings()
    {
        try
        {
            var hostId = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(hostId))
                return Unauthorized(new { message = "User ID not found" });

            var isHost = User.FindFirst("isHost")?.Value == "True";
            if (!isHost)
                return Forbid("Only hosts can access this endpoint");

            var bookings = await _bookingService.GetHostBookingsAsync(hostId);
            return Ok(bookings);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving host bookings", error = ex.Message });
        }
    }

    /// <summary>
    /// Get guest's bookings
    /// </summary>
    [HttpGet("guest")]
    public async Task<ActionResult<IEnumerable<Booking>>> GetGuestBookings()
    {
        try
        {
            var guestId = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(guestId))
                return Unauthorized(new { message = "User ID not found" });

            var bookings = await _bookingService.GetGuestBookingsAsync(guestId);
            return Ok(bookings);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving guest bookings", error = ex.Message });
        }
    }
}