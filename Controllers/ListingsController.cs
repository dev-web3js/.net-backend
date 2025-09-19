using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Houseiana.Services;
using Houseiana.Models;
using System.Security.Claims;

namespace Houseiana.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ListingsController : ControllerBase
{
    private readonly IListingService _listingService;

    public ListingsController(IListingService listingService)
    {
        _listingService = listingService;
    }

    /// <summary>
    /// Get all listings with pagination
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Listing>>> GetListings([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        try
        {
            var listings = await _listingService.GetListingsAsync(page, pageSize);
            return Ok(listings);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving listings", error = ex.Message });
        }
    }

    /// <summary>
    /// Get listing by ID
    /// </summary>
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<Listing>> GetListing(string id)
    {
        try
        {
            var listing = await _listingService.GetListingByIdAsync(id);
            if (listing == null)
                return NotFound(new { message = "Listing not found" });

            return Ok(listing);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving listing", error = ex.Message });
        }
    }

    /// <summary>
    /// Create new listing (Host only)
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Listing>> CreateListing([FromBody] object listingData)
    {
        try
        {
            var userId = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "User ID not found" });

            var isHost = User.FindFirst("isHost")?.Value == "True";
            if (!isHost)
                return Forbid("Only hosts can create listings");

            var listing = await _listingService.CreateListingAsync(userId, listingData);
            return CreatedAtAction(nameof(GetListing), new { id = listing.Id }, listing);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error creating listing", error = ex.Message });
        }
    }

    /// <summary>
    /// Update listing (Host only)
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<Listing>> UpdateListing(string id, [FromBody] object updateData)
    {
        try
        {
            var userId = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "User ID not found" });

            var listing = await _listingService.UpdateListingAsync(id, updateData);
            return Ok(listing);
        }
        catch (ArgumentException)
        {
            return NotFound(new { message = "Listing not found" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error updating listing", error = ex.Message });
        }
    }

    /// <summary>
    /// Delete listing (Host only)
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteListing(string id)
    {
        try
        {
            var success = await _listingService.DeleteListingAsync(id);
            if (!success)
                return NotFound(new { message = "Listing not found" });

            return Ok(new { message = "Listing deleted successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error deleting listing", error = ex.Message });
        }
    }

    /// <summary>
    /// Get host's listings
    /// </summary>
    [HttpGet("host/{hostId}")]
    public async Task<ActionResult<IEnumerable<Listing>>> GetHostListings(string hostId)
    {
        try
        {
            var listings = await _listingService.GetHostListingsAsync(hostId);
            return Ok(listings);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving host listings", error = ex.Message });
        }
    }

    /// <summary>
    /// Search listings
    /// </summary>
    [HttpPost("search")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Listing>>> SearchListings([FromBody] object searchCriteria)
    {
        try
        {
            var listings = await _listingService.SearchListingsAsync(searchCriteria);
            return Ok(listings);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error searching listings", error = ex.Message });
        }
    }
}