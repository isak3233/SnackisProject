using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Snackis.Application.DTO;
using Snackis.Application.Interfaces;

namespace Snackis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubSubjectController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ISubjectService _subjectService; 
        public SubSubjectController(IAuthService authService, ISubjectService subjectService)
        {
            _authService = authService;
            _subjectService = subjectService;
        }
        [HttpGet("{subSubjectId}")]
        public async Task<IActionResult> GetSubSubjectAsync(int subSubjectId)
        {
            try
            {
                var subSubject = await _subjectService.GetSubSubjectAsync(subSubjectId);
                return Ok(subSubject);
            } catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            
            
        }
        [HttpPost("{subjectId}")]
        public async Task<IActionResult> CreateSubAsync(
            int subjectId,
            [FromHeader(Name = "Api-Key")] string apiKey,
            [FromHeader(Name = "User-Id")] string userId,
            [FromBody] CreateSubSubjectDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _authService.AuthorizeAsync(apiKey, userId, "Admin");
            if (user == null)
            {
                return Unauthorized();
            }
            var result = await _subjectService.CreateSubSubjectAsync(subjectId, request);
            if (result == false) return BadRequest();

            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubAsync(
            int id,
            [FromHeader(Name = "Api-Key")] string apiKey,
            [FromHeader(Name = "User-Id")] string userId,
            [FromBody] UpdateSubSubjectDto request)
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
            var result = await _subjectService.UpdateSubSubjectAsync(id, request);
            if (result == false) return BadRequest();

            return NoContent();
        }
    }
}
