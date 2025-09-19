using System.Threading.Tasks;
using Houseiana.Models;
using Houseiana.DTOs;

namespace Houseiana.Services;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
    Task<MessageResponseDto> LogoutAsync(string userId);
    Task<User> GetProfileAsync(string userId);
    Task<AuthResponseDto> UpdateProfileAsync(string userId, UpdateProfileDto updateProfileDto);
    Task<AuthResponseDto> BecomeHostAsync(string userId);
    Task<AuthResponseDto> RefreshTokenAsync(string userId);
    Task<User> ValidateUserAsync(string userId);
}