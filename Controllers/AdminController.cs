using Diff_tool.Data;
using Diff_tool.DTOs;
using Diff_tool.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diff_tool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] // chỉ Admin mới vào được
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _db;

        public AdminController(AppDbContext db)
        {
            _db = db;
        }

        // GET api/admin/users — danh sách user
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _db.Users
                .Include(u => u.Role)
                .Select(u => new
                {
                    u.Id,
                    u.Username,
                    u.IsActive,
                    u.CreatedAt,
                    Role = u.Role.Name
                })
                .ToListAsync();

            return Ok(users);
        }

        // POST api/admin/users — tạo user mới
        [HttpPost("users")]
        public async Task<IActionResult> CreateUser(CreateUserDto dto)
        {
            if (await _db.Users.AnyAsync(u => u.Username == dto.Username))
                return BadRequest(new { message = "Username đã tồn tại" });

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                RoleId = dto.RoleId, // 1 = Admin, 2 = User
                IsActive = true
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Tạo user thành công", user.Id, user.Username });
        }
        // PATCH api/admin/users/{id}/password - Đổi mật khẩu người dùng 
        [HttpPatch("users/{id}/password")]
        public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePasswordDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.NewPassword) || dto.NewPassword.Length < 6)
                return BadRequest(new { message = "Mật khẩu phải có ít nhất 6 ký tự" });

            var user = await _db.Users.FindAsync(id);
            if (user == null)
                return NotFound(new { message = "Không tìm thấy user" });

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Đổi mật khẩu thành công" });
        }
        // PATCH api/admin/users/{id}/toggle — bật/tắt tài khoản
        [HttpPatch("users/{id}/toggle")]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null)
                return NotFound(new { message = "Không tìm thấy user" });

            user.IsActive = !user.IsActive;
            await _db.SaveChangesAsync();

            return Ok(new
            {
                message = user.IsActive ? "Đã mở khóa tài khoản" : "Đã khóa tài khoản",
                user.Id,
                user.IsActive
            });
        }

        // DELETE api/admin/users/{id} — xóa user
        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null)
                return NotFound(new { message = "Không tìm thấy user" });

            _db.Users.Remove(user);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Đã xóa user" });
        }
    }
}