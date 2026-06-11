using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Snackis.Application.DTO;
using Snackis.Application.Interfaces;

namespace Snackis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ISubjectService _subjectService;
        public SubjectController(IAuthService authService, ISubjectService subjectService)
        {
            _authService = authService;
            _subjectService = subjectService;
        }
        [HttpGet]
        public async Task<IActionResult> GetSubjectsAsync()
        {
            var subjects = await _subjectService.GetSubjectsAsync();
            return Ok(subjects);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(
            [FromHeader(Name = "Api-Key")] string apiKey, 
            [FromHeader(Name = "User-Id")] string userId, 
            [FromBody] CreateSubjectDto request)
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
            await _subjectService.CreateSubjectAsync(request);

            return Created();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(
            int id,
            [FromHeader(Name = "Api-Key")] string apiKey,
            [FromHeader(Name = "User-Id")] string userId,
            [FromBody] UpdateSubjectDto request)
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
            var result = await _subjectService.UpdateSubjectAsync(id, request);
            if (result == false) return BadRequest();

            return NoContent();
        }
        
        



    }
}
