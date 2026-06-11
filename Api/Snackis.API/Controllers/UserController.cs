using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Snackis.Application.Interfaces;
using Snackis.Application.Services;

namespace Snackis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        public UserController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }
        [HttpGet("useremail")]
        public async Task<IActionResult> GetUserIdByEmail([FromBody] string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request data.");
            }
            try
            {
                var user = await _userService.GetUserByEmail(email);
                return Ok(user);
            } catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}/avatar")]
        public async Task<IActionResult> GetAvatar(string id)
        {
            try
            {
                var avatarUrlDto = await _userService.GetAvatarUrlAsync(id);
                var avatarUrl = $"{Request.Scheme}://{Request.Host}{avatarUrlDto.AvatarUrl}";
                return Ok(new {
                    AvatarUrl = avatarUrl
                });
            } catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            
            
        }

        [HttpPost("upload-avatar")]
        public async Task<IActionResult> UploadAvatar(
            [FromHeader(Name = "Api-Key")] string apiKey,
            [FromHeader(Name = "User-Id")] string userId,
            IFormFile file
            )
        {
            var user = await _authService.AuthorizeAsync(apiKey, userId);
            if (user == null) return Unauthorized();

            string? badRequestText = _userService.CheckFile(file);
            if (badRequestText != null)
            {

                return BadRequest(badRequestText);
            }

            try
            {
                await _userService.UpdateAvatarAsync(userId, file);
                return Created();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

}