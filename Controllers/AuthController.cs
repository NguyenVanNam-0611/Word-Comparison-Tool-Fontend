using Diff_tool.Data;
using Diff_tool.DTOs;
using Diff_tool.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diff_tool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly JwtService _jwt;

        public AuthController(AppDbContext db, JwtService jwt)
        {
            _db = db;
            _jwt = jwt;
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _db.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == dto.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized(new { message = "Username hoặc mật khẩu không đúng" });

            // ✅ Kiểm tra tài khoản có bị khóa không
            if (!user.IsActive)
                return Unauthorized(new { message = "Tài khoản đã bị khóa, liên hệ Admin" });

            return Ok(new AuthResponseDto(
            _jwt.GenerateToken(user.Username, user.Role.Name),
            user.Username,
            user.Role.Name
));
        }
    }
}