using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Houseiana.Services;
using Houseiana.DTOs;

namespace Houseiana.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public AuthController(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    /// <summary>
    /// Register a new user
    /// </summary>
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            var result = await _authService.RegisterAsync(registerDto);
            return CreatedAtAction(nameof(Register), result);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Login user
    /// </summary>
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var result = await _authService.LoginAsync(loginDto);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Logout user
    /// </summary>
    [HttpPost("logout")]
    [Authorize]
    public async Task<ActionResult<MessageResponseDto>> Logout()
    {
        var userId = User.FindFirst("userId")?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest(new { message = "User ID not found in token" });
        }

        var result = await _authService.LogoutAsync(userId);
        return Ok(result);
    }

    /// <summary>
    /// Get user profile
    /// </summary>
    [HttpGet("profile")]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetProfile()
    {
        try
        {
            var userId = User.FindFirst("userId")?.Value;
            var user = await _authService.GetProfileAsync(userId);
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Update user profile
    /// </summary>
    [HttpPut("profile")]
    [Authorize]
    public async Task<ActionResult<AuthResponseDto>> UpdateProfile([FromBody] UpdateProfileDto updateProfileDto)
    {
        try
        {
            var userId = User.FindFirst("userId")?.Value;
            var result = await _authService.UpdateProfileAsync(userId, updateProfileDto);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Become a host
    /// </summary>
    [HttpPost("become-host")]
    [Authorize]
    public async Task<ActionResult<AuthResponseDto>> BecomeHost()
    {
        try
        {
            var userId = User.FindFirst("userId")?.Value;
            var result = await _authService.BecomeHostAsync(userId);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Refresh JWT token
    /// </summary>
    [HttpPost("refresh")]
    [Authorize]
    public async Task<ActionResult<AuthResponseDto>> RefreshToken()
    {
        try
        {
            var userId = User.FindFirst("userId")?.Value;
            var result = await _authService.RefreshTokenAsync(userId);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Validate current token
    /// </summary>
    [HttpGet("validate")]
    [Authorize]
    public async Task<ActionResult<TokenValidationResponseDto>> ValidateToken()
    {
        try
        {
            var userId = User.FindFirst("userId")?.Value;
            var user = await _authService.ValidateUserAsync(userId);
            var userDto = _mapper.Map<UserDto>(user);

            return Ok(new TokenValidationResponseDto
            {
                Valid = true,
                User = userDto,
                Message = "Token is valid"
            });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
}