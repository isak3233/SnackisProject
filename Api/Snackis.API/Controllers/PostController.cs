using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Snackis.Application.DTO;
using Snackis.Application.Interfaces;
using Snackis.Application.Services;

namespace Snackis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IAuthService _authService;
        public PostController(IPostService postService, IAuthService authService)
        {
            _postService = postService;
            _authService = authService;
        }
        [HttpGet]
        public async Task<IActionResult> GetPostAsync([FromQuery] int postId)
        {
            try
            {
                var post = await _postService.GetPostAsync(postId);
                return Ok(post);
            } catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            


        }
        [HttpGet("/api/Posts")]
        public async Task<IActionResult> GetPostsAsync([FromQuery] GetPostsDto query)
        {
            var posts = await _postService.GetPostsAsync(query.SubSubjectId, query.Start, query.Count);
            return Ok(posts);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePostAsync(
            [FromHeader(Name = "Api-Key")] string apiKey,
            [FromHeader(Name = "User-Id")] string userId,
            [FromBody] CreatePostDto request
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request data.");
            }
            var user = await _authService.AuthorizeAsync(apiKey, userId);
            if(user == null)
            {
                return Unauthorized();
            }
            await _postService.CreatePostAsync(request, userId);
            return Created();

        }
        [HttpGet("postcomments")]
        public async Task<IActionResult> GetPostCommentsAsync([FromQuery] GetPostCommentDto query)
        {
            var result = await _postService.GetPostCommentsAsync(query.PostId, query.Start, query.Count);
            return Ok(result);
        }
        [HttpPost("postcomment")]
        public async Task<IActionResult> CreatePostCommentAsync(
            [FromHeader(Name = "Api-Key")] string apiKey,
            [FromHeader(Name = "User-Id")] string userId,
            [FromBody] CreatePostCommentDto request
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request data.");
            }
            var user = await _authService.AuthorizeAsync(apiKey, userId);
            if (user == null)
            {
                return Unauthorized();
            }
            var result = await _postService.CreatePostCommentAsync(request, userId);
            if (result == false) return BadRequest();
            return Created();
        }
        [HttpGet("report")]
        public async Task<IActionResult> GetReports(
            [FromHeader(Name = "Api-Key")] string apiKey,
            [FromHeader(Name = "User-Id")] string userId,
            [FromQuery] int count = 10,
            [FromQuery] bool solved = false
            )
        {
            var user = await _authService.AuthorizeAsync(apiKey, userId, "Admin");
            if (user == null)
            {
                return Unauthorized();
            }
            var reports = await _postService.GetReportsAsync(count, solved);
            return Ok(reports);

        }
        [HttpPost("report")]
        public async Task<IActionResult> CreateReportAsync(
            [FromBody] CreateReportDto request
            )
        {

            try
            {
                await _postService.CreateReportAsync(request);
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Created();
        }
        [HttpPut("report")]
        public async Task<IActionResult> SolveReportAsync(
            [FromHeader(Name = "Api-Key")] string apiKey,
            [FromHeader(Name = "User-Id")] string userId,
            [FromBody] UpdateReportDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request data.");
            }
            var user = await _authService.AuthorizeAsync(apiKey, userId, "Admin");
            if (user == null)
            {
                return Unauthorized();
            }
            try
            {
                await _postService.UpdateReportAsync(request);
                return Created();
            } catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePostAsync(
            int postId,
            [FromHeader(Name = "Api-Key")] string apiKey,
            [FromHeader(Name = "User-Id")] string userId,
            [FromBody] DeletePostDto request
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request data.");
            }
            var user = await _authService.AuthorizeAsync(apiKey, userId, "Admin");
            if (user == null)
            {
                return Unauthorized();
            }
            try
            {
                await _postService.DeletePostAsync(request, postId);
                return NoContent();
            } catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            

        }
        [HttpDelete("postcomment/{postCommentId}")]
        public async Task<IActionResult> DeletePostCommentAsync(
            int postCommentId,
            [FromHeader(Name = "Api-Key")] string apiKey,
            [FromHeader(Name = "User-Id")] string userId
            )
        {
            var user = await _authService.AuthorizeAsync(apiKey, userId, "Admin");
            if (user == null)
            {
                return Unauthorized();
            }
            try
            {
                await _postService.DeletePostCommentAsync(postCommentId);
                return NoContent();
            } catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
