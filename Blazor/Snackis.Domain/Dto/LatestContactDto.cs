using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Domain.DTO
{
    public class ContactDto
    {
        public string UserId { get; set; }
        public string DisplayName { get; set; }
        public string AvatarUrl { get; set; }
        public string TextContent { get; set; }
        public DateTime LatestMessageTime { get; set; }
    }
}
