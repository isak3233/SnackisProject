using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Snackis.Application.DTO;
using Snackis.Application.Interfaces;
using Snackis.Application.Services;

namespace Snackis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IAuthService _authService;
        public MessageController(IMessageService messageService, IAuthService authService)
        {
            _messageService = messageService;
            _authService = authService;
        }
        [HttpGet("{receiverUserId}")]
        public async Task<IActionResult> GetMessages(
            string receiverUserId,
            [FromHeader(Name = "Api-Key")] string apiKey,
            [FromHeader(Name = "User-Id")] string userId,
            [FromQuery] int start = 0,
            [FromQuery] int count = 10
            )
        {
            var user = await _authService.AuthorizeAsync(apiKey, userId);
            if (user == null)
            {
                return Unauthorized();
            }
            try
            {
                var messages = await _messageService.GetMessagesAsync(userId, receiverUserId, start, count);
                return Ok(messages);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
        [HttpGet("latestcontacts")]
        public async Task<IActionResult> GetLatestContacts(
            [FromHeader(Name = "Api-Key")] string apiKey,
            [FromHeader(Name = "User-Id")] string userId,
            [FromQuery] int start = 0,
            [FromQuery] int count = 10
            )
        {
            var user = await _authService.AuthorizeAsync(apiKey, userId);
            if (user == null)
            {
                return Unauthorized();
            }
            try
            {
                var latestContacts = await _messageService.GetLatestContactsAsync(userId, start, count);
                return Ok(latestContacts);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> SendMessageAsync(
            [FromHeader(Name = "Api-Key")] string apiKey,
            [FromHeader(Name = "User-Id")] string senderUserId,
            [FromBody] CreateMessageDto request
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request data.");
            }
            var user = await _authService.AuthorizeAsync(apiKey, senderUserId);
            if(user == null)
            {
                return Unauthorized();
            }
            try
            {
                await _messageService.CreateMessageAsync(senderUserId, request);
                return Created();
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            

        }
    }
}
