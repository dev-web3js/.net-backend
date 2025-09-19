using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Houseiana.Services;
using Houseiana.Models;
using System.Security.Claims;

namespace Houseiana.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Get all users (Admin only)
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        try
        {
            var users = await _userService.GetUsersAsync(page, pageSize);
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving users", error = ex.Message });
        }
    }

    /// <summary>
    /// Get user by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(string id)
    {
        try
        {
            var currentUserId = User.FindFirst("userId")?.Value;
            var isAdmin = User.FindFirst("isAdmin")?.Value == "True";

            // Users can only access their own profile unless they're admin
            if (currentUserId != id && !isAdmin)
                return Forbid("Access denied");

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving user", error = ex.Message });
        }
    }

    /// <summary>
    /// Update user profile
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<User>> UpdateUser(string id, [FromBody] object updateData)
    {
        try
        {
            var currentUserId = User.FindFirst("userId")?.Value;
            var isAdmin = User.FindFirst("isAdmin")?.Value == "True";

            // Users can only update their own profile unless they're admin
            if (currentUserId != id && !isAdmin)
                return Forbid("Access denied");

            var user = await _userService.UpdateUserAsync(id, updateData);
            return Ok(user);
        }
        catch (ArgumentException)
        {
            return NotFound(new { message = "User not found" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error updating user", error = ex.Message });
        }
    }

    /// <summary>
    /// Delete user (Admin only or self)
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(string id)
    {
        try
        {
            var currentUserId = User.FindFirst("userId")?.Value;
            var isAdmin = User.FindFirst("isAdmin")?.Value == "True";

            // Users can only delete their own account unless they're admin
            if (currentUserId != id && !isAdmin)
                return Forbid("Access denied");

            var success = await _userService.DeleteUserAsync(id);
            if (!success)
                return NotFound(new { message = "User not found" });

            return Ok(new { message = "User deleted successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error deleting user", error = ex.Message });
        }
    }

    /// <summary>
    /// Verify user email
    /// </summary>
    [HttpPost("{id}/verify-email")]
    public async Task<ActionResult> VerifyEmail(string id, [FromBody] VerifyEmailRequest request)
    {
        try
        {
            var currentUserId = User.FindFirst("userId")?.Value;
            if (currentUserId != id)
                return Forbid("Access denied");

            var success = await _userService.VerifyEmailAsync(id, request.Token);
            if (!success)
                return BadRequest(new { message = "Invalid verification token" });

            return Ok(new { message = "Email verified successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error verifying email", error = ex.Message });
        }
    }

    /// <summary>
    /// Verify user phone
    /// </summary>
    [HttpPost("{id}/verify-phone")]
    public async Task<ActionResult> VerifyPhone(string id, [FromBody] VerifyPhoneRequest request)
    {
        try
        {
            var currentUserId = User.FindFirst("userId")?.Value;
            if (currentUserId != id)
                return Forbid("Access denied");

            var success = await _userService.VerifyPhoneAsync(id, request.Code);
            if (!success)
                return BadRequest(new { message = "Invalid verification code" });

            return Ok(new { message = "Phone verified successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error verifying phone", error = ex.Message });
        }
    }
}

public class VerifyEmailRequest
{
    public string Token { get; set; } = string.Empty;
}

public class VerifyPhoneRequest
{
    public string Code { get; set; } = string.Empty;
}