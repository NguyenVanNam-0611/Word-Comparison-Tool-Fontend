using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Diff_tool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UploadController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public UploadController(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        [HttpPost("compare")]
        public async Task<IActionResult> Compare(
            IFormFile? file1,
            IFormFile? file2)
        {
            if (file1 == null || file2 == null)
                return BadRequest(new { message = "Vui lòng upload đủ 2 file" });

            var username = User.FindFirst(ClaimTypes.Name)?.Value ?? "unknown";

            var fastApiUrl = _config["FastApi:BaseUrl"] ?? "http://localhost:8000";

            using var form = new MultipartFormDataContent();

            // username
            form.Add(new StringContent(username), "username");

            // file1 → originalFile
            var stream1 = file1.OpenReadStream();
            form.Add(new StreamContent(stream1), "originalFile", file1.FileName);

            // file2 → modifiedFile
            var stream2 = file2.OpenReadStream();
            form.Add(new StreamContent(stream2), "modifiedFile", file2.FileName);

            var client = _httpClientFactory.CreateClient();

            var response = await client.PostAsync($"{fastApiUrl}/upload", form);

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, new { message = content });

            return Content(content, "application/json");
        }
    }
}