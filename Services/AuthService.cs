using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BCrypt.Net;
using AutoMapper;
using Houseiana.Data;
using Houseiana.Models;
using Houseiana.DTOs;

namespace Houseiana.Services;

public class AuthService : IAuthService
{
    private readonly HouseianaDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public AuthService(HouseianaDbContext context, IConfiguration configuration, IMapper mapper)
    {
        _context = context;
        _configuration = configuration;
        _mapper = mapper;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        // Check if user already exists
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == registerDto.Email);

        if (existingUser != null)
        {
            throw new InvalidOperationException("User with this email already exists");
        }

        // Hash password
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password, 12);

        // Create user
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            UserName = registerDto.Email,
            Email = registerDto.Email,
            EmailConfirmed = false,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Phone = registerDto.Phone,
            Role = Enum.Parse<UserRole>(registerDto.Role ?? "Guest", true),
            IsHost = registerDto.Role?.ToLower() == "host" || registerDto.Role?.ToLower() == "both",
            PasswordHash = hashedPassword,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Generate JWT token
        var token = GenerateJwtToken(user);

        // Create session
        var session = new Session
        {
            Id = Guid.NewGuid().ToString(),
            UserId = user.Id,
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddDays(30),
            CreatedAt = DateTime.UtcNow
        };

        _context.Sessions.Add(session);
        await _context.SaveChangesAsync();

        var userDto = _mapper.Map<UserDto>(user);

        return new AuthResponseDto
        {
            User = userDto,
            Token = token,
            Message = "User registered successfully"
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
    {
        // Find user
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        // Verify password
        var isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
        if (!isPasswordValid)
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        // Generate JWT token
        var token = GenerateJwtToken(user);

        // Update or create session
        var existingSession = await _context.Sessions
            .FirstOrDefaultAsync(s => s.UserId == user.Id);

        if (existingSession != null)
        {
            existingSession.Token = token;
            existingSession.ExpiresAt = DateTime.UtcNow.AddDays(30);
            existingSession.LastActivity = DateTime.UtcNow;
            _context.Sessions.Update(existingSession);
        }
        else
        {
            var newSession = new Session
            {
                Id = Guid.NewGuid().ToString(),
                UserId = user.Id,
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddDays(30),
                LastActivity = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };
            _context.Sessions.Add(newSession);
        }

        await _context.SaveChangesAsync();

        var userDto = _mapper.Map<UserDto>(user);

        return new AuthResponseDto
        {
            User = userDto,
            Token = token,
            Message = "Login successful"
        };
    }

    public async Task<MessageResponseDto> LogoutAsync(string userId)
    {
        // Delete sessions
        var sessions = await _context.Sessions
            .Where(s => s.UserId == userId)
            .ToListAsync();

        _context.Sessions.RemoveRange(sessions);
        await _context.SaveChangesAsync();

        return new MessageResponseDto { Message = "Logout successful" };
    }

    public async Task<User> GetProfileAsync(string userId)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            throw new UnauthorizedAccessException("User not found");
        }

        return user;
    }

    public async Task<AuthResponseDto> UpdateProfileAsync(string userId, UpdateProfileDto updateProfileDto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            throw new UnauthorizedAccessException("User not found");
        }

        // Update user properties
        if (!string.IsNullOrEmpty(updateProfileDto.FirstName))
            user.FirstName = updateProfileDto.FirstName;
        if (!string.IsNullOrEmpty(updateProfileDto.LastName))
            user.LastName = updateProfileDto.LastName;
        if (!string.IsNullOrEmpty(updateProfileDto.Phone))
            user.Phone = updateProfileDto.Phone;
        if (!string.IsNullOrEmpty(updateProfileDto.Bio))
            user.Bio = updateProfileDto.Bio;
        if (!string.IsNullOrEmpty(updateProfileDto.ProfileImage))
            user.ProfileImage = updateProfileDto.ProfileImage;
        if (!string.IsNullOrEmpty(updateProfileDto.Language))
            user.Language = updateProfileDto.Language;
        if (!string.IsNullOrEmpty(updateProfileDto.Currency))
            user.Currency = updateProfileDto.Currency;

        if (updateProfileDto.EmailNotifications.HasValue)
            user.EmailNotifications = updateProfileDto.EmailNotifications.Value;
        if (updateProfileDto.SmsNotifications.HasValue)
            user.SmsNotifications = updateProfileDto.SmsNotifications.Value;
        if (updateProfileDto.PushNotifications.HasValue)
            user.PushNotifications = updateProfileDto.PushNotifications.Value;

        user.UpdatedAt = DateTime.UtcNow;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        var userDto = _mapper.Map<UserDto>(user);

        return new AuthResponseDto
        {
            User = userDto,
            Message = "Profile updated successfully"
        };
    }

    public async Task<AuthResponseDto> BecomeHostAsync(string userId)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            throw new UnauthorizedAccessException("User not found");
        }

        user.IsHost = true;
        user.Role = UserRole.Both;
        user.HostSince = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        var userDto = _mapper.Map<UserDto>(user);

        return new AuthResponseDto
        {
            User = userDto,
            Message = "Successfully became a host"
        };
    }

    public async Task<AuthResponseDto> RefreshTokenAsync(string userId)
    {
        var user = await ValidateUserAsync(userId);
        var token = GenerateJwtToken(user);

        // Update session
        var existingSession = await _context.Sessions
            .FirstOrDefaultAsync(s => s.UserId == userId);

        if (existingSession != null)
        {
            existingSession.Token = token;
            existingSession.ExpiresAt = DateTime.UtcNow.AddDays(30);
            existingSession.LastActivity = DateTime.UtcNow;
            _context.Sessions.Update(existingSession);
        }
        else
        {
            var newSession = new Session
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddDays(30),
                LastActivity = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };
            _context.Sessions.Add(newSession);
        }

        await _context.SaveChangesAsync();

        var userDto = _mapper.Map<UserDto>(user);

        return new AuthResponseDto
        {
            User = userDto,
            Token = token,
            Message = "Token refreshed successfully"
        };
    }

    public async Task<User> ValidateUserAsync(string userId)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            throw new UnauthorizedAccessException("User not found");
        }

        return user;
    }

    private string GenerateJwtToken(User user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"]);

        var claims = new[]
        {
            new Claim("userId", user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("role", user.Role.ToString()),
            new Claim("isHost", user.IsHost.ToString()),
            new Claim("isAdmin", user.IsAdmin.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(int.Parse(jwtSettings["ExpiryInHours"])),
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}