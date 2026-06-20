// DTOs/AuthDtos.cs
namespace Diff_tool.DTOs
{
    public record LoginDto(string Username, string Password);
    public record AuthResponseDto(string Token, string Username, string Role);
    public record CreateUserDto(string Username, string Password, int RoleId);
}