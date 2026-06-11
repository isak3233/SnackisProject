using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Domain.DTO
{
    public class MessageDto
    {
        public string SenderId { get; set; }
        public string DisplayName { get; set; }
        public string AvatarUrl { get; set; }
        public string TextContent { get; set; }
        public DateTime SentAt { get; set; }
    }
}
